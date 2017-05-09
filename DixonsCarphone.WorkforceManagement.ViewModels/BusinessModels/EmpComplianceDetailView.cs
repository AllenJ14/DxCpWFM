﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.Model
{
    public class EmpComplianceDetailView
    {
        public int EntryId { get; set; }
        public int WeekNumber { get; set; }
        public short StoreNumber { get; set; }
        public string PersonNumber { get; set; }
        public string PersonName { get; set; }
        public decimal EmployeeStandardHours { get; set; }
        public bool TimecardSignedOff { get; set; }
        public bool FTLeakageFlag { get; set; }
        public decimal WorkedHours { get; set; }
        public bool ZeroHourFlag { get; set; }
    }
}
