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
    
    public partial class PeakHC
    {
        public string Channel { get; set; }
        public string Division { get; set; }
        public Nullable<short> Region { get; set; }
        public Nullable<short> Branch { get; set; }
        public string Store_Name { get; set; }
        public System.DateTime Date { get; set; }
        public int MaxHC { get; set; }
        public int DeployedHC { get; set; }
        public int Closed { get; set; }
    }
}
