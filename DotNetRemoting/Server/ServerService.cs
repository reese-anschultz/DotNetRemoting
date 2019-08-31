using System.Threading;
using DotNetRemoting.ServiceController;

namespace DotNetRemoting.Server
{
    public class ServerService : DefaultServiceControlVisitor
    {
        private readonly ManualResetEvent _stopEvent = new ManualResetEvent(false);
        public override void Visit(ServiceControlStart serviceControlStart)
        {
            _stopEvent.Reset();
            WaitHandle.WaitAny(new WaitHandle[] { _stopEvent });
        }

        public override void Visit(ServiceControlStop serviceControlStop)
        {
            _stopEvent.Set();
        }
    }
}
