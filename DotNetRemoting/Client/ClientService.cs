using DotNetRemoting.ServiceController;
using DotNetRemoting.Shared;
using System;
using System.IO;
using System.Runtime.Remoting;
using System.Threading;

namespace DotNetRemoting.Client
{
    public class ClientService : DefaultServiceControlVisitor
    {
        private readonly ManualResetEvent _stopEvent = new ManualResetEvent(false);
        public override void Visit(ServiceControlStart serviceControlStart)
        {
            _stopEvent.Reset();
            ClientMain(serviceControlStart.Args);
        }

        private static int ClientMain(string[] args)
        {
            var configFilename = Path.Combine(Path.GetDirectoryName(typeof(ClientService).Assembly.Location) ?? throw new InvalidOperationException(), "client.config");
            RemotingConfiguration.Configure(configFilename, false);
            var remotingObject = new RemotingObject();
            Console.WriteLine($"Remoting object text = {remotingObject.Text}");
            var remotingObject2 = remotingObject.OtherRemotingObject;
            Console.WriteLine($"Remoting object 2 text = {remotingObject2.Text}");
            return 0;
        }
    }
}
