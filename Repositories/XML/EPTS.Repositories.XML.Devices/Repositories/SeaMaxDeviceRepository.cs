using System.Collections.Generic;
using System.Xml.Serialization;
using EPTS.Repositories.XML.Devices.Infrastructure.Entities.SEAMAX;

namespace EPTS.Repositories.XML.Devices.Repositories
{
    [XmlRoot(ElementName = "SeaMaxDeviceRepository")]
    public class SeaMaxDeviceRepository
    {
        [XmlElement(ElementName = "SeaIO420")]
        public List<SeaIO420> SeaIO420 { get; set; }
        [XmlElement(ElementName = "SeaIO410")]
        public List<SeaIO410> SeaIO410 { get; set; }
    }


}