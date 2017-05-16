using DixonsCarphone.WorkforceManagement.Business.Entities;
using DixonsCarphone.WorkforceManagement.Business.Managers;
using DixonsCarphone.WorkforceManagement.ViewModels;
using DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels;
using DixonsCarphone.WorkforceManagement.Web.Attributes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.Web.Controllers
{
    public class ProfilesController : BaseController
    {
        IPeopleManager _peopleManager;
        IDashBoardDataManager _dashBoardManager;
        IKronosManager _kronosManager;

        public ProfilesController()
        {
            _peopleManager = new PeopleManager();
            _dashBoardManager = new DashBoardDataManager();
            _kronosManager = new KronosManager(isOffice);
        }
        public ActionResult Index()
        {
            return View("Store");
        }

        // Action when changing store in drop down list
        [UserFilter(AccessLevel ="Admin,TPC,RD,DD,RM")]
        public async Task<ActionResult> SetNewStore(string selectedStoreNumber)
        {
            if(System.Web.HttpContext.Current.Session["_RegionNumber"] != null)
            {
                System.Web.HttpContext.Current.Session.Remove("_RegionNumber");
            }
            if (System.Web.HttpContext.Current.Session["_DivisionName"] != null)
            {
                System.Web.HttpContext.Current.Session.Remove("_DivisionName");
            }
            if (System.Web.HttpContext.Current.Session["_ChannelName"] != null)
            {
                System.Web.HttpContext.Current.Session.Remove("_ChannelName");
            }

            int newStoreNumber;
            if (int.TryParse(selectedStoreNumber, out newStoreNumber))
            {
                var newSelectedStore = await _storeManager.GetStoreDetails(newStoreNumber);
                if (newSelectedStore != null)
                {
                    if(newSelectedStore.RegionNo == System.Web.HttpContext.Current.Session["_AccessArea"].ToString() || System.Web.HttpContext.Current.Session["_AccessLevel"].ToString() == "DD" || newSelectedStore.RegionNo == System.Web.HttpContext.Current.Session["_SecAccessArea"].ToString())
                    {
                        System.Web.HttpContext.Current.Session.Add("_StoreDetails", newSelectedStore);
                        System.Web.HttpContext.Current.Session.Add("_ROIFlag", newSelectedStore.Channel == "ROI");
                    }
                    else
                    {
                        TempData["errorMessage"] = "This store is not in your region. Please use the drop down to select a valid store.";
                    }
                }
                else
                {
                    TempData["errorMessage"] = "This store could not be found. Please use the drop down to select a valid store.";
                }
            }
            else if (selectedStoreNumber.Contains("Region"))
            {
                int newSelectedRegion = int.Parse(selectedStoreNumber.Replace("Region ",""));

                if(newSelectedRegion.ToString() == System.Web.HttpContext.Current.Session["_AccessArea"].ToString() || System.Web.HttpContext.Current.Session["_AccessLevel"].ToString() == "DD" || newSelectedRegion.ToString() == System.Web.HttpContext.Current.Session["_SecAccessArea"].ToString())
                {
                    System.Web.HttpContext.Current.Session.Add("_RegionNumber", newSelectedRegion);
                }
                else
                {
                    TempData["errorMessage"] = "The region you have selected is not valid for your account. Please use the drop down to select a valid region.";
                }
                
            }
            else if (selectedStoreNumber.Contains("DCPW"))
            {
                System.Web.HttpContext.Current.Session.Add("_DivisionName", selectedStoreNumber);
            }
            else
            {
                System.Web.HttpContext.Current.Session.Add("_ChannelName", selectedStoreNumber);
            }

            var returnUri = Request.UrlReferrer; // HttpContext.Request.Url;
            if (returnUri != null && Url.IsLocalUrl(returnUri?.AbsolutePath))
            {
                return Redirect(returnUri?.AbsolutePath);
            }
            else
                return Redirect("/");
        }

        // Action when changing region in drop down list
        [UserFilter(AccessLevel = "Admin,TPC,DD,RM")]
        public async Task<ActionResult> SetNewRegion(string selectedRegionNumber)
        {
            if (System.Web.HttpContext.Current.Session["_RegionNumber"] != null)
            {
                System.Web.HttpContext.Current.Session.Remove("_RegionNumber");
            }
            if (System.Web.HttpContext.Current.Session["_DivisionName"] != null)
            {
                System.Web.HttpContext.Current.Session.Remove("_DivisionName");
            }
            if (System.Web.HttpContext.Current.Session["_ChannelName"] != null)
            {
                System.Web.HttpContext.Current.Session.Remove("_ChannelName");
            }

            System.Web.HttpContext.Current.Session["_AccessArea"] = selectedRegionNumber;

            if (selectedRegionNumber.StartsWith("DCPW"))
            {
                var stores = await _storeManager.GetRegionsForDivision(selectedRegionNumber);

                StoresMenuViewModel storesVm = (StoresMenuViewModel)System.Web.HttpContext.Current.Session["_storesMenu"];
                storesVm.Stores = stores.Select(x => new SelectListItem { Value = "Region " + x.RegionNo.ToString(), Text = "Region " + x.RegionNo }).ToList();
                storesVm.Stores.Add(new SelectListItem { Value = stores[0].Division.ToString(), Text = stores[0].Division.ToString() });
                System.Web.HttpContext.Current.Session.Add("_StoresMenu", storesVm);

                var newSelectedStore = await _storeManager.GetStoreDetails(stores.Select(x => x.CST_CNTR_ID).First());
                if (newSelectedStore != null)
                {
                    System.Web.HttpContext.Current.Session.Add("_StoreDetails", newSelectedStore);
                    System.Web.HttpContext.Current.Session.Add("_ROIFlag", newSelectedStore.Channel == "ROI");
                }

                System.Web.HttpContext.Current.Session.Add("_DivisionName", selectedRegionNumber);
            }
            else
            {
                var stores = await _storeManager.GetStoresForRegion(selectedRegionNumber.Replace("Region ", ""));

                StoresMenuViewModel storesVm = (StoresMenuViewModel)System.Web.HttpContext.Current.Session["_storesMenu"];
                storesVm.Stores = stores.Select(x => new SelectListItem { Value = x.CST_CNTR_ID.ToString(), Text = x.CST_CNTR_ID + " - " + x.StoreName }).ToList();
                storesVm.Stores.Add(new SelectListItem { Value = "Region " + stores[0].RegionNo.ToString(), Text = "Region " + stores[0].RegionNo.ToString() });
                System.Web.HttpContext.Current.Session.Add("_StoresMenu", storesVm);

                var newSelectedStore = await _storeManager.GetStoreDetails(stores.Select(x => x.CST_CNTR_ID).First());
                if (newSelectedStore != null)
                {
                    System.Web.HttpContext.Current.Session.Add("_StoreDetails", newSelectedStore);
                    System.Web.HttpContext.Current.Session.Add("_ROIFlag", newSelectedStore.Channel == "ROI");
                }
            }            

            var returnUri = Request.UrlReferrer; // HttpContext.Request.Url;
            if (returnUri != null && Url.IsLocalUrl(returnUri?.AbsolutePath))
            {
                return Redirect(returnUri?.AbsolutePath);
            }
            else
                return Redirect("/");
        }

        public ActionResult UnknownStore()
        {
            if(TempData["storeFindError"] != null)
            {
                ViewBag.StoreFindError = true;
            }
            return View("UnknownStore");
        }

        public async Task<ActionResult> DuplicateRecords()
        {
            if (TempData["storeSelectError"] != null)
            {
                ViewBag.storeSelectError = true;
            }

            var ip = MvcHelper.GetIPHelper();
            var storeSearch = await _storeManager.GetStoreDetailsFullIP(ip);

            if(storeSearch == null)
            {
                var vm = new StoreListViewModel
                {
                    storeList = mapper.Map<List<StoreViewModel>>(await _storeManager.GetAllStoreDetails(ip))
                };
                return View(vm);
            }
            else
            {
                System.Web.HttpContext.Current.Session.Add("_StoreDetails", storeSearch);
                System.Web.HttpContext.Current.Session.Add("_ROIFlag", storeSearch.Channel == "ROI");
                return RedirectToAction("Index", "Home");
            }
            
        }

        [HttpPost]
        public async Task<ActionResult> DuplicateRecords(int storeSelection)
        {
            //int storeNum;
            //var parseable = int.TryParse(storeSelection.Substring(storeSelection.IndexOf(" ")), out storeNum);
            //if (parseable)
            //{
                await _storeManager.SubmitNewIdStoreRecord(storeSelection, MvcHelper.GetIPHelper());
                System.Web.HttpContext.Current.Session.Remove("_StoreDetails");
                return RedirectToAction("Index", "Home");
            //}
            //TempData["storeSelectError"] = true;

            //return RedirectToAction("DuplicateRecords");
        }

        public async Task<ActionResult> LinkToMedics()
        {
            await _storeManager.LogUnknownBranch(MvcHelper.GetIPHelper(), 9999);
            return Redirect("https://dna.cpwplc.com/DNA/caa/?targetLink=%2FDNA%2Fcf%2Fcastrum%2Fviewer%2Fbasic%2F%3FdocumentId%3D106%26folderId%3D304%26savedFolderId%3D306%26sourceId%3D13622%23documentId%3D106%26folderId%3D304%26savedFolderId%3D306%26sourceId%3D13622");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UnknownStore(string StoreID)
        {
            int StoreNum = Convert.ToInt32(StoreID);

            var data = await _storeManager.GetStoreDetails(StoreNum);

            if(data == null)
            {
                TempData["storeFindError"] = true;
                RedirectToAction("UnknownStore");
            }

            System.Web.HttpContext.Current.Session.Add("_StoreDetails", data);

            await _storeManager.LogUnknownBranch(MvcHelper.GetIPHelper(), StoreNum);

            return RedirectToAction("Index","Home");
        }

        //public async Task<ActionResult> People()
        //{
        //    var currWkNum = DateTime.Now.GetWeekOfYear(ConfigurationManager.AppSettings["FinancialYearStart"]);
        //    var currYr = currWkNum.ToString().Substring(0, 4);
        //    var wk1 = int.Parse(string.Format("{0}01", currYr));
        //    var lastWk = int.Parse(string.Format("{0}52", currYr));

        //    var result = await _peopleManager.GetStoreStaff(StoreNumber);
        //    var dashData = await _dashBoardManager.GetStoreDashBoardData(StoreNumber, wk1, lastWk);
        //    var vm = new PeopleViewModel { Staff = mapper.Map<List<HrFeed>, List<HrFeedView>>(result) };

        //    var currDt = DateTime.Now.GetFirstDayOfWeek();
        //    var kronosEndDate = currDt.AddDays(35);

        //    var kronos = await Utils.GetKronosData(currDt, kronosEndDate, _store.KronosStoreName, isOffice);

        //    for (var i = 1; i < 53; i++)
        //    {
        //        double? hols = null;
        //        var yr = int.Parse(string.Format("{0}{1}", currYr, i.ToString().PadLeft(2, '0')));

        //        if (yr < currWkNum)
        //        {
        //            hols = dashData.FirstOrDefault(x => x.WeekNumber == yr)?.HolidayTaken;
        //        }
        //        else
        //        {
        //            var absent = kronos.Where(x => x.ScheduledDate.GetWeekOfYear(ConfigurationManager.AppSettings["FinancialYearStart"]) == yr && x.IsAbsent).ToList();
        //            hols = absent.Sum(x => Utils.CalculateHours(x.AbscenceAmountInTime, "0:00"));
        //        }

        //        vm.HolidaysTakenForYear.Add(i, hols);
        //    }

        //    vm.PageBlurb = ConfigurationManager.AppSettings["StaffDetailsBlurb"];
        //    return View(vm);
        //}
    }
}