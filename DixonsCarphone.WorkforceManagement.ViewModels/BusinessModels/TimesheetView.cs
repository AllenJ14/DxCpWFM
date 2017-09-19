using System.Collections.Generic;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class TimesheetView
    {
        public string LastTotalizationDateTime { get; set; }
        public string ManagerSignoffDateTime { get; set; }
        public string TotalsUpToDateFlag { get; set; }
        public EmployeeView Employee { get; set; }
        public PeriodTotalDataView PeriodTotalData { get; set; }
        public PeriodView Period { get; set; }
    }

    public class PeriodView
    {
        public TimeFramePeriodView TimeFramePeriod { get; set; }
    }

    public class TimeFramePeriodView
    {
        public string PeriodDateSpan { get; set; }
        public string TimeFrameName { get; set; }
    }

    public class PeriodTotalDataView
    {
        public PeriodTotalsView PeriodTotals { get; set; }
    }

    public class PeriodTotalsView
    {
        public string PeriodDateSpan { get; set; }
        public TotalsView Totals { get; set; }
    }

    public class TotalsView
    {
        public List<TotalView> Total { get; set; }
    }

    public class TotalView
    {
        public string IsCurrencyFlag { get; set; }
        public string LaborAccountDescription { get; set; }
        public string LaborAccountId { get; set; }
        public string LaborAccountName { get; set; }
        public string AmmountInCurrency { get; set; }
        public string PayCodeId { get; set; }
        public string PayCodeName { get; set; }
        public string AmountInTime { get; set; }
        public string OrgJobId { get; set; }
        public string OrgJobName { get; set; }
        public string OrgJobDescription { get; set; }
    }

    public class EmployeeView
    {
        public PersonIdentityView PersonIdentity { get; set; }
    }

    public class PersonIdentityView
    {
        public string PersonNumber { get; set; }
    }


}
