using System.Collections.Generic;
using System.Xml.Serialization;

namespace Devices.SEAMAX.Core
{

    public class DigitalInputDevice
    {
        public delegate void DigitalOutputHandler(DigitalInputDevice digitalInputDevice);
        public event DigitalOutputHandler DigitalInputEvent;

        public string Description { get; set; }

        private bool _value;
        public bool Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnDigitalInputEvent(this);
            }
        }


        public void OnDigitalInputEvent(DigitalInputDevice digitalinputdevice)
        {
            if (DigitalInputEvent != null) DigitalInputEvent.Invoke(digitalinputdevice);
        }
    }
}

