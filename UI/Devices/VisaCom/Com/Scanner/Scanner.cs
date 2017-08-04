using System;
using Devices.Core;

namespace Devices.VisaCom.Com.Scanner
{
    public class Scanner:ComPort
    {
        public new string Trigger { get; set; }

        /// <summary>
        /// Scanner Serial
        /// </summary>
        /// <returns>Serial Number</returns>
        public string ScannerRead()
        {
            string data = "";
            int count = 0;
            try
            {
                do
                {
                    WriteCom(Trigger);
                    HandlerCpu.Delay(50);
                    data = ReadCommand();
                    count++;
                } while ((data == "") && (count < 3));
            }
            catch (Exception ex)
            {
                throw new Exception("command VisaCom Method. " + ex.Message);
            }
            return data;
        }

    }
}
