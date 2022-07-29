using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using TesyHttpServer.models;

namespace TesyHttpServer
{
   public static class  Utils
    {
        public static async Task AddBody(HttpListenerContext c, string body)
        {
            await using var sw = new StreamWriter(c.Response.OutputStream);
            await sw.WriteAsync(body);
            await sw.FlushAsync();
        }

        public static string RandomString(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789йцукенгшщзхъдлорпавыфячсмить";
            var stringChars = new char[length];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new string(stringChars);
        }

        public static string ErrorPostfix(MDataIn mDataIn)
        {
            return $"(hotel:{mDataIn.HotelId} PosId:{mDataIn.PosId})";
        }

        public static string ChanelName(MDataIn mDataIn)
        {
            return $"canel:{mDataIn.HotelId}:{mDataIn.PosId}";
        }
    }
}
