using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.Model
{
    public class OpeningTimeCommentView
    {
        public int CommentID { get; set; }
        public int EntryID { get; set; }
        public string Comment { get; set; }
        public string AddedBy { get; set; }
        public System.DateTime Datetime { get; set; }
    }
}
