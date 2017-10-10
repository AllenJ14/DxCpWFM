using System.Xml.Serialization;

namespace DixonsCarphone.WorkforceManagement.Business.Kronos
{
    public class PunchStatus
    {
        [XmlAttribute]
        public string Status { get; set; }

        [XmlElement]
        public Employee Employee { get; set; }
    }
}
