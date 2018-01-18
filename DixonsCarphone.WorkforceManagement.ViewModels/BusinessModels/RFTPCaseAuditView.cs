using System;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class RFTPCaseAuditView
    {
        public int ActionID { get; set; }
        public int CaseID { get; set; }
        public string ActionType { get; set; }
        public string Comment { get; set; }
        public string CompletedBy { get; set; }
        public DateTime DateTimeCreated { get; set; }
    }
}
