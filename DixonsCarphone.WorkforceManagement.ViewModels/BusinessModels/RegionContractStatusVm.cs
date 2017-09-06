using System.Collections.Generic;
using System.Linq;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class RegionContractStatusVm
    {
        public short Marker { get; set; }
        public string GraphLegendSAS { get; set; }
        public string ContractsSAS { get; set; }
        public string VacanciesSAS { get; set; }
        public string ContractBaseSAS { get; set; }

        public string GraphLegendSIS { get; set; }
        public string ContractsSIS { get; set; }
        public string VacanciesSIS { get; set; }
        public string ContractBaseSIS { get; set; }

        public void populateVm(List<RegionContractStatusView> a)
        {
            GraphLegendSAS = "[\"";
            ContractsSAS = "[";
            VacanciesSAS = "[";
            ContractBaseSAS = "[";
            GraphLegendSIS = "[";
            ContractsSIS = "[";
            VacanciesSIS = "[";
            ContractBaseSIS = "[";

            IEnumerable<RegionContractStatusView> b = new List<RegionContractStatusView>();
            if (a.First().Area.Contains("-"))
            {
                Marker = 1;
                b = a;
            }
            else
            {
                Marker = 2;
                b = a.Where(x => int.Parse(x.Area) < 700);
            }

            foreach (var item in b)
            {
                GraphLegendSAS = GraphLegendSAS + item.Area.ToString() + "\",\"";
                ContractsSAS = ContractsSAS + (item.ContractHours * 100).ToString() + ",";
                VacanciesSAS = VacanciesSAS + (item.VacancyHours * 100).ToString() + ",";
                ContractBaseSAS = ContractBaseSAS + 100 + ",";
            }
            GraphLegendSAS = GraphLegendSAS.TrimEnd('\"').TrimEnd(',') + "]";
            ContractsSAS = ContractsSAS.TrimEnd(',') + "]";
            VacanciesSAS = VacanciesSAS.TrimEnd(',') + "]";
            ContractBaseSAS = ContractBaseSAS.TrimEnd(',') + "]";

            if(Marker == 2)
            {
                foreach (var item in a.Where(x => int.Parse(x.Area) > 700))
                {
                    GraphLegendSIS = GraphLegendSIS + item.Area.ToString() + ",";
                    ContractsSIS = ContractsSIS + (item.ContractHours * 100).ToString() + ",";
                    VacanciesSIS = VacanciesSIS + (item.VacancyHours * 100).ToString() + ",";
                    ContractBaseSIS = ContractBaseSIS + 100 + ",";
                }
                GraphLegendSIS = GraphLegendSIS.TrimEnd(',') + "]";
                ContractsSIS = ContractsSIS.TrimEnd(',') + "]";
                VacanciesSIS = VacanciesSIS.TrimEnd(',') + "]";
                ContractBaseSIS = ContractBaseSIS.TrimEnd(',') + "]";
            }

        }
    }
}
