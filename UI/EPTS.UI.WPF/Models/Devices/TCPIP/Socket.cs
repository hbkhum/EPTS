using PropertyChanged;
using DeviceInformation = EPTS.UI.WPF.Models.Devices.Core.DeviceInformation;

namespace EPTS.UI.WPF.Models.Devices.TCPIP
{
    public abstract class Socket : Core.DeviceInformation
    {
        [AlsoNotifyFor("IpAddress")]
        public string IpAddress { get; set; }

        [AlsoNotifyFor("Port")]
        public int Port { get; set; }
    }
}