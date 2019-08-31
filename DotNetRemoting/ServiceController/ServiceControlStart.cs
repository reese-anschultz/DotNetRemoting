namespace DotNetRemoting.ServiceController
{
    public class ServiceControlStart : IServiceControl
    {
        public string[] Args { get; }

        public ServiceControlStart(string[] args)
        {
            Args = args;
        }

        public void Accept(IServiceControlVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}