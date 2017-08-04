using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPTS.Repositories.XML.Devices.Repositories;

namespace EPTS.UI.ViewModel.Devices.SEAMAX
{
    public class SeaMaxViewModel: ViewModelBase
    {
        public SeaMaxDeviceRepository SeaMaxDeviceRepository { get; private set; }

        public SeaMaxViewModel(IDataRepositories dataRepositories)
        {
            SeaMaxDeviceRepository = dataRepositories.SeaMaxDeviceRepository;
        }
    }
}
