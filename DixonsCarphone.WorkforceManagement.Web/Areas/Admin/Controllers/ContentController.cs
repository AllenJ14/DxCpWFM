using DixonsCarphone.WorkforceManagement.Business.Entities;
using DixonsCarphone.WorkforceManagement.Business.Managers;
using DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels;
using DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels.Cms;
using DixonsCarphone.WorkforceManagement.ViewModels.Model;
using DixonsCarphone.WorkforceManagement.Web.Attributes;
using DixonsCarphone.WorkforceManagement.Web.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.Web.Areas.Admin.Controllers
{
    public class ContentController : BaseController
    {
        private readonly IActivityManager _activityManager;
        private readonly IContentManager _contentManager;

        public ContentController()
        {
            _activityManager = new ActivityManager();
            _contentManager = new ContentManager();
        }

        // GET: Admin/Home
        [UserFilter(AccessLevel = "Admin")]
        public async Task<ActionResult> Index()
        {
            var vm = await GetAdminContentViewModel();

            return View(vm);
        }

        [UserFilter(AccessLevel = "Admin")]
        public ActionResult NewsAndActions()
        {
            var vm = new ActivityAdminViewModel();
            return View(vm);
        }

        //Show summary of opening times pending approval
        [UserFilter(AccessLevel = "Admin, OHAdmin")]
        public async Task<ActionResult> PendingTimes(string SearchString)
        {
            AdminOpeningTimes adminOpeningTimes = new AdminOpeningTimes();

            adminOpeningTimes.PendingApproval = await _storeManager.GetPendingStoreOpeningTimes();
            
            if(!string.IsNullOrEmpty(SearchString))
            {
                Int32 number;
                bool valid = Int32.TryParse(SearchString, out number);
                if (valid)
                {
                    adminOpeningTimes.QueriedRecord = mapper.Map<StoreOpeningTimeView>(await _storeManager.GetCurrentOpeningTimes(number));
                }
                else
                {
                    ViewBag.ErrorMessage = "Invalid store entry";
                }
                System.Web.HttpContext.Current.Session["_SelectedStore"] = number;
            }

            return View(adminOpeningTimes);
        }

        // Load view for new opening time request
        [UserFilter(AccessLevel = "Admin, OHAdmin")]
        public ActionResult NewStoreTimeRequest()
        {
            var vm = new ChangeStoreOpeningTimeViewModel();
            return View("ChangeStoreOpeningTimes", vm);
        }

        //Post new opening time submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeTimes(ChangeStoreOpeningTimeViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.ReasonForChange = Server.HtmlEncode(model.ReasonForChange);
                await _storeManager.ProposeNewStoreOpeningTime(model, (int)System.Web.HttpContext.Current.Session["_SelectedStore"], System.Web.HttpContext.Current.Session["_UserName"].ToString());
            }

            return RedirectToAction("PendingTimes");
        }

        //Retrieve all relevant detail for reviewing a submission
        [UserFilter(AccessLevel = "Admin, OHAdmin")]
        public async Task<ActionResult> ReviewTimes(int entryID, int StoreID)
        {
            AdminHoursReview adminHoursReview = new AdminHoursReview();

            adminHoursReview.CurrentTime = mapper.Map<StoreOpeningTime, StoreOpeningTimeView>(await _storeManager.GetCurrentOpeningTimes(StoreID));
            adminHoursReview.ProposedTime = mapper.Map<StoreOpeningTime, StoreOpeningTimeView>(await _storeManager.GetSingleTime(entryID));
            adminHoursReview.OutstandingChanges = mapper.Map<List<StoreOpeningTime>, List<StoreOpeningTimeView>>(await _storeManager.GetPendingStoreOpeningTimes(StoreID, entryID));
            adminHoursReview.Comments = mapper.Map<List<OpeningTimesComment>, List<OpeningTimeCommentView>>(await _storeManager.GetAllComments(entryID));
            adminHoursReview.CalcDifferences();

            return View(adminHoursReview);
        }

        // Add comment to opening time request
        [HttpPost]
        [UserFilter(AccessLevel = "Admin, OHAdmin")]
        public async Task<ActionResult> ReviewTimes(int EntryID, string Comment, int storeNum)
        {
            await _storeManager.AddOpeningTimesComment(EntryID, Comment, HttpContext.User.Identity.Name);

            return RedirectToAction("ReviewTimes", new { EntryID = EntryID, StoreID = storeNum });
        }

        // Approve Opening time request
        [UserFilter(AccessLevel = "Admin, OHAdmin")]
        public async Task<ActionResult> ApproveOpeningTime(int EntryID)
        {
            await _storeManager.ApproveRejectPendingRequest(EntryID, true);
            return RedirectToAction("PendingTimes");

        }

        //Reject Opening time request
        [UserFilter(AccessLevel = "Admin, OHAdmin")]
        public async Task<ActionResult> DeclineOpeningTime(int EntryID)
        {
            await _storeManager.ApproveRejectPendingRequest(EntryID, false);
            return RedirectToAction("PendingTimes");

        }

        //Load list of 'articles'
        [UserFilter(AccessLevel = "Admin")]
        public async Task<ActionResult> GetContentHeaderAndDetails()
        {
            var data = await _contentManager.GetAllContentHeaderAndDetails();
            ContentViewModel vm = new ContentViewModel {
                ContentCollection = mapper.Map<List<ContentHeaderViewModel>>(data)
            };

            return View("ContentList", vm);
        }

        //Upload image file
        [HttpPost]
        [UserFilter(AccessLevel = "Admin")]
        public ActionResult GetContentHeaderAndDetails(HttpPostedFileBase file)
        {

            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Images/CMS_Uploads"), fileName);
                file.SaveAs(path);
            }

            return RedirectToAction("GetContentHeaderAndDetails",1);
        }

        [UserFilter(AccessLevel = "Admin")]
        public async Task<ActionResult> ContentEditor(string contentId, string contentHeaderId = null)
        {
            int cId;
            int chId;
            var vm = new ContentDetailViewModel
            {
                ContentDetailId = 0,
                ContentHeaderId = !string.IsNullOrEmpty(contentHeaderId) && int.TryParse(contentHeaderId, out chId) ? chId : 1 // TODO Default to Best Practice
            };

            if (int.TryParse(contentId, out cId))
            {
                if (cId == 0) return View("ContentEditor", vm);
                var data = await _contentManager.GetContentDetail(cId);
                vm = mapper.Map<ContentDetailViewModel>(data);
            }

            return View("ContentEditor", vm);
        }

        //Save new/updated content
        [UserFilter(AccessLevel = "Admin")]
        public async Task<ActionResult> SaveContent(ContentDetailViewModel model)
        {
            var msg = string.Empty;

            if (Request.Form["DeleteButton"] != null)
            {
                var del = await DeleteContent(model.ContentDetailId, model.ContentHeaderId.GetValueOrDefault());
                if (del)
                {
                    RefreshCache();
                    return RedirectToAction("GetContentHeaderAndDetails", new { contentHeaderId = model.ContentHeaderId });
                }
                else
                    msg = "Error occurred while saving";
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var rtn = await _contentManager.SaveContentDetail(mapper.Map<ContentDetail>(model));
                    if (rtn)
                    {
                        RefreshCache();
                        return RedirectToAction("GetContentHeaderAndDetails", new { contentHeaderId = model.ContentHeaderId });
                    }
                    else
                        msg = "Error occurred while saving";
                }
                else
                {
                    model.Message = "Please provide all required info before submitting";
                    model.MessageType = MessageType.Error;
                }
            }

            return View("ContentEditor", model);
        }

        // add new event/task/notification
        [UserFilter(AccessLevel = "Admin")]
        public async Task<ActionResult> CreateActivity(ActivityAdminViewModel model)
        {
            //var vm = await GetAdminContentViewModel();

            if (ModelState.IsValid)
            {
                var rtn = await _activityManager.AddActivity(model);
                ModelState.Clear();
                model = new ActivityAdminViewModel { Message = "New activity added successfully", MessageType = MessageType.Info};
            }
            else
            {
                model.Message = "Please provide required info";
            }

            return View("NewsAndActions", model);
        }

        [UserFilter(AccessLevel = "Admin")]
        private async Task<bool> DeleteContent(int contentDetailId, int contentHeaderId)
        {
            var msg = string.Empty;

            return await _contentManager.DeleteContentDetail(contentDetailId);
        }

        [UserFilter(AccessLevel = "Admin")]
        private async Task<AdminContentViewModel> GetAdminContentViewModel()
        {
            //var data = await _storeManager.GetPendingStoreOpeningTimes();
            //var vm = new AdminContentViewModel { PendingOpeningTimes = mapper.Map<List<PendingOpeningTimeView>>(data) };
            var vm = new AdminContentViewModel();
            return vm;
        }

        private void RefreshCache()
        {
            var path = HttpContext.Server.MapPath("~/CacheDependencyFiles/BestPractice.txt");
            var createText = DateTime.Now.ToString();
            System.IO.File.WriteAllText(path, createText);
        }
    }
}