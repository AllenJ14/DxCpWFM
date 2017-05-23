using System;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class FileUploadRecord
    {
        public int EntryId { get; set; }
        public string FileCat { get; set; }
        public string FileName { get; set; }
        public Nullable<System.DateTime> LastUploaded { get; set; }
        public Nullable<bool> UploadFailed { get; set; }
    }
}
