using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EPTS.Repositories.XML.Devices.Infrastructure.Entities.VisaCom.Com
{
    public abstract class Com:Visa
    {
        [XmlElement(ElementName = "BaudRate")]
        public string BaudRate { get; set; }
        [XmlElement(ElementName = "TimeOut")]
        public string TimeOut { get; set; }

    }
}
