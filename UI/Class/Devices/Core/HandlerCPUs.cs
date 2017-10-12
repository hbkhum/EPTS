using System;
using System.Threading;

namespace Devices.Core
{
    public static class HandlerCpu
    {

        public static void Sleep(int mSec)
        {
            Thread.Sleep(mSec);
        }
        public static void Delay(int mstimeout)
        {
            DateTime cTime = DateTime.Now;
            do
            {
            } while (DateTime.Now.Subtract(cTime).TotalMilliseconds <= mstimeout);
        }
    }
}
