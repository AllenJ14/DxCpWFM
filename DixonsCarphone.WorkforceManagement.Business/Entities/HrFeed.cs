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
    
    public partial class HrFeed
    {
        public long HR_FEED_ID { get; set; }
        public Nullable<int> STORE_NUM { get; set; }
        public Nullable<long> EMP_NUM { get; set; }
        public string FORENAME { get; set; }
        public string SURNAME { get; set; }
        public string CONTRACT_DAYS { get; set; }
        public Nullable<double> CONTRACT_HOURS { get; set; }
        public string SalaryType { get; set; }
        public string HourlyBasic { get; set; }
        public string AnnualBasic { get; set; }
        public string ROLE { get; set; }
        public Nullable<System.DateTime> DOJ { get; set; }
        public string DOL { get; set; }
        public Nullable<System.DateTime> DATE_IN_CURRENT_ROLE { get; set; }
        public Nullable<System.DateTime> DATE_IN_CURRENT_DEPARTMENT { get; set; }
        public Nullable<int> StaffTypeId { get; set; }
        public string EMP_CLASS { get; set; }
        public Nullable<System.DateTime> DATE_OF_BIRTH { get; set; }
        public Nullable<double> AGE { get; set; }
        public string SET_ID_DEPT { get; set; }
        public string DX_STARTYPE { get; set; }
        public string DIVISION { get; set; }
        public string REGION { get; set; }
        public Nullable<double> MAY_01_BALANCE { get; set; }
        public Nullable<double> TODAYS_BALANCE { get; set; }
        public Nullable<double> ABSENCE_TAKEN { get; set; }
    
        public virtual StaffType StaffType { get; set; }
    }
}
