namespace RedisTr.utils
{
    public static class Auth
    {
        public static string Host { get; set; } = "host.docker.internal";
        public static string Pwd { get; set; } = "d9ezTxevOkZYhxs8b31O/cdum/2sL9I/fXB3fBJFzLaHZnqUfBdGuOaRwo5hRyIWCk4eGAtcCAWvJVdhbsr";
    }
    public class MPgSecret2
    {
        static readonly MPgSecret2 _secret = new MPgSecret2();
       

        public static MPgSecret2 Secret => _secret;

       
        public string PgName { get; private set; } = "postgres";
        public string PgPwd { get; private set; } = "ion100312873";
        public string Token { get; private set; } = "f92780be-b1f5-4740-addf-cc6f95888b33";
    }
}