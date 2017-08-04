
using System.Xml.Serialization;
using EPTS.Repositories.XML.Devices.Infrastructure.Entities.Core;

namespace EPTS.Repositories.XML.Devices.Infrastructure.Entities.Com
{
    [XmlRoot(ElementName = "Com")]
    public class ComPort : DeviceInformation
    {
        [XmlElement(ElementName = "Com")]
        public string COM { get; set; }
        [XmlElement(ElementName = "BaudRate")]
        public int BaudRate { get; set; }
    }
}
