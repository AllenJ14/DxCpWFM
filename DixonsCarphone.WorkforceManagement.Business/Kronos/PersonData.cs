using System.Xml.Serialization;

namespace DixonsCarphone.WorkforceManagement.Business.Kronos
{
    public class PersonData
    {
        [XmlElement]
        public Person Person { get; set; }
    }
}