using System;
using System.Threading;

namespace DotNetRemoting.ServiceController
{
    public class ServiceController<TServiceControlVisitor> : MarshalByRefObject
        where TServiceControlVisitor : IServiceControlVisitor, new()
    {
        private readonly IServiceControlVisitor _serviceControlVisitor;

        private readonly Thread _thread;

        public ServiceController()
        {
            _serviceControlVisitor = new TServiceControlVisitor();
            _thread = new Thread(ServiceThreadMain) {Name = AppDomain.CurrentDomain.FriendlyName + " service"};
        }

        public void Start(string[] args = null)
        {
            _thread.Start(args);
        }

        public void Join()
        {
            DispatchServiceControl(new ServiceControlStop());
            _thread.Join();
        }

        private void ServiceThreadMain(object obj)
        {
            DispatchServiceControl(new ServiceControlStart(obj as string[]));
        }

        private void DispatchServiceControl(IServiceControl serviceControl)
        {
            serviceControl.Accept(_serviceControlVisitor);
        }
    }
}
