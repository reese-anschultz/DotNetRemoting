namespace DotNetRemoting.ServiceController
{
    public interface IServiceControl
    {
        void Accept(IServiceControlVisitor visitor);
    }
}