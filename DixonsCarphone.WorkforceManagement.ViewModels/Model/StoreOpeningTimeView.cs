﻿using System;
using System.ComponentModel.DataAnnotations;

namespace DixonsCarphone.WorkforceManagement.ViewModels.Model
{
    public class StoreOpeningTimeView
    {
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
    }
}