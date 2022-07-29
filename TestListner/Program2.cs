using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestListener.models.pg;

namespace TestListener
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
   

    namespace TestListener
    {
        internal static class Program
        {
            private static Exception staException;
            private static HttpServer _server = new HttpServer(10);

            internal static void Main(string[] args)
            {

                _server.ProcessRequest += async context => await WorkerCore(context);
                _server.Start(1234);
                Console.WriteLine("start 1234");
            }

         

            private static async Task WorkerCore(HttpListenerContext c)
            {
               c.Response.StatusCode=200;
              await Utils.AddBody(c, "dsdsd");
            }
        }
    }

}
