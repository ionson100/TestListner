using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RedisTr.utils;
using StackExchange.Redis;

namespace TestListener
{
    public class RedisConnectionSingleton : IRedisConnectionE,IDisposable
    {
        private static readonly int count = 100;
        //private readonly ConnectionMultiplexer _connectionMultiplexer;
        //private readonly IServer _server = null;
        List<WrapperConnection> connections=new List<WrapperConnection>(count);
        public RedisConnectionSingleton()
        {
           
            try
            {
                for (int i = 0; i < count; i++)
                {
                    Task<ConnectionMultiplexer> redis = ConnectionMultiplexer.ConnectAsync(
                        new ConfigurationOptions //host.docker.internal
                        {
                            Password = Auth.Pwd,
                            EndPoints = { $"{Auth.Host}:6379" },
                            AbortOnConnectFail = false,
                        });
                    var t= redis.Result;
                    connections.Add(new WrapperConnection
                    {
                        ConnectionMultiplexer = t,
                        IsOccupied = 0
                    });
                }
               
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
          
        }

        
       

        public void Dispose()
        {
            connections.ForEach(a=>a.Dispose());
        }

        public Task<WrapperConnection> GeWrapperConnection()
        {
           return GetInnerWrapper();
        }

        Task<WrapperConnection> GetInnerWrapper()
        {
            return Task.Run(() =>
            {
                while (true)
                {
                    foreach (var wc in connections)
                    {
                        var ss = Interlocked.Read(ref wc.IsOccupied);
                        if (ss == 0)
                        {
                            Interlocked.Increment(ref wc.IsOccupied);
                            return wc;
                        }
                    }
                }
            });
        }
    }

  public  class WrapperConnection:IDisposable
  {
      public ConnectionMultiplexer ConnectionMultiplexer;
        public long IsOccupied;
        public void Dispose()
        {
            ConnectionMultiplexer?.Dispose();
        }

        public IServer Server => ConnectionMultiplexer.GetServer($"{Auth.Host}:6379");
    }
    public interface IRedisConnectionE
    {
        Task<WrapperConnection> GeWrapperConnection();
      
    }
}