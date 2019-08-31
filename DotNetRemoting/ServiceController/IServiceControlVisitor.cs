namespace DotNetRemoting.ServiceController
{
    public interface IServiceControlVisitor
    {
        void Visit(ServiceControlStart serviceControlStart);
        void Visit(ServiceControlStop serviceControlStop);
    }
}