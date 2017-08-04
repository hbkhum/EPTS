using System.Xml.Serialization;

namespace EPTS.Repositories.XML.Devices.Infrastructure.Entities.VisaCom.Com.Scanner
{
    [XmlRoot(ElementName = "Scanner")]
    public class Scanner : Com
    {
        [XmlElement(ElementName = "Trigger")]
        public string Trigger { get; set; }
    }
}