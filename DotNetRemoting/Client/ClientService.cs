using DotNetRemoting.ServiceController;
using System.Threading;

namespace DotNetRemoting.Client
{
    public class ClientService : DefaultServiceControlVisitor
    {
        public override void Visit(ServiceControlStart serviceControlStart)
        {
            Thread.Sleep(5 * 1000);
        }
    }
}
