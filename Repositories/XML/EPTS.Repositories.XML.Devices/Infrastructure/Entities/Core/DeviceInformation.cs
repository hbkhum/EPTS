using System.Xml.Serialization;

namespace EPTS.Repositories.XML.Devices.Infrastructure.Entities.Core
{
    public abstract class DeviceInformation
    {
        [XmlElement(ElementName = "DeviceId")]
        public string DeviceId { get; set; }
        [XmlElement(ElementName = "DeviceName")]
        public string DeviceName { get; set; }
        [XmlElement(ElementName = "DeviceDescription")]
        public string DeviceDescription { get; set; }
    }
}