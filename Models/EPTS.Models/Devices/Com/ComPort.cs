
using DeviceInformation = EPTS.Models.Devices.Core.DeviceInformation;

namespace EPTS.Models.Devices.Com
{
    public class ComPort : Core.DeviceInformation
    {
        //[AlsoNotifyFor("Com")]
        public string Com { get; set; }

        //[AlsoNotifyFor("BaudRate")]
        public string BaudRate { get; set; }
    }
}
