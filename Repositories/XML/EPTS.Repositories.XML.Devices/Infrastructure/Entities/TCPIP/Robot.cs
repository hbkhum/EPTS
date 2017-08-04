using System.Xml.Serialization;

namespace EPTS.Repositories.XML.Devices.Infrastructure.Entities.TCPIP
{
    [XmlRoot(ElementName = "Robot")]
    public class Robot : Socket
    {
    }
}