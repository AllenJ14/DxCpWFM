using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.Web.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Unauthorised()
        {
            var err = new ErrorViewModel { ErrorMessage = "You are not authorised to view this page" };
            return View("Unauthorised", err);
        }
    }

    public class ErrorViewModel
    {
        public string ErrorMessage { get; set; }
    }
}