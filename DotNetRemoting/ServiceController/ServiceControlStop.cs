namespace DotNetRemoting.ServiceController
{
    public class ServiceControlStop : IServiceControl
    {
        public void Accept(IServiceControlVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}