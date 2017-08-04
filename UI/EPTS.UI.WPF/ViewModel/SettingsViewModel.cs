using EPTS.UI.WPF.ViewModel.Devices;
using EPTS.UI.WPF.ViewModel.Events;


namespace EPTS.UI.WPF.ViewModel
{
    public class SettingsViewModel: ViewModelBase, ISettingsViewModel
    {

        public DeviceViewModel DeviceViewModel { get;  set; }
        public DeviceEvent DeviceEvent { get; set; }

        public SettingsViewModel(DeviceEvent deviceEvent)
        {
            DeviceEvent = deviceEvent;
            DeviceViewModel = deviceEvent.DeviceViewModel;
            
        }

    }

    public interface ISettingsViewModel
    {
        DeviceViewModel DeviceViewModel { get; set; }
        DeviceEvent DeviceEvent { get; set; }
    }


}
