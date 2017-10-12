using System;
using Devices.Core;
using Ivi.Visa.Interop;

namespace Devices.VisaCom
{
    public abstract class Visa : DeviceInformation 
    {
        
        public string DeviceAddress { get; set; }

        private ResourceManagerClass ResourceManager { get; set; }
        private FormattedIO488Class ResourceIO { get; set; }

        #region "Device Product"


        /// <summary>
        /// Device Agilent Initialization
        /// </summary>
        public virtual void Initialization()
        {
            HandlerError.ClearErrors();

            ResourceManager = new ResourceManagerClass();
            ResourceIO = new FormattedIO488Class();
            try
            {
                ResourceManager.Open(DeviceAddress, AccessMode.NO_LOCK, 0, "");
                ResourceIO.IO = (IMessage)ResourceManager.Open(DeviceAddress, AccessMode.NO_LOCK, 0, "");
            }
            catch (Exception e)
            {
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Init resource Agilent Method. " + e.Message;
                throw new Exception(HandlerError.ErrorMsg);
            }
        }

        /// <summary>
        /// COM Setting
        /// </summary>
        internal void ResourceIO_setting(string baudRate,string timeOut)
        {
            var optionString = "Timeout = 200 ; SendEndEnabled = TRUE ; TerminationCharacter = 10 ; TerminationCharacterEnabled = FALSE ; BaudRate = " + baudRate + "; DataBits = 8 ; EndIn = ASRL_END_TERMCHAR ; EndOut = ASRL_END_NONE ; FlowControl = ASRL_FLOW_NONE ; Parity = ASRL_PAR_NONE ; RequestToSendState = 1 ; DataTerminalReadyState = 1 ; StopBits = ASRL_STOP_ONE ; MaximumQueueLength = 1000 ; ReplacementCharacter = 255 ; XONCharacter = 17 ; XOFFCharacter = 19";
            try
            {
                ResourceIO.IO = (IMessage)ResourceManager.Open(DeviceAddress,AccessMode.NO_LOCK, 0, "Timeout = 2000 ; SendEndEnabled = TRUE ; TerminationCharacter = 10 ; TerminationCharacterEnabled = FALSE ; BaudRate = 9600 ; DataBits = 8 ; EndIn = ASRL_END_TERMCHAR ; EndOut = ASRL_END_NONE ; FlowControl = ASRL_FLOW_NONE ; Parity = ASRL_PAR_NONE ; RequestToSendState = 1 ; DataTerminalReadyState = 1 ; StopBits = ASRL_STOP_ONE ; MaximumQueueLength = 1000 ; ReplacementCharacter = 255 ; XONCharacter = 17 ; XOFFCharacter = 19");
                ResourceIO.IO = (IMessage)ResourceManager.Open(ResourceIO.IO.ResourceName, AccessMode.NO_LOCK, 0, optionString);
                ResourceIO.IO.Timeout = Int32.Parse(timeOut);
                ResourceIO.IO.TerminationCharacter = 13;
                ResourceIO.IO.TerminationCharacterEnabled = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Init resource method. " + ex.Message);
            }
        }

        /// <summary>
        /// The Close All Connection to Device
        /// </summary>
        protected  void Close()
        {
            throw new NotImplementedException();
        }



        #endregion

        #region "GENERAL VISA"


        /// <summary>
        /// Write/Send Command to Device
        /// </summary>
        /// <param name="command">Is a Query</param>
        protected  void WriteCommand(string command)
        {
            try
            {
                ResourceIO.WriteString(command, true);
            }
            catch (Exception ex)
            {
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Write command VisaCom Method. " + ex.Message;
                throw new Exception(HandlerError.ErrorMsg);
            }
        }

        /// <summary>
        /// Read values
        /// </summary>
        /// <param name="buffer">buffer is variable to show result</param>
        protected void ReadCommand(out double buffer)
        {
            HandlerError.ClearErrors();
            try
            {
                var tmpbuffer = ResourceIO.ReadString();
                double dbuffer;
                double.TryParse(tmpbuffer, out dbuffer);
                buffer = 0;//buffer = dbuffer;
                HandlerCpu.Delay(1);
            }
            catch (Exception ex)
            {
                buffer = 0;
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Read command VisaCom Method. " + ex.Message;
                throw new Exception(HandlerError.ErrorMsg);
            }
        }

        /// <summary>
        /// Read Command
        /// </summary>
        /// <returns>Answer from Device</returns>
        protected string ReadCommand()
        {
            HandlerError.ClearErrors();
            var tmpbuffer = "";
            ResourceIO.FlushRead();
            try
            {
                tmpbuffer = ResourceIO.ReadString();

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("timeout")) return tmpbuffer;
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Read command VisaCom Method. " + ex.Message;
                throw new Exception(HandlerError.ErrorMsg);
            }
            return tmpbuffer;
        }

        /// <summary>
        /// Clean the Device Buffer 
        /// </summary>
        public void Flush()
        {
            HandlerError.ClearErrors();
            try
            {
                ResourceIO.FlushWrite(false);
                ResourceIO.IO.Clear();
            }
            catch (Exception ex)
            {
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Flush command VisaCom Method. " + ex.Message;
                throw new Exception(HandlerError.ErrorMsg);
            }
        }

        /// <summary>
        /// Identity Device
        /// </summary>
        public void FindDevice()
        {
            HandlerError.ClearErrors();
            try
            {
                ResourceIO.WriteString("*IDN?", true);
            }
            catch (Exception ex)
            {
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Find Device command VisaCom Method. " + ex.Message;
                throw new Exception(HandlerError.ErrorMsg);
            }
        }

        /// <summary>
        /// Poll
        /// </summary>
        public void Poll()
        {
            HandlerError.ClearErrors();
            try
            {
                ResourceIO.WriteString("*STB?", true);
            }
            catch (Exception ex)
            {
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Find Device command VisaCom Method. " + ex.Message;
                throw new Exception(HandlerError.ErrorMsg);
            }
        }

        /// <summary>
        /// Close Communication with Device
        /// </summary>
        public void CloseDevice()
        {
            HandlerError.ClearErrors();
            try
            {
                ResourceIO.IO.Close();
            }
            catch (Exception ex)
            {
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Close Device command VisaCom Method. " + ex.Message;
                throw new Exception(HandlerError.ErrorMsg);
            }
        }

        /// <summary>
        /// Clear Device
        /// </summary>
        public void ClearDevice()
        {
            HandlerError.ClearErrors();
            try
            {
                ResourceIO.IO.Clear();
            }
            catch (Exception ex)
            {
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Clear Device command VisaCom Method. " + ex.Message;
                throw new Exception(HandlerError.ErrorMsg);
            }
        }

        /// <summary>
        /// Triger Device
        /// </summary>
        public void Trigger()
        {
            HandlerError.ClearErrors();
            try
            {
                ResourceIO.IO.AssertTrigger(Ivi.Visa.Interop.TriggerProtocol.TRIG_PROT_ON);
                HandlerCpu.Delay(1);
            }
            catch (Exception ex)
            {
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Trigger command VisaCom Method. " + ex.Message;
                throw new Exception(HandlerError.ErrorMsg);
            }
        }

        /// <summary>
        /// Clear All Error
        /// </summary>
        public void Clear()
        {
            HandlerError.ClearErrors();
            try
            {
                ResourceIO.WriteString("*CLS", true);

            }
            catch (Exception ex)
            {
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Clear Device command VisaCom Method. " + ex.Message;
                throw new Exception(HandlerError.ErrorMsg);
            }
        }

        /// <summary>
        /// Abort Device
        /// </summary>
        public void Abort()
        {
            HandlerError.ClearErrors();
            try
            {
                ResourceIO.WriteString("ABOR", true);
            }
            catch (Exception ex)
            {
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Abort command VisaCom Method. " + ex.Message;
                throw new Exception(HandlerError.ErrorMsg);
            }
        }

        /// <summary>
        /// Reset Device
        /// </summary>
        public void Reset()
        {
            HandlerError.ClearErrors();
            try
            {
                WriteCommand("*RST");
            }
            catch (Exception ex)
            {
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Reset command VisaCom Method. " + ex.Message;
                throw new Exception(HandlerError.ErrorMsg);
            }
        }

        #endregion


        ~Visa()
        {
            Dispose(false);
        }
    }

}
