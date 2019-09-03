using DotNetRemoting.ServiceController;
using System;
using System.IO;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Threading;

namespace DotNetRemoting.Server
{
    public class ServerService : DefaultServiceControlVisitor
    {
        private readonly ManualResetEvent _stopEvent = new ManualResetEvent(false);
        public override void Visit(ServiceControlStart serviceControlStart)
        {
            _stopEvent.Reset();
            ServerMain(serviceControlStart.Args);
            WaitHandle.WaitAny(new WaitHandle[] { _stopEvent });
        }

        public override void Visit(ServiceControlStop serviceControlStop)
        {
            _stopEvent.Set();
        }

        private static int ServerMain(string[] args)
        {
            var configFilename = Path.Combine(Path.GetDirectoryName(typeof(ServerService).Assembly.Location) ?? throw new InvalidOperationException(), "server.config");
            RemotingConfiguration.Configure(configFilename, false);
            var x = ChannelServices.RegisteredChannels;
            var y = RemotingConfiguration.GetRegisteredWellKnownServiceTypes();
            return 0;
        }
    }
}
