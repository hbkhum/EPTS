using System.Collections.ObjectModel;
using System.Linq;
using EPTS.Repositories.XML.Devices.Repositories;
using EPTS.Models.Devices.SEAMAX;
using EPTS.Models.Devices.SEAMAX.Core;

namespace EPTS.UI.ViewModel.Devices.SEAMAX
{
    public class SeaMaxViewModelRepository
    {
        //private readonly DataRepositories _dataRepositories;

        public delegate void DigitalOutputHandler(object sender,int seaioIndex, int digitaloutputindex, DigitalOutput digitalOutput);
        public event DigitalOutputHandler DigitalOutputEvent;

        public delegate void DigitalInputHandler(object sender, int seaioIndex, int digitalinputindex, DigitalInput digitalInput);
        public event DigitalInputHandler DigitalInputEvent;

        public ObservableCollection<SeaLevel420> SeaLevel420 { get; private set; }
        public ObservableCollection<SeaLevel410> SeaLevel410 { get; private set; }

        public SeaMaxViewModelRepository(IDataRepositories dataRepositories)
        {
            SeaLevel420 =
                new ObservableCollection<SeaLevel420>(dataRepositories.SeaMaxDeviceRepository.SeaIO420
                    .Select(c => new SeaLevel420
                    {
                        DeviceId = c.DeviceId,
                        DeviceName = c.DeviceName,
                        DeviceDescription = c.DeviceDescription,
                        Com = c.COM,
                        SlaveId = c.SlaveId,
                        DigitalInput = c.DigitalInput
                            .Select(n => new DigitalInput
                            {
                                Description = n.Description
                            }).ToList(),
                        DigitalOutput = c.DigitalOutput
                            .Select(n => new DigitalOutput
                            {
                                Description = n.Description

                            }).ToList(),
                    }).ToList()) ;
            SeaLevel410 = new ObservableCollection<SeaLevel410>(dataRepositories.SeaMaxDeviceRepository.SeaIO410
                .Select(c => new SeaLevel410
                {
                    DeviceId = c.DeviceId,
                    DeviceName = c.DeviceName,
                    DeviceDescription = c.DeviceDescription,
                    Com = c.COM,
                    SlaveId = c.SlaveId,
                    DigitalOutput = c.DigitalOutput
                        .Select(n => new DigitalOutput
                        {
                            Description = n.Description
                        }).ToList()
                }).ToList());
            


            SeaLevel420.ToList().ForEach(c => c.DigitalOutput.ForEach(n => n.DigitalOutputEvent += c.OnSeaLevelDigitalOutput));
            SeaLevel420.ToList().ForEach(c => c.SeaLevelDigitalOutput += OnSeaIoDigitalOutput<SeaLevel420>);

            SeaLevel420.ToList().ForEach(c => c.DigitalInput.ForEach(n => n.DigitalInputEvent += c.OnSeaLevelDigitalInput));
            SeaLevel420.ToList().ForEach(c => c.SeaLevelDigitalInput += OnSeaIoDigitalInput <SeaLevel420>);

            SeaLevel410.ToList().ForEach(c => c.DigitalOutput.ForEach(n => n.DigitalOutputEvent += c.OnSeaLevelDigitalOutput));
            SeaLevel410.ToList().ForEach(c => c.SeaLevelDigitalOutput += OnSeaIoDigitalOutput<SeaLevel410>);


        }


        internal protected void OnSeaIoDigitalInput<T>(object sender, DigitalInput digitalInput) where T : class
        {
            var type = sender.GetType();
            if (type == typeof(SeaLevel420))
            {
                var seaio = (SeaLevel420)sender;
                var seaioIndex = SeaLevel420.IndexOf(seaio);
                var digitalinputindex = seaio.DigitalInput.IndexOf(digitalInput);
                DigitalInputEvent?.Invoke(seaio, seaioIndex, digitalinputindex, digitalInput);
            }
            else if (type == typeof(SeaLevel410))
            {
                var seaio410 = (SeaLevel410)sender;
                var seaio410Index = SeaLevel410.IndexOf(seaio410);
                //var digitalinputindex = seaio410.DigitalInput.IndexOf(digitalInput);
            }

        }

        internal protected void OnSeaIoDigitalOutput<T>(object sender, DigitalOutput digitalOutput) where T : class
        {
            var type = sender.GetType();
            if (type == typeof(SeaLevel420))
            {
                var seaio = (SeaLevel420)sender;
                var seaioIndex = SeaLevel420.IndexOf(seaio);
                var digitaloutputindex = seaio.DigitalOutput.IndexOf(digitalOutput);
                DigitalOutputEvent?.Invoke(seaio, seaioIndex, digitaloutputindex, digitalOutput);
            }
            else if (type == typeof(SeaLevel410))
            {
                var seaio = (SeaLevel410)sender;
                var seaioIndex = SeaLevel410.IndexOf(seaio);
                var digitaloutputindex = seaio.DigitalOutput.IndexOf(digitalOutput);
                DigitalOutputEvent?.Invoke(seaio, seaioIndex, digitaloutputindex, digitalOutput);
            }
        }

    }




}
