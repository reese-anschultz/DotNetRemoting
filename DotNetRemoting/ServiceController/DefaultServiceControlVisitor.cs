namespace DotNetRemoting.ServiceController
{
    public abstract class DefaultServiceControlVisitor : IServiceControlVisitor
    {
        public abstract void Visit(ServiceControlStart serviceControlStart);

        public virtual void Visit(ServiceControlStop serviceControlStop)
        { }
    }
}