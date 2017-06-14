using DixonsCarphone.WorkforceManagement.Business.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.Web.Controllers
{
    public class APITestController : BaseController
    {
        IKronosManager _KronosManager;

        public APITestController()
        {
            _KronosManager = new KronosManager();
        }

        // GET: APITest
        public async Task<ActionResult> Index()
        {


            return View();
        }
    }
}