using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class DashBoardView 
    {
        public int DashBoardDataId { get; set; }
        public string Year { get; set; }
        public Nullable<byte> Quarter { get; set; }
        public Nullable<byte> Period { get; set; }
        public Nullable<int> WeekNumber { get; set; }
        public Nullable<short> BranchNumber { get; set; }
        public string UkBranchNumber { get; set; }
        public string StoreName { get; set; }
        public string Division { get; set; }
        public Nullable<short> Region { get; set; }
        public string RegionName { get; set; }
        public Nullable<short> TotalHeadCount { get; set; }
        public Nullable<short> TimeCardsCompleted { get; set; }
        public Nullable<short> ZeroHour { get; set; }
        public Nullable<double> ContractHours { get; set; }
        public Nullable<double> FTContractHours { get; set; }
        public Nullable<double> PTContractHours { get; set; }
        public Nullable<double> OriginalTarget { get; set; }
        public Nullable<double> FinalTarget { get; set; }
        public Nullable<double> HolidayTaken { get; set; }
        public Nullable<double> FTLeakage { get; set; }
        public Nullable<short> LeakageCount { get; set; }
        public Nullable<double> AllApprovedHours { get; set; }
        public Nullable<double> SOH { get; set; }
        public Nullable<bool> OverTargetFlag { get; set; }
        public Nullable<bool> OverContractFlag { get; set; }
        public Nullable<short> ComplianceScore { get; set; }
        public Nullable<short> SOHUtilization { get; set; }
        public Nullable<bool> UtilizationZeroFlag { get; set; }
        public Nullable<double> GSContract { get; set; }
        public Nullable<double> GSTarget { get; set; }
        public Nullable<double> GSHolidayTaken { get; set; }
        public Nullable<double> GSFTLeakage { get; set; }
        public Nullable<double> GSAllApprovedHours { get; set; }
        public Nullable<double> GSSOHSpend { get; set; }
        public Nullable<bool> GSOverTargetFlag { get; set; }
        public Nullable<bool> GSOverContractFlag { get; set; }
        public string StoreFlag { get; set; }
        public Nullable<double> ContractBaseTarget { get; set; }
        public Nullable<double> IgniteCredits { get; set; }
        public Nullable<double> PunchCompliance { get; set; }
        public Nullable<short> ShortShifts { get; set; }
        public Nullable<double> PayEscalations { get; set; }
    }
}
