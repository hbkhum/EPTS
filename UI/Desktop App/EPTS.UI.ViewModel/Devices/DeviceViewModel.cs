using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using Devices.VisaCom.Core;
using EPTS.UI.ViewModel.Devices.SEAMAX;
using EPTS.UI.ViewModel.Devices.TCPIP;
using EPTS.UI.ViewModel.Devices.VisaCom;
using EPTS.Models;

namespace EPTS.UI.ViewModel.Devices
{
    public class DeviceViewModel : ViewModelBase, IDeviceViewModel
    {

        public DeviceViewModel(SeaMaxViewModelRepository seaMaxViewModelRepository,VisaViewModelRepository visaViewModelRepository, SocketViewModelRepository socketViewModelRepository)
        {
            SeaMaxViewModelRepository = seaMaxViewModelRepository;
            VisaViewModelRepository = visaViewModelRepository;
            SocketViewModelRepository = socketViewModelRepository;
        }
        public SeaMaxViewModelRepository SeaMaxViewModelRepository { get; set; }
        public VisaViewModelRepository VisaViewModelRepository { get; set; }
        public SocketViewModelRepository SocketViewModelRepository { get; set; }
    }


    public interface IDeviceViewModel
    {
        SeaMaxViewModelRepository SeaMaxViewModelRepository { get; set; }
        VisaViewModelRepository VisaViewModelRepository { get; set; }
        SocketViewModelRepository SocketViewModelRepository { get; set; }

    }


}
