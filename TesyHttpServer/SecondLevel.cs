using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RedisRPC;
using TesyHttpServer.models;

namespace TesyHttpServer
{
    class SecondLevel
    {
        public static async Task WorkerHttpAsync(HttpListenerContext c)
        {

          
            string body = null;
            try
            { 
                body=new StreamReader(c.Request.InputStream).ReadToEnd();

            }
            catch (Exception e)
            {
                await Utils.AddBody(c, "Не верное тело запроса");
                Console.WriteLine($"Error: 400 Не верное тело запроса. {e}");
                c.Response.StatusCode = 403;
                return;
            }
           
           


            if (string.IsNullOrEmpty(body))
            {
                await Utils.AddBody(c, "Не могу проебразовать тело запроса в структуру входных данных");
                Console.WriteLine("Error: 403 Не могу проебразовать тело запроса в структуру входных данных. ");
                c.Response.StatusCode = 403;
                return;
            }

            MDataIn mDataIn = null;

           
            try
            {
                mDataIn = JsonConvert.DeserializeObject<MDataIn>(body);
                if (mDataIn == null)
                {
                    await Utils.AddBody(c, "Не могу проебразовать тело запроса в структуру входных данных. (JSON)");
                    c.Response.StatusCode = 403;
                    Console.WriteLine("Error: 403 Не могу проебразовать тело запроса в структуру входных данных. (JSON)");
                    return;
                }

            }
            catch (Exception e)
            {
                await Utils.AddBody(c, e.Message);
                c.Response.StatusCode = 403;
                Console.WriteLine($"Error: 403 {e.Message}");

                return;
            }
           


            MDataOut mDataOut =new MDataOut(mDataIn);
           try
           {
               //await mDataIn.Validate();
           
           }
           catch (Exception e)
           {
               mDataOut.SetErrorData(ErrorCode.PrintError, $"{e.Message} {Utils.ErrorPostfix(mDataIn)}");
               c.Response.StatusCode = 200;
               var ee = JsonConvert.SerializeObject(value: mDataOut);
               Console.WriteLine($"Error: {ee}");
               await Utils.AddBody(c, ee);
           }
            //c.Response.StatusCode = 200;
            //await Utils.AddBody(c, "{}");
            //return;

            IRedisConnectionE connectionE = FactorySingleton.GetSingleton<IRedisConnectionE>();
            var wc = connectionE.GConnectionMultiplexer();
            var b = connectionE.GetServer().SubscriptionSubscriberCountAsync(Utils.ChanelName(mDataIn)).Result;
            if (b == 0)
            {
                mDataOut.SetErrorData(ErrorCode.PrintError,
                    $"Печатающее устройство не подключено {Utils.ErrorPostfix(mDataIn)} ");

                c.Response.StatusCode = 200;
                var ee = JsonConvert.SerializeObject(value: mDataOut);
                Console.WriteLine($"Error: {ee}");
                await Utils.AddBody(c, ee);

            }

            if (b > 1)
            {
                mDataOut.SetErrorData(ErrorCode.PrintError,
                    $"Печатающx устройств больше чем одно {b} {Utils.ErrorPostfix(mDataIn)}");
                c.Response.StatusCode = 200;
                var ee = JsonConvert.SerializeObject(value: mDataOut);
                Console.WriteLine($"Error: {ee}");
                await Utils.AddBody(c, ee);
                return;
            }
            var sub = wc.GetSubscriber();
            string resp = string.Empty;
            try
            {

                resp = await sub.CallerRcpExtAsync(Utils.ChanelName(mDataIn), body);
                AnswerPrinter answerPrinter = JsonConvert.DeserializeObject<AnswerPrinter>(resp);
                if (answerPrinter == null)
                {
                    mDataOut.SetErrorData(ErrorCode.PrintError, "Пустой ответ от принтера");
                }
                else
                {
                    if (answerPrinter.IsOk)
                    {
                        mDataOut.Status = Status.Printed;
                    }
                    else
                    {
                        mDataOut.SetErrorData(ErrorCode.PrintError, answerPrinter.ErrorMessage);
                    }
                }
            }
            catch (Exception e)
            {
                c.Response.StatusCode = (int)HttpStatusCode.InternalServerError; ;
                await Utils.AddBody(c, $"Error: {e.Message} {e.StackTrace}");
                Console.WriteLine($"Error: {e.Message}");
                return;
            }
            c.Response.StatusCode = (int)HttpStatusCode.OK;
            c.Response.ContentType = "application/json; charset=utf-8";
            await Utils.AddBody(c, JsonConvert.SerializeObject(value: mDataOut));
            Console.WriteLine("oк");
        }
    }
    public class AnswerPrinter
    {
        public bool IsOk { get; set; }
        public string ErrorMessage { get; set; }
    }
}