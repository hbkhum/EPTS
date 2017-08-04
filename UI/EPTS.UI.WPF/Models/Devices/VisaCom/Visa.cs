using PropertyChanged;
using DeviceInformation = EPTS.UI.WPF.Models.Devices.Core.DeviceInformation;

namespace EPTS.UI.WPF.Models.Devices.VisaCom
{
    public class Visa : Core.DeviceInformation
    {
        [AlsoNotifyFor("DeviceAddress")]
        public string DeviceAddress { get; set; }
    }
}