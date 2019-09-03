using System;
using System.Runtime.Remoting.Lifetime;

namespace DotNetRemoting.Client
{
    public class ClientSideSponsor : MarshalByRefObject, ISponsor
    {
        private readonly ILease _objectLease;

        public ClientSideSponsor(MarshalByRefObject objectToSponsor)
        {
            _objectLease = (ILease)objectToSponsor.GetLifetimeService();
        }

        public void Register(TimeSpan renewalTime)
        {
            _objectLease.Register(this, renewalTime);
        }

        public TimeSpan Renewal(ILease lease)
        {
            throw new NotImplementedException();
        }
    }
}
