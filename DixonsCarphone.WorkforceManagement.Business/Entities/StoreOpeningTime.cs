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
    
    public partial class StoreOpeningTime
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StoreOpeningTime()
        {
            this.OpeningTimesComments = new HashSet<OpeningTimesComment>();
        }
    
        public int EntryID { get; set; }
        public int StoreNumber { get; set; }
        public System.DateTime DateTimeSubmitted { get; set; }
        public System.TimeSpan SundayOpen { get; set; }
        public System.TimeSpan SundayClose { get; set; }
        public System.TimeSpan MondayOpen { get; set; }
        public System.TimeSpan MondayClose { get; set; }
        public System.TimeSpan TuesdayOpen { get; set; }
        public System.TimeSpan TuesdayClose { get; set; }
        public System.TimeSpan WednesdayOpen { get; set; }
        public System.TimeSpan WednesdayClose { get; set; }
        public System.TimeSpan ThursdayOpen { get; set; }
        public System.TimeSpan ThursdayClose { get; set; }
        public System.TimeSpan FridayOpen { get; set; }
        public System.TimeSpan FridayClose { get; set; }
        public System.TimeSpan SaturdayOpen { get; set; }
        public System.TimeSpan SaturdayClose { get; set; }
        public System.DateTime EffectiveDate { get; set; }
        public Nullable<bool> TemporaryChange { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> DateTimeModified { get; set; }
        public string ModifiedByUser { get; set; }
        public string SubmittedByUser { get; set; }
        public string ReasonForChange { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OpeningTimesComment> OpeningTimesComments { get; set; }
        public virtual Store Store { get; set; }
    }
}
