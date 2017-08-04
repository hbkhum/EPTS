

using EPTS.UI.ViewModel.Devices;

namespace EPTS.UI.ViewModel
{
    public class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
    {
        public DeviceViewModel DeviceViewModel { get; set; }

        public MainWindowViewModel(DeviceViewModel deviceViewModel)
        {
            DeviceViewModel = deviceViewModel;
        }
    }

    public interface IMainWindowViewModel
    {
        DeviceViewModel DeviceViewModel { get; set; }
    }


}
