using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devices.Core;

namespace Devices.TCPIP
{
    public class CamLineDevice:SocketDevice
    {
        public string MessagaMes(string message)
        {
            WriteMessage(message);
            HandlerCpu.Delay(200);
            var data = ReadMessage();
            return data;
        }
    }
}
