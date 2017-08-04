using System;
using Devices.Core;
using Sealevel;

namespace Devices.SEAMAX
{
    public abstract class SeaMaxDevice:DeviceInformation
    {

        public string Com { get; set; }
        public int SlaveId { get; set; }
        internal SeaMAX SeaMaxDeviceHandler;

        public virtual void Initialization()
        {
            HandlerError.ClearErrors();
            try
            {
                //SeaMaxDeviceHandler = new SeaMAX();
            }
            catch (Exception e)
            {
                throw new Exception("SEA LEVEL Error: " + e.Message);
            }
        }

        public void SM_Open()
        {
            HandlerError.ClearErrors();
            try
            {

                HandlerError.ErrorCode = SeaMaxDeviceHandler.SM_Open(Com);
                if (HandlerError.ErrorCode < 0)
                {
                    HandlerError.ErrorOccurred = true;
                    HandlerError.ErrorMsg = "Open Returned Error Code: " + HandlerError.ErrorCode + ", when try to Open Port";
                    throw new Exception(HandlerError.ErrorMsg);

                }
                SeaMaxDeviceHandler.SM_SelectDevice(SlaveId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void SM_Close()
        {
            HandlerError.ClearErrors();
            try
            {
                SeaMaxDeviceHandler.SM_Close();
            }
            catch (Exception e)
            {
                HandlerError.ErrorCode = -1;
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Close Returned Error Code: " + HandlerError.ErrorCode + ", when try to Open Port";
                throw new Exception(e.Message);
            }
        }

        protected void SM_WriteDigitalOutputs(int start, int quantity, byte[] data)
        {
            HandlerError.ClearErrors();
            try
            {
                //HandlerError.ErrorCode = SeaMaxDeviceHandler.SM_WriteDigitalOutputs(start, quantity, data);
                if (HandlerError.ErrorCode >= 0) return ;
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Write Digital Outputs Returned Error Code: " + HandlerError.ErrorCode + "";
                throw new Exception(HandlerError.ErrorMsg);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        protected byte[] SM_ReadDigitalOutputs(int start, int quantity)
        {
            HandlerError.ClearErrors();
            var data = new byte[2];
            try
            {

                //HandlerError.ErrorCode = SeaMaxDeviceHandler.SM_ReadDigitalOutputs(start, quantity, data);
                if (HandlerError.ErrorCode >= 0) return data;
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Read Digital Inputs Returned Error Code: " + HandlerError.ErrorCode + "";
                throw new Exception(HandlerError.ErrorMsg);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        protected byte[] SM_ReadDigitalInputs(int start, int quantity)
        {
            HandlerError.ClearErrors();
            var data = new byte[2];
            try
            {

                //HandlerError.ErrorCode = SeaMaxDeviceHandler.SM_ReadDigitalInputs(start, quantity, data);
                if (HandlerError.ErrorCode >= 0) return data;
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Read Digital Inputs Returned Error Code: " + HandlerError.ErrorCode + "";
                throw new Exception(HandlerError.ErrorMsg);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        protected abstract void SM_ClearDigitalOutputs();
        ~SeaMaxDevice()
        {
            Dispose(false);
        }
    }
}
