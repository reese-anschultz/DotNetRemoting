using System;
using System.Threading;

namespace DotNetRemoting
{
    public class ServerController : MarshalByRefObject
    {
        private readonly Thread _thread = new Thread(Main);
        private readonly ManualResetEvent _joinEvent = new ManualResetEvent(false);
        public void Start()
        {
            _thread.Start(this);
        }

        public void Join()
        {
            _joinEvent.Set();
            _thread.Join();
        }

        private static void Main(object obj)
        {
            if (!(obj is ServerController serverController))
                return;

            WaitHandle.WaitAny(new WaitHandle[] { serverController._joinEvent });
        }
    }
}
