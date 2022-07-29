using System.Net;
using System.Threading.Tasks;

namespace TesyHttpServer
{
    class FirstLayer
    {
     
        public static async Task WorkerHttpAsync(HttpListenerContext c)
        {

            c.Response.Headers.Add("content-type: application/json; charset=utf-8");
            c.Response.Headers.Add("transfer-encoding: chunked");
            c.Response.Headers.Add("x-xss-protection:1; mode=block");
            c.Response.Headers.Add("x-frame-options:DENY");
            c.Response.Headers.Add("cache-control:no-store, no-cache, must-revalidate");
            c.Response.Headers.Add("pragma:no-cache");
            c.Response.Headers.Add("Server", "jl");






            if (c.Request.HttpMethod!="POST")
            {
                c.Response.StatusCode = 403;
                await Utils.AddBody(c,"Только POST");
                return;
            }



           
            await SecondLevel.WorkerHttpAsync(c);

            



        }

      
    }
}
