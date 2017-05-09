using DixonsCarphone.WorkforceManagement.ViewModels;
using DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.Web.Controllers
{
    public class BestPracticeController : BaseController
    {
        // GET: BestPractice
        public ActionResult Index()
        {
            var ls = GetContent();
            return View(ls);
        }

        public ActionResult Section(string sectionName, string cateGory)
        {
            var content = new BestPracticeContent { Title = sectionName, Category = cateGory, Summary = "Some Text" };
            return View("Index", content);
        }


        public ActionResult ViewDocument(string FileName, string category)
        {
            try
            {
                var FullPath = FileName; // Path.Combine(Server.MapPath(string.Format("~/Uploads/Resources/{0}/", category)), FileName);
                if (!System.IO.File.Exists(FullPath))
                {
                    return HttpNotFound();
                }
                var contentType = "text/plain";
                return File(FullPath, contentType, Path.GetFileName(FullPath));
            }
            catch
            {
            }
            return File(new byte[] { }, "text/plain");
        }

        private List<BestPracticeContent> GetContent()
        {
            var serObjPath = HttpContext.Server.MapPath("~/WebContent/BestPractice.xml");

            if (!Directory.Exists(HttpContext.Server.MapPath("~/WebContent")))
                return new List<BestPracticeContent>();

            var ls = Helpers.DeSerializeObject<List<BestPracticeContent>>(serObjPath) ?? new List<BestPracticeContent>();
            return ls;
        }
    }
}