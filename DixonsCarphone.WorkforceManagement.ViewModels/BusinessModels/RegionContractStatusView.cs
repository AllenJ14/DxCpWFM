using System;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class RegionContractStatusView
    {
        public string Area { get; set; }
        public Nullable<double> ContractBase { get; set; }
        public Nullable<double> ContractHours { get; set; }
        public Nullable<double> VacancyHours { get; set; }
    }
}
