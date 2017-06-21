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
        public double? Quarter { get; set; }
        public double? Period { get; set; }
        public double? WeekNumber { get; set; }
        public double? BranchNumber { get; set; }
        public string UkBranchNumber { get; set; }
        public string StoreName { get; set; }
        public string Division { get; set; }
        public double? Region { get; set; }
        public string RegionName { get; set; }
        public double? TotalHeadCount { get; set; }
        public double? TimeCardsCompleted { get; set; }
        public double? ZeroHour { get; set; }
        public double? ContractHours { get; set; }
        public double? FTContractHours { get; set; }
        public double? PTContractHours { get; set; }
        public double? OriginalTarget { get; set; }
        public double? FinalTarget { get; set; }
        public double? HolidayTaken { get; set; }
        public double? LTS { get; set; }
        public double? FTLeakage { get; set; }
        public double? LeakageCount { get; set; }
        public double? AllApprovedHours { get; set; }
        public double? PayrollCorrections { get; set; }
        public double? SOH { get; set; }
        public bool? OverTargetFlag { get; set; }
        public bool? OverContractFlag { get; set; }
        public double? ComplianceScore { get; set; }
        public double? SOHUtilization { get; set; }
        public Nullable<bool> UtilizationZeroFlag { get; set; }
        public double? GSContract { get; set; }
        public double? GSTarget { get; set; }
        public double? GSHolidayTaken { get; set; }
        public double? GSFTLeakage { get; set; }
        public double? GSAllApprovedHours { get; set; }
        public double? GSSOHSpend { get; set; }
        public double? GSOverTargetFlag { get; set; }
        public double? GSOverContractFlag { get; set; }
        public string StoreFlag { get; set; }
        public double? StoreCount { get; set; }
        public double? FTHeadcountSAS { get; set; }
        public double? FTHeadcountSIS { get; set; }
        public double? ContractBaseTarget { get; set; }
        public double? PaidForHoursNotWorkedCompliance { get; set; }
        public string SATSOH { get; set; }
        public string SATTarget { get; set; }
        public string ServiceLVSOH { get; set; }
        public string ServiceLV1Target { get; set; }
        public string ServiceLV2SOH { get; set; }
        public string ServiceLV2Target { get; set; }
        public string SATContractHours { get; set; }
        public string ServiceLV1ContractHours { get; set; }
        public string ServiceLV2ContractHours { get; set; }
        public double? C25PercOver { get; set; }
        public double? C25PercUnder { get; set; }
        public double? PlusMinus10Perc { get; set; }
        public double? Awol { get; set; }
        public double? Suspension { get; set; }
        public double? Sickness { get; set; }
        public Nullable<double> IgniteCredits { get; set; }
    }
}
