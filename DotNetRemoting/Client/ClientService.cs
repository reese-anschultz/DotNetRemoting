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
        private static readonly ManualResetEvent StopEvent = new ManualResetEvent(false);
        public override void Visit(ServiceControlStart serviceControlStart)
        {
            StopEvent.Reset();
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
            if (!(remotingObject2 is MarshalByRefObject objectToSponsor))
                throw new InvalidOperationException("Object is not MarshalByRefObject");

            var sponsor = new ClientSideSponsor(objectToSponsor);
            sponsor.Register(new TimeSpan(0, 0, 0, 0, 200));  // Register with lifetime of 2 seconds
            // Wait a while. Sponsor should get a renewal in this time.
            WaitHandle.WaitAny(new WaitHandle[] { StopEvent }, new TimeSpan(0,0,20));
            return 0;
        }
    }
}
