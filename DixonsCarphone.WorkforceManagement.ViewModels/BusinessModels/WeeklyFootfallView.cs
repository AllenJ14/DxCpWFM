namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class WeeklyFootfallView
    {
        public int WeeklyFootfallId { get; set; }
        public int? Branch { get; set; }
        public string ProfileType { get; set; }
        public double? Sunday { get; set; }
        public double? Monday { get; set; }
        public double? Tuesday { get; set; }
        public double? Wednesday { get; set; }
        public double? Thursday { get; set; }
        public double? Friday { get; set; }
        public double? Saturday { get; set; }
    }
}