using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPTS.UI.ViewModel.Devices;
using EPTS.UI.ViewModel.Devices.SEAMAX;

namespace EPTS.UI.ViewModel
{
    public class SettingsViewModel: ViewModelBase, ISettingsViewModel
    {

        public DeviceViewModel DeviceViewModel { get; set; }

        public SettingsViewModel(DeviceViewModel deviceViewModel)
        {
            DeviceViewModel = deviceViewModel;
            //DeviceViewModel.SeaMaxViewModel.SeaMaxDeviceRepository.SeaIO420[0].
        }
    }

    public interface ISettingsViewModel
    {
        DeviceViewModel DeviceViewModel { get; set; }
    }
}
