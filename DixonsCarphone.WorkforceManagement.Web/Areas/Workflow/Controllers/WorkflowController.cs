using DixonsCarphone.WorkforceManagement.Business.Entities;
using DixonsCarphone.WorkforceManagement.Business.Managers;
using DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels;
using DixonsCarphone.WorkforceManagement.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.Web.Areas.Workflow.Controllers
{
    [Authorize]
    public class WorkflowController : BaseController
    {
        private readonly ITicketManager _ticketManager;

        public WorkflowController()
        {
            _ticketManager = new TicketManager();
            
            if(System.Web.HttpContext.Current.Session["_wfUserGroup"] == null && System.Web.HttpContext.Current.Session["_UserName"] != null)
            {
                System.Web.HttpContext.Current.Session["_wfUserGroup"] = _ticketManager.GetUserGroup(System.Web.HttpContext.Current.Session["_UserName"].ToString());
            }            
        }

        // GET: Workflow/Home
        public async Task<ActionResult> Index()
        {
            TicketSummaryVM vm = new TicketSummaryVM();
            int userGroup = (int)System.Web.HttpContext.Current.Session["_wfUserGroup"];
            string userName = System.Web.HttpContext.Current.Session["_TPCOverride"] == null ? System.Web.HttpContext.Current.Session["_UserName"].ToString() : System.Web.HttpContext.Current.Session["_TPCOverride"].ToString();

            vm._TicketCollection = _getOpenSummaryList();

            var TypeMenu = await _ticketManager.GetTypeMenu();
            vm._TypeMenu = new List<SelectListItem>
            {
                new SelectListItem {Value = "0", Text = "All", Selected = true }
            };
            foreach(var item in TypeMenu)
            {
                vm._TypeMenu.Add(new SelectListItem
                {
                    Value = item.TicketTypeId.ToString(),
                    Text = item.Title
                });
            }            

            if(userGroup == 3)
            {
                var tpcList = await _ticketManager.GetTPCMenu();
                vm._TPCMenu = new List<SelectListItem>();
                foreach (var item in tpcList)
                {
                    vm._TPCMenu.Add(new SelectListItem
                    {
                        Value = item.UserName,
                        Text = item.FriendlyName,
                        Selected = item.UserName == userName
                    });
                }
            }

            System.Web.HttpContext.Current.Session["_viewTicket"] = null;

            if(TempData["error"] != null)
            {
                vm.Message = TempData["error"].ToString();
                vm.MessageType = MessageType.Error;
            }

            return View(vm);
        }

        public async Task<ActionResult> ViewTicket(int TicketId)
        {
            var userGroup = (int)System.Web.HttpContext.Current.Session["_wfUserGroup"];
            System.Web.HttpContext.Current.Session["_viewTicket"] = TicketId;
            var result = await _ticketManager.GetSingleTicket(TicketId, userGroup, System.Web.HttpContext.Current.Session["_UserName"].ToString());
            if (result.Status == null)
            {
                TempData["error"] = "This ticket does not exist or you do not have permissions.";
                return RedirectToAction("Index");
            }

            if(_ticketManager.ChkInteractLvl(result.EscalationLevel, result.TicketTypeId) == userGroup)
            {
                ViewBag.action = true;
            }

            return View(result);
        }
        
        public async Task<ActionResult> CancelTicket(int TicketId)
        {
            if(TicketId == (int)System.Web.HttpContext.Current.Session["_viewTicket"])
            {
                var result = await _ticketManager.CancelTicket(TicketId, System.Web.HttpContext.Current.Session["_UserName"].ToString());
                if (result == -5)
                {
                    TempData["error"] = "You cannot cancel this ticket as you are not the owner.";
                }
            }
            else
            {
                TempData["error"] = "Ticket ID mismatch occured, please try again.";
            }
            
            return RedirectToAction("Index");
        }
        
        public async Task<ActionResult> CompleteTicket(int TicketId)
        {
            if (TicketId == (int)System.Web.HttpContext.Current.Session["_viewTicket"])
            {
                var result = await _ticketManager.CompleteTicket(TicketId, System.Web.HttpContext.Current.Session["_UserName"].ToString(), (int)System.Web.HttpContext.Current.Session["_wfUserGroup"]);
                if (result == -5)
                {
                    TempData["error"] = "You cannot alter this ticket as it is assigned to someone else..";
                }
            }
            else
            {
                TempData["error"] = "Ticket ID mismatch occured, please try again.";
            }            

            return RedirectToAction("Index");
        }
        
        public async Task<ActionResult> SendTicket(int lvlAction)
        {
            var result = await _ticketManager.SendTicket((int)System.Web.HttpContext.Current.Session["_viewTicket"], System.Web.HttpContext.Current.Session["_UserName"].ToString(), lvlAction, (int)System.Web.HttpContext.Current.Session["_wfUserGroup"]);
            if (result == -5)
            {
                TempData["error"] = "You cannot alter this ticket as it is assigned to someone else.";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<PartialViewResult> _PostNewComment(string commentText)
        {
            var toReturn = await _ticketManager.AddNewComment(commentText, System.Web.HttpContext.Current.Session["_UserName"].ToString(), (int)System.Web.HttpContext.Current.Session["_viewTicket"]);
            return PartialView("~/Areas/Workflow/Views/Partials/_NewComment.cshtml", toReturn);
        }
       
        [HttpGet]
        public PartialViewResult _GetEscalationOptions(int ticketType, int level)
        {
            var toReturn = _ticketManager.GetEscalationOptions(ticketType, level);
            return PartialView("~/Areas/Workflow/Views/Partials/_EscOptions.cshtml", toReturn);
        }

        [HttpGet]
        public PartialViewResult _UpdateSummary(string status, string type, string TPC = "")
        {
            List<TicketSummaryView> toReturn = new List<TicketSummaryView>();

            if(TPC != "")
            {
                System.Web.HttpContext.Current.Session["_TPCOverride"] = TPC;
            }

            if(status == "1")
            {
                toReturn = _getOpenSummaryList(type);
            }
            else
            {
                toReturn = _getClosedSummaryList(type);
            }
            
            return PartialView("~/Areas/Workflow/Views/Partials/_SummaryList.cshtml", toReturn);
        }

        [HttpGet]
        public async Task<string> _getRegion(int branchNum)
        {
            return await _ticketManager.GetRegion(branchNum);
        }

        [HttpGet]
        [OutputCache(Duration = 30, VaryByParam = "none", Location = System.Web.UI.OutputCacheLocation.Client, NoStore = true)]
        public int _getTicketCount()
        {
            var toReturn = _getOpenSummaryList();

            if((int)System.Web.HttpContext.Current.Session["_wfUserGroup"] == 0)
            {
                return toReturn.Where(x => x.Description == "Branch").Count();
            }
            else
            {
                return toReturn.Count();
            }
        }

        private List<TicketSummaryView> _getOpenSummaryList(string filter = "0")
        {
            List<TicketSummaryView> toReturn = new List<TicketSummaryView>();
            int userGroup = (int)System.Web.HttpContext.Current.Session["_wfUserGroup"];
            string userName = System.Web.HttpContext.Current.Session["_TPCOverride"] == null ? System.Web.HttpContext.Current.Session["_UserName"].ToString() : System.Web.HttpContext.Current.Session["_TPCOverride"].ToString();
            string _filter = filter == "0" ? "%" : filter.ToString();

            if (userGroup == 0)
            {
                toReturn = mapper.Map<List<TicketSummaryView>>(_ticketManager.GetFormsSelf(userName, _filter));
            }
            else if (userGroup == 3)
            {
                toReturn = mapper.Map<List<TicketSummaryView>>(_ticketManager.GetFormsWithTPC(userName, _filter));
            }
            else
            {
                toReturn = mapper.Map<List<TicketSummaryView>>(_ticketManager.GetFormsWithAuth((int)System.Web.HttpContext.Current.Session["_wfUserGroup"], _filter));
            }
            return toReturn;
        }

        private List<TicketSummaryView> _getClosedSummaryList(string filter = "0")
        {
            List<TicketSummaryView> toReturn = new List<TicketSummaryView>();
            int userGroup = (int)System.Web.HttpContext.Current.Session["_wfUserGroup"];
            string userName = System.Web.HttpContext.Current.Session["_TPCOverride"] == null ? System.Web.HttpContext.Current.Session["_UserName"].ToString() : System.Web.HttpContext.Current.Session["_TPCOverride"].ToString();
            string _filter = filter == "0" ? "%" : filter.ToString();

            if (userGroup == 0)
            {
                toReturn = mapper.Map<List<TicketSummaryView>>(_ticketManager.GetClosedFormsSelf(userName, _filter));
            }
            else if (userGroup == 3)
            {
                toReturn = mapper.Map<List<TicketSummaryView>>(_ticketManager.GetClosedFormsWithTPC(userName, _filter));
            }
            else
            {
                toReturn = mapper.Map<List<TicketSummaryView>>(_ticketManager.GetClosedFormsWithAuth((int)System.Web.HttpContext.Current.Session["_wfUserGroup"], _filter));
            }
            return toReturn;
        }
    }
}