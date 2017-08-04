using System.Collections.ObjectModel;
using System.Linq;
using EPTS.Repositories.XML.Devices.Repositories;
using EPTS.UI.WPF.Models.Devices.TCPIP;

namespace EPTS.UI.WPF.ViewModel.Devices.TCPIP
{
    public class SocketViewModelRepository
    {
        public ObservableCollection<CamLine> CamLine { get; private set; }
        public ObservableCollection<Robot> Robot { get; private set; }
        public ObservableCollection<Modbus> Modbus { get; private set; }

        public SocketViewModelRepository(IDataRepositories dataRepositories)
        {
            CamLine =
                new ObservableCollection<CamLine>(dataRepositories.SocketDeviceRepository.CamLine
                    .Select(c => new CamLine
                    {
                        DeviceId = c.DeviceId,
                        DeviceName = c.DeviceName,
                        DeviceDescription = c.DeviceDescription,
                        IpAddress = c.IpAddress,
                        Port = c.Port
                    }).ToList());
            Robot = new ObservableCollection<Robot>(dataRepositories.SocketDeviceRepository.Robot
                .Select(c => new Robot
                {
                    DeviceId = c.DeviceId,
                    DeviceName = c.DeviceName,
                    DeviceDescription = c.DeviceDescription,
                    IpAddress = c.IpAddress,
                    Port = c.Port
                }).ToList());
            Modbus = new ObservableCollection<Modbus>(dataRepositories.SocketDeviceRepository.ModbusTcp
                .Select(c => new Modbus
                {
                    DeviceId = c.DeviceId,
                    DeviceName = c.DeviceName,
                    DeviceDescription = c.DeviceDescription,
                    IpAddress = c.IpAddress,
                    Port = c.Port
                }).ToList());  
        }
    }
}
