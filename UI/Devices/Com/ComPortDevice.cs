using System;
using System.IO.Ports;
using Devices.Core;

namespace Devices.Com
{
    public class ComPortDevice
    {
        public delegate void DataReceivedDelegate(string data);
        public event DataReceivedDelegate DataReceived;
        public string PortName { get; set; }
        public int BaudRate { get; set; }
        public Parity Parity { get; set; }
        public int DataBits { get; set; }
        public StopBits StopBits { get; set; }

        private SerialPort _serialPort;

        public virtual void Initialization()
        {
            HandlerError.ClearErrors();
            try
            {
                _serialPort = new SerialPort(PortName, BaudRate, Parity, DataBits, StopBits);
                _serialPort.DataReceived += DataReceivedHandler;
            }
            catch (Exception ex)
            {
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Open port. " + ex.Message;
                throw new Exception(HandlerError.ErrorMsg);
            }
        }
        public void Write(string message)
        {
            HandlerError.ClearErrors();
            try
            {
                _serialPort.Write(message);
            }
            catch (Exception ex)
            {
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Write port. " + ex.Message;
                throw new Exception(HandlerError.ErrorMsg);
            }
        }

        public void Open()
        {
            HandlerError.ClearErrors();
            try
            {
                if (_serialPort.IsOpen) throw new Exception("its Open the ports");
                _serialPort.Open();
            }
            catch (Exception ex)
            {
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Open port. " + ex.Message;
                throw new Exception(HandlerError.ErrorMsg);
            }
        }

        public void Close()
        {
            HandlerError.ClearErrors();
            try
            {
                _serialPort.Close();
            }
            catch (Exception ex)
            {
                HandlerError.ErrorOccurred = true;
                HandlerError.ErrorMsg = "Closed port. " + ex.Message;
                throw new Exception(HandlerError.ErrorMsg);
            }
        }
        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            var sp = (SerialPort)sender;
            var indata = sp.ReadExisting();
            if (DataReceived != null) DataReceived.Invoke(indata);
        }


    }
}
