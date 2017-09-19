using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DixonsCarphone.WorkforceManagement.Business.Kronos
{
    public class Person
    {
        [XmlAttribute]
        public string BirthDate { get; set; }

        [XmlAttribute]
        public string FirstName { get; set; }

        [XmlAttribute]
        public string FullName { get; set; }

        [XmlAttribute]
        public string HireDate { get; set; }

        [XmlAttribute]
        public string LastName { get; set; }

        [XmlAttribute]
        public string PersonNumber { get; set; }

        [XmlAttribute]
        public string ShortName { get; set; }

        [XmlAttribute]
        public int EmployeeStandardHours { get; set; }

        [XmlAttribute]
        public int FullTimeStandardHours { get; set; }

        [XmlAttribute]
        public string AccrualProfileName { get; set; }

        [XmlAttribute]
        public string ManagerSignoffThruDate { get; set; }

        [XmlAttribute]
        public string PayrollLockoutThruDate { get; set; }

        [XmlAttribute]
        public bool FingerRequiredFlag { get; set; }

        [XmlAttribute]
        public decimal BaseWageHourly { get; set; }
    }
}
