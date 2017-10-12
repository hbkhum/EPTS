using EPTS.UI.ViewModel;


namespace EPTS.UI.ViewModel.Models.Devices.SEAMAX.Core
{

    public class DigitalOutput : ViewModelBase
    {
        public delegate void DigitalOutputHandler(DigitalOutput digitalOutput);

        public event DigitalOutputHandler DigitalOutputEvent;

        //[AlsoNotifyFor("Description")]
        public string Description { get; set; }

        //[AlsoNotifyFor("Value")]
        public bool Value { get; set; } 

        // OnDigitalOutputEvent(this);
        protected virtual void OnDigitalOutputEvent(DigitalOutput digitaloutput)
        {
            DigitalOutputEvent?.Invoke(digitaloutput);
        }
    }
}