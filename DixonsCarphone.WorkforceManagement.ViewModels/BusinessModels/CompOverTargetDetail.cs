using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class CompOverTargetDetail 
    {
        public string HeadlineFigure {
            get {
                return Math.Round(SOHSpend - SOHTarget, 1).ToString();
                } }
        public decimal SOHSpend { get; set; }
        public decimal SOHTarget { get; set; }
        public decimal Variance {
            get {
                return Math.Round(SOHSpend - SOHTarget, 1);
            }}
    }
}
