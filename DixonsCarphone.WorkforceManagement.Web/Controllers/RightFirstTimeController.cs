using DixonsCarphone.WorkforceManagement.Business.Managers;
using DixonsCarphone.WorkforceManagement.ViewModels;
using DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.Web.Controllers
{
    public class RightFirstTimeController : BaseController
    {
        IKronosManager _KronosManager;

        public RightFirstTimeController()
        {
            _KronosManager = new KronosManager();
        }

        [Authorize]
        public async Task<ActionResult> ColleaguePayPortal()
        {
            ColleaguePortalVm vm = new ColleaguePortalVm();

            vm.rawMenu = mapper.Map<List<PayCalendarRefView>>(await _storeManager.GetPayCalendarRef(_store.Channel));

            return View(vm);
        }

        public async Task<ActionResult> TimecardSignOff()
        {
            DateTime startDate = DateTime.Now.AddDays(-7).GetFirstDayOfWeek();
            var result = await _KronosManager.GetKronosHyperFind(_store.KronosStoreName, startDate.ToShortDateString(), startDate.AddDays(6).ToShortDateString());

            return View();
        }
    }
}