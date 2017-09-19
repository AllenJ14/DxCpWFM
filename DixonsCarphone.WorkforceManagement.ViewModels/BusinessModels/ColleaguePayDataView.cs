namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class ColleaguePayDataView
    {
        public int PersonNumber { get; set; }
        public int WeekNumber { get; set; }
        public decimal WorkedHours { get; set; }
        public decimal Holiday { get; set; }
        public decimal Sickness { get; set; }
        public decimal PaidAbsence { get; set; }
        public decimal UnpaidAbsence { get; set; }
    }
}
