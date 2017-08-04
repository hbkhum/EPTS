using System;
using System.Globalization;
using Devices.Core;
using Devices.VisaCom.Core;

namespace Devices.VisaCom.Power
{
    public class Power3645A : Visa
    {
        public delegate void VolatageHandler(object sender, string voltage);
        public event VolatageHandler VoltageEvent;

        public delegate void CurrentHandler(object sender, string current);
        public event CurrentHandler CurrentEvent;
        /// <summary>
        /// Get Voltage
        /// </summary>
        /// <returns>Get Measure Voltage</returns>
        public double GetVoltage()
        {

            HandlerError.ClearErrors();
            double measure = 0;
            try
            {
                WriteCommand(MeasureType.GetMeasure(MeasType.VoltDc));
                ReadCommand(out measure);
                OnVoltageEvent(measure.ToString(CultureInfo.InvariantCulture));
            }
            catch (Exception e)
            {
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Get Voltage command Agilent Method. " + e.Message;
                throw new Exception(HandlerError.ErrorMsg);
            }
            return measure;
        }
        /// <summary>
        /// Set the voltage
        /// </summary>
        /// <param name="sValue">Value voltage</param>
        public void SetVoltage(string sValue)
        {
            HandlerError.ClearErrors();
            try
            {
                WriteCommand("VOLT " + sValue);
            }
            catch (Exception e)
            {
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Set Voltage command Agilent Method. " + e.Message;
                throw new Exception(HandlerError.ErrorMsg);
            }
        }

        public void SetCurrent(string sValue)
        {
            HandlerError.ClearErrors();
            try
            {
                WriteCommand("CURR " + sValue);
            }
            catch (Exception e)
            {
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Set Current command Agilent Method. " + e.Message;
                throw new Exception(HandlerError.ErrorMsg);
            }
        }
        public double GetCurrent()
        {
            HandlerError.ClearErrors();
            double measure = 0;
            try
            {
                WriteCommand("MEAS:CURR?");
                ReadCommand(out measure);
                OnCurrentEvent(measure.ToString(CultureInfo.InvariantCulture));
            }
            catch (Exception e)
            {
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Get Current command Agilent Method. " + e.Message;
                throw new Exception(HandlerError.ErrorMsg);
            }
            return measure;
        }

        public void EnableOutput(InputType onOff)
        {
            HandlerError.ClearErrors();
            try
            {
                WriteCommand(MeasureType.EnableOutput(onOff));
            }
            catch (Exception e)
            {
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Enabled OutPut command Agilent Method. " + e.Message;
                throw new Exception(HandlerError.ErrorMsg);
            }
        }
        
        protected override void Dispose(bool diposing)
        {
            if (!IsDisposed)
            {
                if (diposing)
                {
                    //Clean Up managed resources
                }
                //Clean up unmanaged resources
            }
            IsDisposed = true;
        }

        protected virtual void OnVoltageEvent(string voltage)
        {
            VoltageEvent?.Invoke(this, voltage);
        }

        protected virtual void OnCurrentEvent(string current)
        {
            CurrentEvent?.Invoke(this, current);
        }
    }
}
