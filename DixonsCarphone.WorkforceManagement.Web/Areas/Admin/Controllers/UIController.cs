using DixonsCarphone.WorkforceManagement.ViewModels;
using DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels;
using DixonsCarphone.WorkforceManagement.ViewModels.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.Web.Areas.Admin.Controllers
{
    public class UIController : Controller
    {
        private IEnumerable<WidgetsAndControls> ls;
        private string basePartialPath = "~/Views/Partials";
        private string serObjPath = string.Empty;  

        public UIController()
        {
            ls = new List<WidgetsAndControls>
            {
                new WidgetsAndControls { Name = "Events", PathToPartial = basePartialPath + "/Widgets/_Events.cshtml" },
                new WidgetsAndControls { Name = "Footfall", PathToPartial = basePartialPath + "/Widgets/_Footfall.cshtml" },
                new WidgetsAndControls { Name = "Orders", PathToPartial = basePartialPath + "/Widgets/_Orders.cshtml" },
                new WidgetsAndControls { Name = "New Staff", PathToPartial = basePartialPath + "/Controls/_NewStaff.cshtml" },
                new WidgetsAndControls { Name = "Product Lis", PathToPartial = basePartialPath + "/Controls/_ProductList.cshtml"},
                new WidgetsAndControls { Name = "UK KPIs", PathToPartial = basePartialPath + "/Controls/_UkKpis.cshtml", OwnRow = true },

            };
        }
        public ActionResult Index()
        {
            //serObjPath = HttpContext.Server.MapPath("~/Data/HomeWidgetsAndControls.xml");

            //var model = Helpers.DeSerializeObject<List<WidgetsAndControls>>(serObjPath) ?? ls;
            var model = new BestPracticeUploadViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(IEnumerable<WidgetsAndControls> model)
        {
            serObjPath = HttpContext.Server.MapPath("~/Data/HomeWidgetsAndControls.xml");

            model.ToList().SerializeObject(serObjPath);
            return View(model);
        }

        public ActionResult Upload(BestPracticeUploadViewModel model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath(string.Format("~/Uploads/{0}/", model.Category)), fileName);
                    file.SaveAs(path);

                    var bestPracticeContent = new BestPracticeContent
                    {
                        Category = model.Category,
                        Title = model.Title,
                        PathToContent = Url.Content(path)
                    };
                    AddToBestPractice(bestPracticeContent);
                }
            }

            return View("Index",new BestPracticeUploadViewModel());
        }

        private void AddToBestPractice(BestPracticeContent bestPracticeContent)
        {
            var serObjPath = HttpContext.Server.MapPath("~/WebContent/BestPractice.xml");

            if (!Directory.Exists(HttpContext.Server.MapPath("~/WebContent")))
                Directory.CreateDirectory(HttpContext.Server.MapPath("~/BestPractice"));

            var ls = Helpers.DeSerializeObject<List<BestPracticeContent>>(serObjPath) ?? new List<BestPracticeContent>();
            var existing = ls.FirstOrDefault(x => x.Category == bestPracticeContent.Category && string.Equals(x.Title, bestPracticeContent.Title, StringComparison.InvariantCultureIgnoreCase));

            if (existing == null)
             ls.Add(bestPracticeContent);
            else
            {
                existing.Summary = bestPracticeContent.Summary;
                existing.PathToContent = bestPracticeContent.PathToContent;
            }

            ls.SerializeObject(serObjPath);
        }

        private void AddToFooter(FooterContent footerContent)
        {
            serObjPath = HttpContext.Server.MapPath("~/Footer/FootContent.xml");

            if (!Directory.Exists(HttpContext.Server.MapPath("~/Footer")))
                Directory.CreateDirectory(HttpContext.Server.MapPath("~/Footer"));

            var ls = Helpers.DeSerializeObject<List<FooterContent>>(serObjPath) ?? new List<FooterContent>(); 
            ls.Add(footerContent);

            ls.SerializeObject(serObjPath);
        }
    }
}