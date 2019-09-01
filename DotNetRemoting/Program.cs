using DotNetRemoting.Client;
using DotNetRemoting.Server;
using DotNetRemoting.ServiceController;
using System;

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
                    var clientAppDomain = AppDomain.CreateDomain("client");
                    try
                    {
                        var clientServiceType = typeof(ServiceController<ClientService>);
                        var clientController = clientAppDomain.CreateInstanceAndUnwrap(clientServiceType.Assembly.FullName, clientServiceType.FullName ?? throw new InvalidOperationException()) as ServiceController<ClientService>;
                        Console.WriteLine("About to start client");
                        clientController?.Start();
                        try
                        {
                            // Nothing really to do here
                        }
                        finally
                        {
                            Console.WriteLine("About to join client");
                            clientController?.Join();
                        }
                    }
                    finally
                    {
                        AppDomain.Unload(clientAppDomain);
                    }
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
