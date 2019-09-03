using System;

namespace DotNetRemoting.Shared
{
    public class RemotingObject : MarshalByRefObject
    {
        public string Text { get; set; } = "Default initialized Remoting Object";
    }
}
