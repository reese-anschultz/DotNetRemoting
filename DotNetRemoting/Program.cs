using DotNetRemoting.Server;
using System;
using System.Threading;
using DotNetRemoting.ServiceController;

namespace DotNetRemoting
{
    public static class Program
    {
        public static void Main()
        {
            var serverAppDomain = AppDomain.CreateDomain("server");
            try
            {
                Console.WriteLine("About to load server");
                var serverServiceType = typeof(ServiceController<ServerService>);
                var serverController = serverAppDomain.CreateInstanceAndUnwrap(serverServiceType.Assembly.FullName, serverServiceType.FullName ?? throw new InvalidOperationException()) as ServiceController<ServerService>;
                Console.WriteLine("About to start server");
                serverController?.Start();
                try
                {
                    Console.WriteLine("About to sleep");
                    Thread.Sleep(5 * 1000);
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
