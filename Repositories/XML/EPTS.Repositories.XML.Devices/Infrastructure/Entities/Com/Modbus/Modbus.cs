using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EPTS.Repositories.XML.Devices.Infrastructure.Entities.Com.Modbus
{
    [XmlRoot(ElementName = "Modbus-Serial")]
    public class Modbus: ComPort
    {
    }
}
