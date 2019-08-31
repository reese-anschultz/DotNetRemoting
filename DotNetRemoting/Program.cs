using System;
using System.Threading;

namespace DotNetRemoting
{
    public static class Program
    {
        public static void Main()
        {
            var serverAppDomain = AppDomain.CreateDomain("server");
            try
            {
                var serverController = serverAppDomain.CreateInstanceAndUnwrap(typeof(ServerController).Assembly.FullName, typeof(ServerController).Namespace + "." + typeof(ServerController).Name) as ServerController;
                Console.WriteLine("About to start server");
                serverController?.Start();
                try
                {
                    Console.WriteLine("About to sleep");
                    Thread.Sleep(10 * 1000);
                }
                finally
                {
                    Console.WriteLine("About to join server");
                    serverController?.Join();
                }
            }
            finally
            {
                AppDomain.Unload(serverAppDomain);
            }
            Console.WriteLine("Done");
        }
    }
}
