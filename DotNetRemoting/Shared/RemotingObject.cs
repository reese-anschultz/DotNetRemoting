using System;

namespace DotNetRemoting.Shared
{
    public class RemotingObject : MarshalByRefObject, IRemoteObject
    {
        private readonly RemotingObject2 _remotingObject2 = new RemotingObject2();
        public string Text { get; set; } = "Default initialized Remoting Object";
        public IRemoteObject OtherRemotingObject => _remotingObject2;
    }
}
