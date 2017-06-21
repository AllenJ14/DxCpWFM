﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class DashboardViewChannel
    {
        public Nullable<long> DashboardDataId { get; set; }
        public string Year { get; set; }
        public Nullable<byte> Quarter { get; set; }
        public Nullable<byte> Period { get; set; }
        public Nullable<int> WeekNumber { get; set; }
        public Nullable<int> BranchNumber { get; set; }
        public Nullable<int> UkBranchNumber { get; set; }
        public Nullable<int> StoreName { get; set; }
        public string Division { get; set; }
        public Nullable<short> Region { get; set; }
        public Nullable<int> RegionName { get; set; }
        public Nullable<int> TotalHeadcount { get; set; }
        public Nullable<int> TimeCardsCompleted { get; set; }
        public Nullable<int> ZeroHour { get; set; }
        public Nullable<double> ContractHours { get; set; }
        public Nullable<double> FTContractHours { get; set; }
        public Nullable<double> PTContractHours { get; set; }
        public Nullable<double> OriginalTarget { get; set; }
        public Nullable<double> FinalTarget { get; set; }
        public Nullable<double> HolidayTaken { get; set; }
        public Nullable<double> LTS { get; set; }
        public Nullable<double> FTLeakage { get; set; }
        public Nullable<int> LeakageCount { get; set; }
        public Nullable<double> AllApprovedHours { get; set; }
        public Nullable<double> PayrollCorrections { get; set; }
        public Nullable<double> SOH { get; set; }
        public Nullable<int> OverTargetFlag { get; set; }
        public Nullable<int> OverContractFlag { get; set; }
        public Nullable<int> ComplianceScore { get; set; }
        public Nullable<int> SOHUtilization { get; set; }
        public Nullable<int> UtilizationZeroFlag { get; set; }
        public Nullable<double> GSContract { get; set; }
        public Nullable<double> GSTarget { get; set; }
        public Nullable<double> GSHolidayTaken { get; set; }
        public Nullable<double> GSFTLeakage { get; set; }
        public Nullable<double> GSAllApprovedHours { get; set; }
        public Nullable<double> GSSOHSpend { get; set; }
        public Nullable<int> GSOverTargetFlag { get; set; }
        public Nullable<int> GSOverContractFlag { get; set; }
        public string StoreFlag { get; set; }
        public Nullable<int> StoreCount { get; set; }
        public Nullable<int> FTHeadcountSAS { get; set; }
        public Nullable<int> FTHeadcountSIS { get; set; }
        public Nullable<double> ContractBaseTarget { get; set; }
        public Nullable<int> PaidForHoursNotWorkedCompliance { get; set; }
        public Nullable<double> SATSOH { get; set; }
        public Nullable<double> SATTarget { get; set; }
        public Nullable<double> ServiceLV1SOH { get; set; }
        public Nullable<double> ServiceLV1Target { get; set; }
        public Nullable<double> ServiceLV2SOH { get; set; }
        public Nullable<double> ServiceLV2Target { get; set; }
        public Nullable<double> SATContractHours { get; set; }
        public Nullable<double> ServiceLV1ContractHours { get; set; }
        public Nullable<double> ServiceLV2ContractHours { get; set; }
        public Nullable<int> C25PercOver { get; set; }
        public Nullable<int> C25PercUnder { get; set; }
        public Nullable<int> PlusMinus10Perc { get; set; }
        public Nullable<double> Awol { get; set; }
        public Nullable<double> Suspension { get; set; }
        public Nullable<double> Sickness { get; set; }
        public Nullable<double> GSTraining { get; set; }
        public Nullable<double> SATTraining { get; set; }
        public Nullable<double> L1Training { get; set; }
        public Nullable<double> L2Training { get; set; }
        public Nullable<double> SATHoliday { get; set; }
        public Nullable<double> L1Holiday { get; set; }
        public Nullable<double> L2Holiday { get; set; }
        public Nullable<double> IgniteCredits { get; set; }

    }
}
