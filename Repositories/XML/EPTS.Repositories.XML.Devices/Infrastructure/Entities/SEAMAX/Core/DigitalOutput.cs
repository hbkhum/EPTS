using System.Collections.Generic;
using System.Xml.Serialization;

namespace EPTS.Repositories.XML.Devices.Infrastructure.Entities.SEAMAX.Core
{
    [XmlRoot(ElementName = "DigitalOutput")]
    public class DigitalOutput
    {
        [XmlElement(ElementName = "Description")]
        public string Description { get; set; }
    }

}