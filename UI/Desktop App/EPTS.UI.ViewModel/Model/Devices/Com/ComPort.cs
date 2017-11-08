
using DeviceInformation = EPTS.UI.ViewModel.Model.Devices.Core.DeviceInformation;

namespace EPTS.UI.ViewModel.Model.Devices.Com
{
    public class ComPort : Core.DeviceInformation
    {
        //[AlsoNotifyFor("Com")]
        public string Com { get; set; }

        //[AlsoNotifyFor("BaudRate")]
        public string BaudRate { get; set; }
    }
}
