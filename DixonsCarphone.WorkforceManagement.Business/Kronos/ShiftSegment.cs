using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DixonsCarphone.WorkforceManagement.Business.Kronos
{
    public class ShiftSegment
    {
        [XmlAttribute]
        public string SegmentTypeName { get; set; }

        [XmlAttribute]
        public string StartDate { get; set; }

        [XmlAttribute]
        public string StartTime { get; set; }

        [XmlAttribute]
        public int StartDayNumber { get; set; }

        [XmlAttribute]
        public string EndDate { get; set; }

        [XmlAttribute]
        public string EndTime { get; set; }

        [XmlAttribute]
        public int EndDayNumber { get; set; }

        [XmlAttribute]
        public string LaborAccountName { get; set; }
    }
}
