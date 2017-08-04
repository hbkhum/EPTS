using System.Collections.Generic;
using Devices.VisaCom.Com.Scanner;
using Devices.VisaCom.DMM;
using Devices.VisaCom.Power;

namespace Devices.Repositories
{
    public interface IVisaComDeviceRepository
    {
        List<Scanner> VisaCom { get; set; }
        List<Power3645A> Power3645A { get; set; }
        List<Multimeter34401A> Multimeter34401A { get; set; }
    }
    public class VisaComDeviceRepository: IVisaComDeviceRepository
    {
        public List<Scanner> VisaCom { get; set; }
        public List<Power3645A> Power3645A { get; set; }
        public List<Multimeter34401A> Multimeter34401A { get; set; }
    }
}
