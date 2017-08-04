using System.Xml.Serialization;

namespace EPTS.Repositories.XML.Devices.Infrastructure.Entities.TCPIP
{
    [XmlRoot(ElementName = "Modbus-TCP")]
    public class ModbusTcp : Socket
    {
    }
}