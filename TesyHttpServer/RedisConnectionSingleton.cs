using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace TesyHttpServer
{
    public class RedisConnectionSingleton : IRedisConnectionE
    {
        private readonly ConnectionMultiplexer _connectionMultiplexer;
        private readonly IServer _server = null;

        public RedisConnectionSingleton()
        {
            Task<ConnectionMultiplexer> redis = ConnectionMultiplexer.ConnectAsync(
                new ConfigurationOptions //host.docker.internal
                {
                    Password = Auth.Pwd,
                    EndPoints = { $"host.docker.internal:6379" },
                    AbortOnConnectFail = false,
                });
            _connectionMultiplexer = redis.Result;
            _server = _connectionMultiplexer.GetServer($"{Auth.Host}:6379");
        }

        public ConnectionMultiplexer GConnectionMultiplexer()
        {
            return _connectionMultiplexer;
        }

        public IServer GetServer()
        {
            return _server;
        }
    }

    public interface IRedisConnectionE
    {
        ConnectionMultiplexer GConnectionMultiplexer();
        IServer GetServer();
    }
}