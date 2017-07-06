using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DixonsCarphone.WorkforceManagement.Business.Managers;
using DixonsCarphone.WorkforceManagement.Web.Controllers;
using DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels;
using System.Threading.Tasks;
using DixonsCarphone.WorkforceManagement.ViewModels;
using DixonsCarphone.WorkforceManagement.Business.Entities;

namespace DixonsCarphone.WorkforceManagement.Web.Areas.Workflow.Controllers
{
    [Authorize]
    public class FormController : BaseController
    {
        private readonly ITicketManager _ticketManager;

        public FormController()
        {
            _ticketManager = new TicketManager();
        }

        public async Task<ActionResult> Index()
        {
            if(TempData["ticketID"] != null)
            {
                ViewBag.ticketID = TempData["ticketID"];
            }

            if (System.Web.HttpContext.Current.Session["_ChannelName"] != null || System.Web.HttpContext.Current.Session["_DivisionName"] != null || System.Web.HttpContext.Current.Session["_RegionNumber"] != null)
            {
                ViewBag.errorMessage = "Please select a branch from the top menu in order to submit a form.";
                return View();
            }
            else
            {
                return View(await _ticketManager.GetTypeMenu());
            }            
        }

        // GET: Workflow/Form
        public async Task<ActionResult> NewSubmission(int FormTypeId = -5)
        {  
            if(FormTypeId == -5)
            {
                return RedirectToAction("Index");
            }
            var data = await _ticketManager.GetFormTemplate(FormTypeId);
            var name = _ticketManager.GetFormName(FormTypeId);

            return View(new TicketFormTemplate
            {
                QuestionList = mapper.Map<List<TicketTemplateView>>(data),
                FormName = name
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NewSubmission(List<TicketQ_A> q, int TicketTypeId, int exception)
        {
            var toReturn = await _ticketManager.SubmitTicket(TicketTypeId, System.Web.HttpContext.Current.Session["_UserName"].ToString(), StoreNumber, mapper.Map<List<TicketAnswer>>(q), exception);

            TempData["ticketID"] = toReturn;
            return RedirectToAction("Index");
        }

    }
}