

using System;
using System.Collections.Generic;

namespace Devices.SEAMAX.Core
{

    public  class DigitalOutputDevice 
    {

        public delegate void DigitalOutputHandler(DigitalOutputDevice digitalOutputDevice);
        public event DigitalOutputHandler DigitalOutputEvent;
        
        public string Description { get; set; }
        
        private bool _value;
        public bool Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnDigitalOutputEvent(this);
            }
        }

        public void OnDigitalOutputEvent(DigitalOutputDevice digitalOutputDevice)
        {
            if (DigitalOutputEvent != null) DigitalOutputEvent.Invoke(digitalOutputDevice);
        }
    }
}