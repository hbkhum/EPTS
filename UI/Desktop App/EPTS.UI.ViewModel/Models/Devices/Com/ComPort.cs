
using DeviceInformation = EPTS.UI.ViewModel.Models.Devices.Core.DeviceInformation;

namespace EPTS.UI.ViewModel.Models.Devices.Com
{
    public class ComPort : DeviceInformation
    {
        //[AlsoNotifyFor("Com")]
        public string Com { get; set; }

        //[AlsoNotifyFor("BaudRate")]
        public string BaudRate { get; set; }
    }
}
