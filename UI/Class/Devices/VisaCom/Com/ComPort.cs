using System;
using Devices.Core;

namespace Devices.VisaCom.Com
{
    public abstract class ComPort:Visa
    {
        public string TimeOut { get; set; }
        public string BaudRate { get; set; }

        /// <summary>
        /// The Device Initialization 
        /// </summary>
        public override void Initialization()
        {
            try
            {
                base.Initialization();
                base.ResourceIO_setting(BaudRate, TimeOut);
            }
            catch (Exception ex)
            {
                throw new Exception("Command VisaCom Method. " + ex.Message);
            }
        }



        /// <summary>
        /// Write to COM
        /// </summary>
        /// <param name="data">data</param>
        protected void WriteCom(string data)
        {
            HandlerError.ClearErrors();
            try
            {
                WriteCommand(data);
            }
            catch (Exception ex)
            {
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Write COM resource method. " + ex.Message;
                throw new Exception(HandlerError.ErrorMsg);
            }
        }
        /// <summary>
        /// Read COM
        /// </summary>
        /// <returns>Get COM</returns>
        protected string ReadCom()
        {
            HandlerError.ClearErrors();
            var buffer = "";
            try
            {
                buffer = ReadCommand();
            }
            catch (Exception ex)
            {
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Read COM resource method. " + ex.Message;
                throw new Exception(HandlerError.ErrorMsg);
            }
            return buffer;
        }

    }
}
