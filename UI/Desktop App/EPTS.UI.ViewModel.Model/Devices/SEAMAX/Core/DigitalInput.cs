namespace EPTS.UI.ViewModel.Model.Devices.SEAMAX.Core
{
    public class DigitalInput : ViewModelBase
    {
        public delegate void DigitalInputHandler(DigitalInput digitalInput);

        public event DigitalInputHandler DigitalInputEvent;

        //[AlsoNotifyFor("Description")]
        public string Description { get; set; }

        //[AlsoNotifyFor("Value")]
        public bool Value { get; set; }

        protected virtual void OnDigitalInputEvent(DigitalInput digitalinput)
        {
            DigitalInputEvent?.Invoke(digitalinput);
        }
    }
}

