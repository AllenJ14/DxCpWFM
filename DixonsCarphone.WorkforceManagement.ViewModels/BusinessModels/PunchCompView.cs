﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class PunchCompView
    {
        public Nullable<int> CST_CNTR_int { get; set; }
        public string CST_CNTR_DESC { get; set; }
        public string STORE_NUM { get; set; }
        public string REGION_CD { get; set; }
        public string DIV_CD { get; set; }
        public string CHN_CD { get; set; }
        public string TRD_ARM_CD { get; set; }
        public Nullable<int> EMP_NUM { get; set; }
        public string FORENAME { get; set; }
        public string SURNAME { get; set; }
        public string CONTRACT_DAYS { get; set; }
        public string CONTRACT_HOURS { get; set; }
        public string SALARY_TYPE { get; set; }
        public string ROLE { get; set; }
        public Nullable<byte> DAY_NUM { get; set; }
        public Nullable<int> FNCL_WK_NUM { get; set; }
        public string FNCL_PD_NUM { get; set; }
        public string FNCL_QTR_NUM { get; set; }
        public string YEAR_NUM { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string PUNCHTIME_IN { get; set; }
        public string PUNCHTIME_OUT { get; set; }
        public string Clock_In_Early { get; set; }
        public string Clock_In_Late { get; set; }
        public string Clock_Out_Early { get; set; }
        public string Clock_Out_Late { get; set; }
        public int Clock_In_Acceptable { get; set; }
        public int Clock_In_Not_Acceptable { get; set; }
        public int Clock_Out_Not_Acceptable { get; set; }
        public int Clock_Out_Acceptable { get; set; }
        public int Count_In_Missing { get; set; }
        public int Count_Out_Missing { get; set; }
        public int Count_Schedule_Start { get; set; }
        public int Count_Schedule_End { get; set; }
        public int PunchInOveride { get; set; }
        public int PunchOutOveride { get; set; }
        public Nullable<short> TMSHTITEMTYPEID { get; set; }
        public string ClockType_IN { get; set; }
        public Nullable<short> TMSHTITEMTYPEID_OUT { get; set; }
        public string ClockType_OUT { get; set; }
        public string ScheduleDate { get; set; }
        public string Overide_ClockType_IN { get; set; }
        public string Overide_PUNCHTIME_IN { get; set; }
        public string Overide_ClockType_OUT { get; set; }
        public string Overide_PUNCHTIME_OUT { get; set; }
    }
}
