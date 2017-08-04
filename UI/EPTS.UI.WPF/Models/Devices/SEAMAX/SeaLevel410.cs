using System.Collections.Generic;
using DigitalOutput = EPTS.UI.WPF.Models.Devices.SEAMAX.Core.DigitalOutput;

namespace EPTS.UI.WPF.Models.Devices.SEAMAX
{
    public class SeaLevel410:SeaMax
    {

        public delegate void SeaLevelDigitalOutputHandler(object sender, DigitalOutput digitalOutput);
        public event SeaLevelDigitalOutputHandler SeaLevelDigitalOutput;

        public List<DigitalOutput> DigitalOutput { get; set; }


        internal protected virtual void OnSeaLevelDigitalOutput(DigitalOutput digitaloutput)
        {
            SeaLevelDigitalOutput?.Invoke(this, digitaloutput);
        }
    }
}