using System.IO;
using System.Xml.Serialization;
using EPTS.Repositories.XML.Devices.Repositories;
using EPTS.UI.ViewModel.Devices.SEAMAX;

namespace EPTS.UI.ViewModel.Devices
{
    public class DeviceViewModel : ViewModelBase, IDeviceViewModel
    {

        public DeviceViewModel(SeaMaxViewModel seaMaxViewModel)
        {
            SeaMaxViewModel = seaMaxViewModel;
        }

        public SeaMaxViewModel SeaMaxViewModel { get; set; }
    }


    public interface IDeviceViewModel
    {
        SeaMaxViewModel SeaMaxViewModel { get; set; }

    }

}
