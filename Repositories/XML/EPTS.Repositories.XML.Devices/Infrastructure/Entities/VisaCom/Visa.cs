using System.Xml.Serialization;
using EPTS.Repositories.XML.Devices.Infrastructure.Entities.Core;

namespace EPTS.Repositories.XML.Devices.Infrastructure.Entities.VisaCom
{
    public class Visa : DeviceInformation
    {
        [XmlElement(ElementName = "DeviceAddress")]
        public string DeviceAddress { get; set; }
    }
}