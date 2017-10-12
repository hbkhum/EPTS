using System.Linq;
using Devices;
using Devices.Repositories;
using Devices.SEAMAX;
using Devices.SEAMAX.Core;
using Devices.TCPIP;
using Devices.VisaCom.DMM;
using Devices.VisaCom.Power;
using EPTS.UI.ViewModel.Models.Devices.SEAMAX;
using EPTS.UI.ViewModel.Models.Devices.SEAMAX.Core;
using EPTS.UI.ViewModel.Devices;

namespace EPTS.UI.ViewModel.Events
{
    public class DeviceEvent
    {
        private  readonly DeviceRepository _deviceRepository;
        public DeviceViewModel DeviceViewModel { get; set; }
        public DeviceRepository DeviceRepository { get; set; }

        public DeviceEvent(DeviceViewModel deviceViewModel)
        {
            DeviceViewModel = deviceViewModel;
            _deviceRepository = new DeviceRepository
            {
                SeaMaxDeviceRepository = new SeaMaxDeviceRepository
                {
                    SeaLevel420Device = deviceViewModel.SeaMaxViewModelRepository.SeaLevel420
                        .Select(c => new SeaLevel420Device
                        {
                            Com = c.Com,
                            SlaveId = c.SlaveId,
                            DigitalOutput = c.DigitalOutput
                                .Select(n => new DigitalOutputDevice
                                {
                                    Description = n.Description
                                }).ToList(),
                            DigitalInput = c.DigitalInput
                                .Select(n => new DigitalInputDevice
                                {
                                    Description = n.Description,
                                }).ToList()
                        }).ToList(),
                },
                VisaComDeviceRepository = new VisaComDeviceRepository
                {
                    Multimeter34401A = deviceViewModel.VisaViewModelRepository.DMM
                        .Select(c => new Multimeter34401A
                        {
                            DeviceId = c.DeviceId,
                            DeviceName = c.DeviceName,
                            DeviceDescription = c.DeviceDescription,
                            DeviceAddress = c.DeviceAddress,
                        }).ToList(),
                    Power3645A = deviceViewModel.VisaViewModelRepository.Power
                        .Select(c => new Power3645A
                        {
                            DeviceId = c.DeviceId,
                            DeviceName = c.DeviceName,
                            DeviceDescription = c.DeviceDescription,
                            DeviceAddress = c.DeviceAddress,
                        }).ToList()
                },
                SocketDeviceRepository = new SocketDeviceRepository
                {
                    CamLineDevice = deviceViewModel.SocketViewModelRepository.CamLine
                        .Select(c => new CamLineDevice
                        {
                            DeviceId = c.DeviceId,
                            DeviceName = c.DeviceName,
                            DeviceDescription = c.DeviceDescription,
                            IpAddress = c.IpAddress,
                            Port = c.Port
                        }).ToList(),
                    RobotDevice = deviceViewModel.SocketViewModelRepository.Robot
                        .Select(c => new RobotDevice
                        {
                            DeviceId = c.DeviceId,
                            DeviceName = c.DeviceName,
                            DeviceDescription = c.DeviceDescription,
                            IpAddress = c.IpAddress,
                            Port = c.Port
                        }).ToList()
                }
            };
            DeviceRepository = _deviceRepository;
            DeviceRepository.VisaComDeviceRepository.Multimeter34401A.ForEach(c => c.MeasureEvent += OnMeasureEvent);
            DeviceRepository.VisaComDeviceRepository.Multimeter34401A.ForEach(c => c.MeasureTypeEvent += OnMeasureTypeEvent);
            DeviceRepository.VisaComDeviceRepository.Power3645A.ForEach(c => c.CurrentEvent += OnCurrentEvent);
            DeviceRepository.VisaComDeviceRepository.Power3645A.ForEach(c => c.VoltageEvent += OnVoltageEvent);
            DeviceRepository.SeaMaxDeviceRepository.SeaLevel420Device.ForEach(c => c.DigitalOutput.ForEach(n => n.DigitalOutputEvent += c.OnSeaLevelDigitalOutput));
            DeviceRepository.SeaMaxDeviceRepository.SeaLevel420Device.ForEach(c => c.DigitalInput.ForEach(n => n.DigitalInputEvent += c.OnSeaLevelDigitalInput));
            DeviceRepository.SeaMaxDeviceRepository.SeaLevel420Device.ForEach(c => c.SeaLevelDigitalOutput += OnSeaLevelDigitalOutputEvent);
            DeviceRepository.SeaMaxDeviceRepository.SeaLevel420Device.ForEach(c => c.SeaLevelDigitalInput += OnSeaLevelDigitalInputEvent);
            deviceViewModel.SeaMaxViewModelRepository.DigitalOutputEvent += SeaMaxViewModelRepository_DigitalOutputEvent;
            deviceViewModel.SeaMaxViewModelRepository.DigitalInputEvent += SeaMaxViewModelRepository_DigitalInputEvent;
        }
        private  void OnVoltageEvent(object sender, string voltage)
        {
            var type = (Power3645A)sender;
            var power3645AIndex = _deviceRepository.VisaComDeviceRepository.Power3645A.IndexOf(type);
            DeviceViewModel.VisaViewModelRepository.Power[power3645AIndex].Voltage = voltage;
        }
        private  void OnCurrentEvent(object sender, string current)
        {
            var type = (Power3645A)sender;
            var power3645AIndex = _deviceRepository.VisaComDeviceRepository.Power3645A.IndexOf(type);
            DeviceViewModel.VisaViewModelRepository.Power[power3645AIndex].Current = current;
        }
        private  void OnMeasureTypeEvent(object sender, string measuretype)
        {
            var type = (Multimeter34401A)sender;
            var multimeter34401AIndex = _deviceRepository.VisaComDeviceRepository.Multimeter34401A.IndexOf(type);
            DeviceViewModel.VisaViewModelRepository.DMM[multimeter34401AIndex].MeasureType = measuretype;
        }
        private  void OnMeasureEvent(object sender, string measure)
        {
            var type = (Multimeter34401A)sender;
            var multimeter34401AIndex = _deviceRepository.VisaComDeviceRepository.Multimeter34401A.IndexOf(type);
            DeviceViewModel.VisaViewModelRepository.DMM[multimeter34401AIndex].Measure = measure;
        }
        private  void SeaMaxViewModelRepository_DigitalInputEvent(object sender, int seaioIndex, int digitalinputindex, DigitalInput digitalInput)
        {
            var type = sender.GetType();
            if (type == typeof(SeaLevel420))
            {
                var seaioDevice = _deviceRepository.SeaMaxDeviceRepository.SeaLevel420Device[seaioIndex];
                var seaioDigitalintputDevice = seaioDevice.DigitalInput[digitalinputindex];
                var digitalInputDevice = _deviceRepository.SeaMaxDeviceRepository.SeaLevel420Device[seaioIndex].DigitalInput[digitalinputindex];

                seaioDigitalintputDevice.DigitalInputEvent -= seaioDevice.OnSeaLevelDigitalInput;
                seaioDevice.SeaLevelDigitalOutput -= OnSeaLevelDigitalOutputEvent;


                digitalInputDevice.Value = digitalInput.Value;
                _deviceRepository.SeaMaxDeviceRepository.SeaLevel420Device[seaioIndex].OnSeaLevelDigitalInput(digitalInputDevice);

                seaioDigitalintputDevice.DigitalInputEvent += seaioDevice.OnSeaLevelDigitalInput;
                seaioDevice.SeaLevelDigitalOutput += OnSeaLevelDigitalOutputEvent;
            }
        }
        private  void SeaMaxViewModelRepository_DigitalOutputEvent(object sender, int seaioIndex, int digitaloutputindex, DigitalOutput digitalOutput)
        {
            var type = sender.GetType();
            if (type == typeof(SeaLevel420))
            {
                var seaioDevice = _deviceRepository.SeaMaxDeviceRepository.SeaLevel420Device[seaioIndex];
                var seaioDigitaloutputDevice = seaioDevice.DigitalOutput[digitaloutputindex];
                var digitalOutputDevice = _deviceRepository.SeaMaxDeviceRepository.SeaLevel420Device[seaioIndex].DigitalOutput[digitaloutputindex];

                seaioDigitaloutputDevice.DigitalOutputEvent -= seaioDevice.OnSeaLevelDigitalOutput;
                seaioDevice.SeaLevelDigitalOutput -= OnSeaLevelDigitalOutputEvent;


                digitalOutputDevice.Value = digitalOutput.Value;
                _deviceRepository.SeaMaxDeviceRepository.SeaLevel420Device[seaioIndex].OnSeaLevelDigitalOutput(digitalOutputDevice);

                seaioDigitaloutputDevice.DigitalOutputEvent += seaioDevice.OnSeaLevelDigitalOutput;
                seaioDevice.SeaLevelDigitalOutput += OnSeaLevelDigitalOutputEvent;
            }
            else if (type == typeof(SeaLevel410))
            {
                var seaioDevice = _deviceRepository.SeaMaxDeviceRepository.SeaLevel410Device[seaioIndex];
                var seaioDigitaloutputDevice = seaioDevice.DigitalOutput[digitaloutputindex];
                var digitalOutputDevice = _deviceRepository.SeaMaxDeviceRepository.SeaLevel410Device[seaioIndex].DigitalOutput[digitaloutputindex];

                seaioDigitaloutputDevice.DigitalOutputEvent -= seaioDevice.OnDigitalOutputEvent;
                seaioDevice.SeaLevelDigitalOutput -= OnSeaLevelDigitalOutputEvent;


                digitalOutputDevice.Value = digitalOutput.Value;
                _deviceRepository.SeaMaxDeviceRepository.SeaLevel420Device[seaioIndex].OnSeaLevelDigitalOutput(digitalOutputDevice);

                seaioDigitaloutputDevice.DigitalOutputEvent += seaioDevice.OnDigitalOutputEvent;
                seaioDevice.SeaLevelDigitalOutput += OnSeaLevelDigitalOutputEvent;
            }
        }
        private void OnSeaLevelDigitalInputEvent(object sender, DigitalInputDevice digitalInput)
        {
            var seaio420Device = (SeaLevel420Device)sender;
            var seaio420Index = _deviceRepository.SeaMaxDeviceRepository.SeaLevel420Device.IndexOf(seaio420Device);
            var digitalinputindex = seaio420Device.DigitalInput.IndexOf(digitalInput);

            var seaio420 = DeviceViewModel.SeaMaxViewModelRepository.SeaLevel420[seaio420Index];
            var seaio420Digitalinput = DeviceViewModel.SeaMaxViewModelRepository.SeaLevel420[seaio420Index].DigitalInput[digitalinputindex];

            seaio420Digitalinput.DigitalInputEvent -= seaio420.OnSeaLevelDigitalInput;
            seaio420.SeaLevelDigitalInput -= DeviceViewModel.SeaMaxViewModelRepository.OnSeaIoDigitalInput<SeaLevel420>;

            DeviceViewModel.SeaMaxViewModelRepository.SeaLevel420[seaio420Index].DigitalInput[digitalinputindex].Value = digitalInput.Value;

            seaio420Digitalinput.DigitalInputEvent += seaio420.OnSeaLevelDigitalInput;
            seaio420.SeaLevelDigitalInput += DeviceViewModel.SeaMaxViewModelRepository.OnSeaIoDigitalInput<SeaLevel420>;
        }
        private void OnSeaLevelDigitalOutputEvent(object sender, DigitalOutputDevice digitalOutput)
        {
            var seaio420Device = (SeaLevel420Device)sender;
            var seaio420Index = _deviceRepository.SeaMaxDeviceRepository.SeaLevel420Device.IndexOf(seaio420Device);
            var digitaloutputindex = seaio420Device.DigitalOutput.IndexOf(digitalOutput);

            var seaio420 = DeviceViewModel.SeaMaxViewModelRepository.SeaLevel420[seaio420Index];
            var seaio420Digitaloutput = DeviceViewModel.SeaMaxViewModelRepository.SeaLevel420[seaio420Index].DigitalOutput[digitaloutputindex];

            seaio420Digitaloutput.DigitalOutputEvent -= seaio420.OnSeaLevelDigitalOutput;
            seaio420.SeaLevelDigitalOutput -= DeviceViewModel.SeaMaxViewModelRepository.OnSeaIoDigitalOutput<SeaLevel420>;

            DeviceViewModel.SeaMaxViewModelRepository.SeaLevel420[seaio420Index].DigitalOutput[digitaloutputindex].Value = digitalOutput.Value;

            seaio420Digitaloutput.DigitalOutputEvent += seaio420.OnSeaLevelDigitalOutput;
            seaio420.SeaLevelDigitalOutput += DeviceViewModel.SeaMaxViewModelRepository.OnSeaIoDigitalOutput<SeaLevel420>;

        }

    }
}