using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TestListener.models.pg;

namespace TestListener
{
    internal static class Program2
    {
        private static Exception staException;


        static int port = 80;

        static string url = "localhost";//host.docker.internal
        internal static async Task Main2(string[] args)
        {
            //ThreadPool.SetMaxThreads(10000, 10000);
            MainAsync(args).GetAwaiter().GetResult();
          
        }

        private static async Task MainAsync(string[] args)
        {
            var tcs = new TaskCompletionSource<object>();

            Console.CancelKeyPress += (sender, e) => { tcs.SetResult(null); };

            var server = new AsyncHttpServer(portNumber: port);
            var task = Task.Run(() => server.Start());



            await Console.Out.WriteLineAsync("Listening on port 1234. Open http://host.docker.internal:1234 in your browser.");
            await Console.Out.WriteLineAsync("Trying to connect:");
            await Console.Out.WriteLineAsync();
            //await GetResponseAsync($"http://{url}:{port}");
            await Console.Out.WriteLineAsync("Press Ctrl+C to stop the server...");




            await tcs.Task;
            await server.Stop();
        }

        private static async Task GetResponseAsync(string url)
        {
            using (var client = new HttpClient())
            {
                var responseMessage = await client.GetAsync(url);
                var responseHeaders = responseMessage.Headers.ToString();
                var response = await responseMessage.Content.ReadAsStringAsync();

                Console.WriteLine("Response headers:");
                Console.WriteLine(responseHeaders);
                Console.WriteLine("Response body:");
                Console.WriteLine(response);
                Console.WriteLine();
            }
        }

        private class AsyncHttpServer
        {
            private const string ResponseTemplate =
                "<html>" +
                    "<head>" +
                        "<title>Test</title>" +
                    "</head>" +
                    "<body>" +
                        "<h2>Test page</h2>" +
                        "<h4>Today is: {0}</h4>" +
                    "</body>" +
                "</html>";

            private readonly HttpListener _listener;

            public AsyncHttpServer(int portNumber)
            {
                _listener = new HttpListener();
               // ServicePoint sp = ServicePointManager.FindServicePoint(uri);
               // sp.ConnectionLimit = newLimit;
                //ServicePointManager.DefaultConnectionLimit = 200;
                _listener.Prefixes.Add($"http://+:{port}/");
            }
            HttpListenerTimeoutManager manager;
            public async Task Start()
            {
               
                    // manager = _listener.TimeoutManager;
                    // manager.IdleConnection = TimeSpan.FromMilliseconds(1750);
                    // manager.HeaderWait = TimeSpan.FromMilliseconds(1750);
                    _listener.Start();
                    try
                    {
                        //Регистрация синглетонов
                        FactorySingleton.AddSingleton<IRedisConnectionE, RedisConnectionSingleton>();
                        //Регистрация соединения с базой
                        Starter.Start();
                        //IRedisConnectionE d = FactorySingleton.GetSingleton<IRedisConnectionE>();
                    }
                    catch (Exception e)
                    {
                        staException = e;
                    }

                    while (true)
                    {
                        HttpListenerContext ctx = await _listener.GetContextAsync();
                        if (staException != null)
                        {
                            ctx.Response.StatusCode = 500;
                            await Utils.AddBody(ctx, $"При старте сервера произошла ошибка: {staException}");
                            return;


                        }
                        else
                        {
                            try
                            { 
                                await FirstLayer.WorkerHttpAsync(ctx);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                                // доступ у удаленному объекту, клиент отключился
                            }
                           
                        }


                        // await Console.Out.WriteLineAsync("Client connected...");
                        // Console.Out.WriteLine("Serving file: '{0}'", ctx.Request.Url);
                        // 
                        // 
                        // ctx.Response.Headers.Add("content-type: text/html; charset=UTF-8");
                        // ctx.Response.Headers.Add("x-content-type-options: nosniff");
                        // ctx.Response.Headers.Add("x-xss-protection:1; mode=block");
                        // ctx.Response.Headers.Add("x-frame-options:DENY");
                        // ctx.Response.Headers.Add("cache-control:no-store, no-cache, must-revalidate");
                        // ctx.Response.Headers.Add("pragma:no-cache");
                        // ctx.Response.Headers.Add("Server", "jl");
                        // 
                        // var response = string.Format(ResponseTemplate, DateTime.Now);
                        // await using var sw = new StreamWriter(ctx.Response.OutputStream);
                        // await sw.WriteAsync(response);
                        // await sw.FlushAsync();
                    }
                
               
             
            }

            public async Task Stop()
            {
                await Console.Out.WriteLineAsync(
                    "Stopping server...");

                if (_listener.IsListening)
                {
                    _listener.Stop();
                    _listener.Close();
                }
            }
        }
    }
}

