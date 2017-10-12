using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devices.TCPIP;

namespace Devices.Repositories
{
    public class SocketDeviceRepository : ISocketDeviceRepository
    {
        public List<RobotDevice> RobotDevice { get; set; }
        public List<CamLineDevice> CamLineDevice { get; set; }
        public List<ModbusDevice> ModbusDevice { get; set; }
    }

    public interface ISocketDeviceRepository
    {
        List<RobotDevice> RobotDevice { get; set; }
        List<CamLineDevice> CamLineDevice { get; set; }
        List<ModbusDevice> ModbusDevice { get; set; }
    }
}
