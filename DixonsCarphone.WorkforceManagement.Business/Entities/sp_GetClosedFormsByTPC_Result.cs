//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DixonsCarphone.WorkforceManagement.Business.Entities
{
    using System;
    
    public partial class sp_GetClosedFormsByTPC_Result
    {
        public int TicketId { get; set; }
        public string Title { get; set; }
        public System.DateTime DateTimeCreated { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public System.DateTime LastUpdate { get; set; }
        public string RaisedBy { get; set; }
        public byte EscalationLevel { get; set; }
        public Nullable<short> BranchNumber { get; set; }
    }
}