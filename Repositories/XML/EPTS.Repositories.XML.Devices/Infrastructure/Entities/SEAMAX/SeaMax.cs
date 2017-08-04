using System.Xml.Serialization;
using EPTS.Repositories.XML.Devices.Infrastructure.Entities.Core;

namespace EPTS.Repositories.XML.Devices.Infrastructure.Entities.SEAMAX
{
    public abstract class SeaMax : DeviceInformation
    {
        [XmlElement(ElementName = "COM")]
        public string COM { get; set; }
        [XmlElement(ElementName = "SlaveId")]
        public int SlaveId { get; set; }

    }
}