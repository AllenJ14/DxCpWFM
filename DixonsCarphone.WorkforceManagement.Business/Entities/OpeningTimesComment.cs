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
    using System.Collections.Generic;
    
    public partial class OpeningTimesComment
    {
        public int CommentID { get; set; }
        public int EntryID { get; set; }
        public string Comment { get; set; }
        public string AddedBy { get; set; }
        public System.DateTime Datetime { get; set; }
    
        public virtual StoreOpeningTime StoreOpeningTime { get; set; }
    }
}