using DixonsCarphone.WorkforceManagement.Business.Entities;
using DixonsCarphone.WorkforceManagement.Business.Managers;
using DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.Web.Controllers
{
    public class ProfilesController : BaseController
    {
        IPeopleManager _peopleManager;

        public ProfilesController()
        {
            _peopleManager = new PeopleManager();
        }
        public ActionResult Index()
        {
            return View("Store");
        }

        public async Task<ActionResult> People()
        {
            var result = await _peopleManager.GetStoreStaff(StoreNumber);

            var vm = new PeopleViewModel { Staff = mapper.Map<List<HrFeed>, List<HrFeedView>>(result) };
            return View(vm);
        }
    }
}