//using PropertyChanged;
using DeviceInformation = EPTS.UI.ViewModel.Models.Devices.Core.DeviceInformation;

namespace EPTS.UI.ViewModel.Models.Devices.VisaCom
{
    public class Visa : Core.DeviceInformation
    {
        //[AlsoNotifyFor("DeviceAddress")]
        public string DeviceAddress { get; set; }
    }
}