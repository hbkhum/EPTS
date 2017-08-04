using System.Collections.Generic;
using System.Xml.Serialization;
using EPTS.Repositories.XML.Devices.Infrastructure.Entities.VisaCom.Com.Scanner;
using EPTS.Repositories.XML.Devices.Infrastructure.Entities.VisaCom.DMM;
using EPTS.Repositories.XML.Devices.Infrastructure.Entities.VisaCom.Power;

namespace EPTS.Repositories.XML.Devices.Repositories
{
    [XmlRoot(ElementName = "VisaComDeviceRepository")]
    public class VisaComDeviceRepository
    {
        [XmlElement(ElementName = "Scanner")]
        public List<Scanner> Scanner { get; set; }
        [XmlElement(ElementName = "DMM")]
        public List<DMM> DMM { get; set; }
        [XmlElement(ElementName = "Power")]
        public List<Power> Power { get; set; }
    }

}