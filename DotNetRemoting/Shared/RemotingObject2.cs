using System;

namespace DotNetRemoting.Shared
{
    public class RemotingObject2 : MarshalByRefObject, IRemoteObject
    {
        public string Text { get; set; } = "Default initialized second Remoting Object";
    }
}
