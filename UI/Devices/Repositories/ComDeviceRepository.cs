using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devices.Com;

namespace Devices.Repositories
{
    public interface IComRepository
    {
        List<ComPortDevice> Com { get; set; }

        List<ComPortDevice> Modbus { get; set; }
    }
    public class ComDeviceRepository : IComRepository
    {
        public List<ComPortDevice> Com { get; set; }
        public List<ComPortDevice> Modbus { get; set; }
    }
}
