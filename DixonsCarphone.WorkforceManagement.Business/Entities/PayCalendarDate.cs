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
    
    public partial class PayCalendarDate
    {
        public string Chain { get; set; }
        public string Year { get; set; }
        public string Period { get; set; }
        public int Week { get; set; }
        public System.DateTime WCDate { get; set; }
    
        public virtual PayCalendarRef PayCalendarRef { get; set; }
    }
}
