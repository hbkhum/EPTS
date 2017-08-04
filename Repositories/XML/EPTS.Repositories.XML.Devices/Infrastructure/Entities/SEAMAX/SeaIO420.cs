using System.Collections.Generic;
using System.Xml.Serialization;
using EPTS.Repositories.XML.Devices.Infrastructure.Entities.SEAMAX.Core;

namespace EPTS.Repositories.XML.Devices.Infrastructure.Entities.SEAMAX
{
    [XmlRoot(ElementName = "SeaIO420")]
    public class SeaIO420:SeaMax
    {
        [XmlElement(ElementName = "DigitalInput")]
        public List<DigitalInput> DigitalInput { get; set; }
        [XmlElement(ElementName = "DigitalOutput")]
        public List<DigitalOutput> DigitalOutput { get; set; }
    }
}