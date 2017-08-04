using System;
using System.Globalization;
using Devices.Core;
using Devices.VisaCom.Core;

namespace Devices.VisaCom.DMM
{
    public class Multimeter34401A : Visa
    {
        public delegate void MeasureHandler(object sender, string measure);
        public event MeasureHandler MeasureEvent;

        public delegate void MeasureTypeHandler(object sender, string measuretype);
        public event MeasureTypeHandler MeasureTypeEvent;
        public void SetMeasure(MeasType command)
        {
            HandlerError.ClearErrors();
            try
            {
                WriteCommand(MeasureType.SetMeasure(command));   
            }
            catch (Exception e)
            {
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Data Adquisition Agilent 34401A. " + e.Message;
                throw new Exception(HandlerError.ErrorMsg);
            }
        }
        public void SetSenSe(MeasType command)
        {
            HandlerError.ClearErrors();
            
            try
            {
                WriteCommand(MeasureType.SetSenSe(command));
            }
            catch (Exception e)
            {
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Data Adquisition Agilent 34401A. " + e.Message;
                throw new Exception(HandlerError.ErrorMsg);
            }
        }
        public double GetMeasure(MeasType command)
        {
            HandlerError.ClearErrors();
            double measures = 0;
            try
            {
                var commandstring = MeasureType.GetMeasure(command);
                WriteCommand(commandstring);
                OnMeasureTypeEvent(commandstring);
                ReadCommand(out measures);
                OnMeasureEvent(Convert.ToString(measures, CultureInfo.InvariantCulture));
            }
            catch (Exception e)
            {
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Data Adquisition Agilent 34401A. " + e.Message;
                throw new Exception(HandlerError.ErrorMsg);
            }
            return measures;
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


        protected virtual void OnMeasureTypeEvent(string measuretype)
        {
            MeasureTypeEvent?.Invoke(this,measuretype);
        }

        protected virtual void OnMeasureEvent(string measure)
        {
            MeasureEvent?.Invoke(this,measure);
        }
    }
}
