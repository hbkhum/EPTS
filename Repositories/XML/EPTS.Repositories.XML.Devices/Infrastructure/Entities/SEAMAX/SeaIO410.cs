using System.Collections.Generic;
using System.Xml.Serialization;
using EPTS.Repositories.XML.Devices.Infrastructure.Entities.SEAMAX.Core;

namespace EPTS.Repositories.XML.Devices.Infrastructure.Entities.SEAMAX
{
    [XmlRoot(ElementName = "SeaIO410")]
    public class SeaIO410:SeaMax
    {

        [XmlElement(ElementName = "DigitalOutput")]
        public List<DigitalOutput> DigitalOutput { get; set; }
    }

}