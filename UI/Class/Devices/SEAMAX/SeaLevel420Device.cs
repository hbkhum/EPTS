using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Devices.Core;
using Devices.SEAMAX.Core;

namespace Devices.SEAMAX
{
    public class SeaLevel420Device:SeaMaxDevice
    {
        public delegate void SeaLevelDigitalOutputHandler(object sender,DigitalOutputDevice digitalOutput);
        public event SeaLevelDigitalOutputHandler SeaLevelDigitalOutput;

        public delegate void SeaLevelDigitalInputHandler(object sender, DigitalInputDevice digitalInput);
        public event SeaLevelDigitalInputHandler SeaLevelDigitalInput;

        public List<DigitalOutputDevice> DigitalOutput { get; set; }
        public List<DigitalInputDevice> DigitalInput { get; set; }

        private bool _monitor;
        public bool Monitor
        {
            get
            {
                return _monitor;
            }
            set
            {
                _monitor = value;
                if (value)
                {
                    _seaIo420Thread.Start();
                }
                else
                {
                    _seaIo420Thread.Abort();
                }
            }
        }
        private Thread _seaIo420Thread;
        public override void Initialization()
        {
            base.Initialization();
            _seaIo420Thread= new Thread(MonitorThread);
            //DigitalOutput.ForEach(n => n.DigitalOutputEvent += OnDigitalOutputEvent);

        }

        public void OnSeaLevelDigitalOutput(DigitalOutputDevice digitalOutput)
        {
            var outputs = BitConverter.GetBytes(Convert.ToInt32(string.Join("", DigitalOutput.Select(c => Convert.ToInt32(c.Value))), 2));
            SM_WriteDigitalOutputs(0, DigitalOutput.Count, outputs);
            if (SeaLevelDigitalOutput != null) SeaLevelDigitalOutput.Invoke(this, digitalOutput);
        }

        public virtual void OnSeaLevelDigitalInput(DigitalInputDevice digitalinput)
        {
            if (SeaLevelDigitalInput != null) SeaLevelDigitalInput.Invoke(this, digitalinput);
        }



        protected override void SM_ClearDigitalOutputs()
        {
            HandlerError.ClearErrors();
            var data = new byte[4];
            try
            {
                //HandlerError.ErrorCode = SeaMaxDeviceHandler.SM_WriteDigitalOutputs(0, DigitalOutput.Count, data);
                if (HandlerError.ErrorCode >= 0) return;
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Write Digital Outputs Returned Error Code: " + HandlerError.ErrorCode + "";
                throw new Exception(HandlerError.ErrorMsg);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private  void MonitorThread()
        {
            do
            {
                var outputs = Convert.ToString(SM_ReadDigitalOutputs(0, DigitalOutput.Count).Sum(b => b), 2)
                    .PadLeft(DigitalOutput.Count, '0')
                    .Select(c => c.ToString()).ToList().Select(c => Convert.ToBoolean(Convert.ToInt32(c))).ToList();
                DigitalOutput.ForEach(n => outputs.ForEach(m => n.Value=m));
                //for (var j = 0; j < DigitalOutput.Count; j++)
                //{
                //    DigitalOutput[j].Value = outputs[j];
                //}
                HandlerCpu.Delay(100);
                
                var inputs = Convert.ToString(SM_ReadDigitalInputs(0, DigitalInput.Count).Sum(b => b), 2)
                    .PadLeft(DigitalInput.Count, '0')
                    .Select(c => c.ToString()).ToList().Select(c => Convert.ToBoolean(Convert.ToInt32(c))).ToList();
                DigitalInput.ForEach(n => inputs.ForEach(m => n.Value = m));
                //for (var j = 0; j < DigitalInput.Count; j++)
                //{
                //    DigitalInput[j].Value = inputs[j];
                //}

            } while (Monitor);
        }


    }
}
