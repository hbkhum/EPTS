//using PropertyChanged;

namespace EPTS.UI.ViewModel.Model.Devices.VisaCom
{
    public class Visa : Model.Devices.Core.DeviceInformation
    {
        //[AlsoNotifyFor("DeviceAddress")]
        public string DeviceAddress { get; set; }
    }
}