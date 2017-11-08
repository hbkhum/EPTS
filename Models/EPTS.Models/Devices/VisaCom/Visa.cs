//using PropertyChanged;

using EPTS.Models.Devices.Core;

namespace EPTS.Models.Devices.VisaCom
{
    public class Visa : DeviceInformation
    {
        //[AlsoNotifyFor("DeviceAddress")]
        public string DeviceAddress { get; set; }
    }
}