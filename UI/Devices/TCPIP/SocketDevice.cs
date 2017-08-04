using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Devices.Core;

namespace Devices.TCPIP
{
    public abstract class SocketDevice : DeviceInformation
    {
        public string IpAddress { get; set; }
        public int  Port { get; set; }

        private TcpClient _server;

        public void Connect()
        {
            try
            {
                _server = new TcpClient {SendTimeout = 100};
                _server.Connect(IpAddress, Port);
            }
            catch (SocketException ex)
            {
                throw new Exception("The TCPIP: Unable to connect to server. " + ex.Message);
            }            
        }
        public void Close()
        {
            try
            {
                _server.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("The TCPIP Close :" + ex.Message);
            }
        }

        internal void WriteMessage(string message)
        {

            var serverStream = _server.GetStream();
            var outStream = Encoding.ASCII.GetBytes(message);
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();


        }

        internal string ReadMessage()
        {
            var serverStream = _server.GetStream();
            var inStream = new byte[1024];
            serverStream.Read(inStream, 0, _server.ReceiveBufferSize);
            var returndata = Encoding.ASCII.GetString(inStream);
            return returndata;
        }

    }
}
