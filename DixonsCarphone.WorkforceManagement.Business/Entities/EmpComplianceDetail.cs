//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DixonsCarphone.WorkforceManagement.Business.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmpComplianceDetail
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
