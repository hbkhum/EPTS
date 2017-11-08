
namespace EPTS.UI.ViewModel.Model.Devices.TCPIP
{
    public abstract class Socket : Model.Devices.Core.DeviceInformation
    {
        //[AlsoNotifyFor("IpAddress")]
        public string IpAddress { get; set; }

        //[AlsoNotifyFor("Port")]
        public int Port { get; set; }
    }
}