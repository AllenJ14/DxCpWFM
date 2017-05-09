using System;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class HrFeedView
    {
        public long HR_FEED_ID { get; set; }
        public int? STORE_NUM { get; set; }
        public long? EMP_NUM { get; set; }
        public string FORENAME { get; set; }
        public string SURNAME { get; set; }
        public string CONTRACT_DAYS { get; set; }
        public double? CONTRACT_HOURS { get; set; }
        public string SalaryType { get; set; }
        public string HourlyBasic { get; set; }
        public string AnnualBasic { get; set; }
        public string ROLE { get; set; }
        public DateTime? DOJ { get; set; }
        public string DOL { get; set; }
        public DateTime? DATE_IN_CURRENT_ROLE { get; set; }
        public DateTime? DATE_IN_CURRENT_DEPARTMENT { get; set; }
        public int? StaffTypeId { get; set; }
        public string EMP_CLASS { get; set; }
        public DateTime? DATE_OF_BIRTH { get; set; }
        public double? AGE { get; set; }
        public string SET_ID_DEPT { get; set; }
        public string DX_STARTYPE { get; set; }
        public string DIVISION { get; set; }
        public double? REGION { get; set; }
        public double? MAY_01_BALANCE { get; set; }
        public double? TODAYS_BALANCE { get; set; }
        public double? ABSENCE_TAKEN { get; set; }
        
    }

}