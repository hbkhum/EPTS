using System.Collections.Generic;
using System.IO.Ports;
using System.Runtime.InteropServices.ComTypes;
using Devices.Com;
using Devices.Repositories;
using Devices.SEAMAX;
using Devices.VisaCom.Com.Scanner;
using Devices.VisaCom.DMM;
using Devices.VisaCom.Power;

namespace Devices
{

    public  class DeviceRepository
    {
        public ComDeviceRepository ComRepository { get; set; }
        public SeaMaxDeviceRepository SeaMaxDeviceRepository { get; set; }
        public VisaComDeviceRepository VisaComDeviceRepository { get; set; }
        public SocketDeviceRepository SocketDeviceRepository { get; set; }


    }
}
