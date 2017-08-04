using System;
using Devices.Core;
using Devices.VisaCom.Core;

namespace Devices.VisaCom.Dasu
{
    public class Dasu34970A:Visa
    {

        public void UnLatchInputs(string channels)
        {
            HandlerError.ClearErrors();
            try
            {
                OpenChannel(channels);
            }
            catch (Exception e)
            {
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Data Adquisition Agilent 34970A. " + e.Message;
                throw new Exception(HandlerError.ErrorMsg);
            }
        }
        public void Unlatch(string channels)
        {
            HandlerError.ClearErrors();
            try
            {
                OpenChannel(channels);
            }
            catch (Exception e)
            {
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Data Adquisition Agilent 34970A. " + e.Message;
                throw new Exception(HandlerError.ErrorMsg);
            }
        }
        public void Latch(string commands)
        {
            HandlerError.ClearErrors();
            try
            {
                CloseChannel(commands);

            }
            catch (Exception e)
            {
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Data Adquisition Agilent 34970A. " + e.Message;
                throw new Exception(HandlerError.ErrorMsg);
            }
        }
        public void Single_Cycle_Latch(string commands)
        {
            HandlerError.ClearErrors();
            try
            {
                CloseChannel(commands);
                HandlerCpu.Delay(500); 
                OpenChannel(commands);

            }
            catch (Exception e)
            {
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Data Adquisition Agilent 34970A. " + e.Message;
                throw new Exception(HandlerError.ErrorMsg);
            }
        }
        public void Looping(string commands, int cicles)
        {
            HandlerError.ClearErrors();
            int loopCounter = 1;
            try
            {
                do
                {
                    CloseChannel(commands);
                    HandlerCpu.Delay(500);
                    OpenChannel(commands);
                    loopCounter = loopCounter + 1;
                } while (cicles > loopCounter);

            }
            catch (Exception e)
            {
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Data Adquisition Agilent 34970A. " + e.Message;
                throw new Exception(HandlerError.ErrorMsg);
            }
        }


        public void SetMeasure(MeasType command, string channel)
        {
            HandlerError.ClearErrors();
            try
            {
                WriteCommand(MeasureType.SetMeasure(command) + " (@" + channel + ")");
            }
            catch (Exception e)
            {
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Data Adquisition Agilent 34970A. " + e.Message;
                throw new Exception(HandlerError.ErrorMsg);
            }
        }
        public void SetSenSe(MeasType command, string channel)
        {
            HandlerError.ClearErrors();
            try
            {
                WriteCommand(MeasureType.SetSenSe(command) + " (@" + channel + ")");
            }
            catch (Exception e)
            {
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Data Adquisition Agilent 34970A. " + e.Message;
                throw new Exception(HandlerError.ErrorMsg);
            }
        }
        public double GetMeasure(MeasType command, int channel)
        {
            HandlerError.ClearErrors();
            double measures = 0;
            try
            {
                WriteCommand(MeasureType.GetMeasure(command) + " (@" + channel + ")");
                ReadCommand(out measures);
                
            }
            catch (Exception e)
            {
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Data Adquisition Agilent 34970A. " + e.Message;
                throw new Exception(HandlerError.ErrorMsg);
            }
            return measures;
        }
        public double Fetch()
        {
            HandlerError.ClearErrors();
            try
            {
                WriteCommand("INIT");
                WriteCommand("FETCh?");
                double measure = 0;
                ReadCommand(out measure);
                return measure;
            }
            catch (Exception e)
            {
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Data Adquisition Agilent 34970A. " + e.Message;
                throw new Exception(HandlerError.ErrorMsg);
            }
        }
        public double[] FetchM()
        {
            HandlerError.ClearErrors();
            double[] measure = { 0 };
            try
            {
                WriteCommand("INIT");
                WriteCommand("FETCh?");
                //ReadCommand(out measure);
                
            }
            catch (Exception e)
            {
                measure.SetValue(0, 0);
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Data Adquisition Agilent 34970A. " + e.Message;
                throw new Exception(HandlerError.ErrorMsg);
            }
            return measure;
        }
        public double GetLowFreq(string channel)
        {
            HandlerError.ClearErrors();
            double measures = 0;
            try
            {
                //GPIBVisa.WriteCommand("SENS:DIG:DATA:BYTE? (@" + Channel + ")");
                WriteCommand("CONF:FREQ  (@" + channel + ")");
                WriteCommand("SENS:FREQ:RANG:LOW 3,  (@" + channel + ")");
                WriteCommand("INIT");
                WriteCommand("FETCh?");

                //System.Threading.Thread.Sleep(2000);
                ReadCommand(out measures);
                //return measures;
            }
            catch (Exception e)
            {
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Data Adquisition Agilent 34970A. " + e.Message;
                throw new Exception(HandlerError.ErrorMsg);
            }
            return measures;
        }
        public void SetDelay(string channel, double dDelay)
        {
            HandlerError.ClearErrors();
            var val = "";
            dDelay.ToString(val);
            try
            {
                WriteCommand("ROUT:CHAN:DEL " + val + ", (@" + channel + ")");
            }
            catch (Exception e)
            {
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Data Adquisition Agilent 34970A. " + e.Message;
                throw new Exception(HandlerError.ErrorMsg);
            }
        }
        public void Scan(string channel)
        {
            HandlerError.ClearErrors();

            try
            {
                WriteCommand("ROUT:SCAN (@" + channel + ")");
            }
            catch (Exception e)
            {
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Data Adquisition Agilent 34970A. " + e.Message;
                throw new Exception(HandlerError.ErrorMsg);
            }
        }
        public void ClearScan()
        {
            HandlerError.ClearErrors();
            try
            {
                WriteCommand("ROUT:SCAN (@)");
            }
            catch (Exception e)
            {
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Data Adquisition Agilent 34970A. " + e.Message;
                throw new Exception(HandlerError.ErrorMsg);
            }
        }
 

        public void InputImpedance(InputType input, string channel)
        {
            HandlerError.ClearErrors();
            try
            {
                switch (input)
                {
                    case InputType.TurnOn:
                        WriteCommand("INPUT:IMPEDANCE:AUTO ON, (@" + channel + ")");
                        break;
                    case InputType.TurnOff:
                        WriteCommand("INPUT:IMPEDANCE:AUTO OFF, (@" + channel + ")");
                        break;
                }
            }
            catch (Exception e)
            {
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "command Agilent Method. " + e.Message;
                throw new Exception(HandlerError.ErrorMsg);
            }
        }
        public void FrequencyFilter(int filter, string channel)
        {
            HandlerError.ClearErrors();
            try
            {
                switch (filter)
                {
                    case 0:
                        WriteCommand("SENS:VOLT:AC:BAND 3, (@" + channel + ")");
                        break;
                    case 1:
                        WriteCommand("SENS:VOLT:AC:BAND 20, (@" + channel + ")");
                        break;
                    case 2:
                        WriteCommand("SENS:VOLT:AC:BAND 200, (@" + channel + ")");
                        break;
                }
            }
            catch (Exception e)
            {
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "command Agilent Method. " + e.Message;
                throw new Exception(HandlerError.ErrorMsg);
            }
        }

        private void CloseChannel(string channel)
        {
            HandlerError.ClearErrors();

            try
            {

                WriteCommand("ROUT:CLOS (@" + channel + ")");
            }
            catch (Exception e)
            {
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Data Adquisition Agilent 34970A. " + e.Message;
                throw new Exception(HandlerError.ErrorMsg);
            }
        }
        private void OpenChannel(string channel)
        {
            HandlerError.ClearErrors();

            try
            {
                WriteCommand("ROUT:OPEN (@" + channel + ")");
            }
            catch (Exception e)
            {
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Data Adquisition Agilent 34970A. " + e.Message;
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

    }
}
