using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DixonsCarphone.WorkforceManagement.Business.Kronos
{
    [XmlRoot]
    public class HyperFindResult
    {
        [XmlAttribute]
        public string FullName { get; set;  }

        [XmlAttribute]
        public string PersonNumber { get; set; }

        [XmlElement]
        public PersonData PersonData { get; set; }
    }
}
