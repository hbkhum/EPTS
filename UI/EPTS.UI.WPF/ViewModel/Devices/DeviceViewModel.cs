using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using Devices.VisaCom.Core;
using EPTS.UI.WPF.Models.Devices.VisaCom.Power;
using EPTS.UI.WPF.ViewModel.Devices.SEAMAX;
using EPTS.UI.WPF.ViewModel.Devices.TCPIP;
using EPTS.UI.WPF.ViewModel.Devices.VisaCom;

namespace EPTS.UI.WPF.ViewModel.Devices
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
