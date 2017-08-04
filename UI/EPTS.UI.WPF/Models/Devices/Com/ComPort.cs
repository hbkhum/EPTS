using PropertyChanged;
using DeviceInformation = EPTS.UI.WPF.Models.Devices.Core.DeviceInformation;

namespace EPTS.UI.WPF.Models.Devices.Com
{
    public class ComPort : Core.DeviceInformation
    {
        [AlsoNotifyFor("Com")]
        public string Com { get; set; }

        [AlsoNotifyFor("BaudRate")]
        public string BaudRate { get; set; }
    }
}
