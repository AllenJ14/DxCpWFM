using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.Web.Controllers
{
    public class OOSController : Controller
    {
        // GET: OOS
        public ActionResult Index()
        {
            LoginViewModel model = new LoginViewModel();

            if (TempData["modelPass"] != null)
            {
                model = (LoginViewModel)TempData["modelPass"];
                if(TempData["errorMessage"] != null)
                {
                    ViewBag.errorMessage = TempData["errorMessage"].ToString();
                }
            }
            return View(model);
        }
    }
}