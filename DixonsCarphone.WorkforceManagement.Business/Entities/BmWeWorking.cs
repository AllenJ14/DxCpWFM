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
    
    public partial class BmWeWorking
    {
        public int EntryId { get; set; }
        public System.DateTime Date { get; set; }
        public byte Day { get; set; }
        public short BranchNum { get; set; }
        public string BranchName { get; set; }
        public Nullable<short> RegionNum { get; set; }
        public byte Worked { get; set; }
        public Nullable<byte> NotPunched { get; set; }
        public Nullable<byte> Closed { get; set; }
    }
}
