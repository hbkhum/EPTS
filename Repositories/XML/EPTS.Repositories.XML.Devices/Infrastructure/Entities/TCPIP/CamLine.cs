using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EPTS.Repositories.XML.Devices.Infrastructure.Entities.TCPIP
{
    [XmlRoot(ElementName = "CamLine")]
    public class CamLine:Socket
    {
    }
}
