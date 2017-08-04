using System.Collections.Generic;
using System.Xml.Serialization;
using EPTS.Repositories.XML.Devices.Infrastructure.Entities.TCPIP;

namespace EPTS.Repositories.XML.Devices.Repositories
{
    [XmlRoot(ElementName = "SocketDeviceRepository")]
    public class SocketDeviceRepository
    {
        [XmlElement(ElementName = "CamLine")]
        public List<CamLine> CamLine { get; set; }
        [XmlElement(ElementName = "Robot")]
        public List<Robot> Robot { get; set; }
        [XmlElement(ElementName = "Modbus-TCP")]
        public List<ModbusTcp> ModbusTcp { get; set; }
    }
}