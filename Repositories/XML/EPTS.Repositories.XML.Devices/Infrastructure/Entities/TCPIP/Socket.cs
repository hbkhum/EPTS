using System.Xml.Serialization;
using EPTS.Repositories.XML.Devices.Infrastructure.Entities.Core;

namespace EPTS.Repositories.XML.Devices.Infrastructure.Entities.TCPIP
{
    public class Socket : DeviceInformation
    {
        [XmlElement(ElementName = "IpAddress")]
        public string IpAddress { get; set; }
        [XmlElement(ElementName = "Port")]
        public int Port { get; set; }
    }
}