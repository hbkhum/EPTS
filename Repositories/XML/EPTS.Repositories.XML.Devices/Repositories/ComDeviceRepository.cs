using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using EPTS.Repositories.XML.Devices.Infrastructure.Entities.Com;
using EPTS.Repositories.XML.Devices.Infrastructure.Entities.Com.Modbus;

namespace EPTS.Repositories.XML.Devices.Repositories
{
    [XmlRoot(ElementName = "ComDeviceRepository")]
    public class ComDeviceRepository
    {
        [XmlElement(ElementName = "Com")]
        public List<ComPort> Com { get; set; }

        [XmlElement(ElementName = "Modbus-Serial")]
        public List<Modbus> Modbus { get; set; }
    }
}
