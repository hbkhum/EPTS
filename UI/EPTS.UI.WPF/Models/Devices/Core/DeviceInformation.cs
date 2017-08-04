using EPTS.UI.WPF.ViewModel;
using PropertyChanged;

namespace EPTS.UI.WPF.Models.Devices.Core
{
    public abstract class DeviceInformation:ViewModelBase
    {
        [AlsoNotifyFor("DeviceId")]
        public string DeviceId { get; set; }

        [AlsoNotifyFor("DeviceName")]
        public string DeviceName { get; set; }

        [AlsoNotifyFor("DeviceDescription")]
        public string DeviceDescription { get; set; }
    }
}