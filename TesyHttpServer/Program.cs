using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.VisualBasic.CompilerServices;

namespace TesyHttpServer
{
    class Program
    {
        private static Exception staException;
        static void Main(string[] args)
        {
            Console.WriteLine("run server");
            HttpServer server = new HttpServer(200);
            server.ProcessRequest += async context =>
            {
                try
                {
                    await FirstLayer.WorkerHttpAsync(context);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    // доступ у удаленному объекту, клиент отключился
                }
                finally
                { 
                    //context.Response.Close();

                }
               
                
                
            };
            FactorySingleton.AddSingleton<IRedisConnectionE, RedisConnectionSingleton>();
            server.Start(80);
            Console.WriteLine(" start ok");
            Console.Read();

        }
        public static async Task AddBody(HttpListenerContext c, string body)
        {
            await using var sw = new StreamWriter(c.Response.OutputStream);
            await sw.WriteAsync(body);
            await sw.FlushAsync();
        }
    }
}
