using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class CompOverContractDetail
    {
        public string HeadlineFigure {
            get
            {
                return (CurrentContract - ContractBase).ToString();
            }}
        public decimal CurrentContract { get; set; }
        public decimal ContractBase { get; set; }
        public decimal Variance
        {
            get
            {
                return Math.Round(CurrentContract - ContractBase, 1);
            }
        }
    }
}
