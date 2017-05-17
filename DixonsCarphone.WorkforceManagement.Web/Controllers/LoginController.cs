using DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels;
using DixonsCarphone.WorkforceManagement.ViewModels;
using DixonsCarphone.WorkforceManagement.Web.App_Start;
using DixonsCarphone.WorkforceManagement.Web.Models;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.Web.Controllers
{
    public class LoginController : BaseController
    {
        [AllowAnonymous]
        public virtual ActionResult Index(bool o = false)
        {
            ViewBag.ReturnUrl = Request.QueryString["returnUrl"];
            if (!o)
            {
                ViewBag.SecureCheck = true;
            }            
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Index(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                if (string.Equals(Request.UrlReferrer.AbsolutePath.ToString(), "/oos", StringComparison.CurrentCultureIgnoreCase))
                {
                    TempData["modelPass"] = model;
                    return RedirectToAction("Index", "OOS");
                }
                return View(model);
            }

            // usually this will be injected via DI. but creating this manually now for brevity
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            var authService = new AdAuthenticationService(authenticationManager);

            var authenticationResult = authService.SignIn(model.Username, model.Password);
            if (authenticationResult.IsSuccess)
            {
                //System.Web.HttpContext.Current.Session.Add("_UserName", model.Username);
                var user = System.Web.HttpContext.Current.Session["_UserName"].ToString();
                var _accessLevel = await _storeManager.GetAuthLevel(user);
                if(_accessLevel.Count() > 0)
                {
                    System.Web.HttpContext.Current.Session["_AccessLevel"] = _accessLevel.First().AccessLevel;
                    if(_accessLevel.First().AccessLevel == "Admin" || _accessLevel.First().AccessLevel == "TPC")
                    {
                        System.Web.HttpContext.Current.Session["_AccessArea"] = "101";
                    }
                    else if(_accessLevel.First().AccessLevel == "OHAdmin")
                    {
                        System.Web.HttpContext.Current.Session["_AccessArea"] = "101";
                    }
                    else
                    {
                        System.Web.HttpContext.Current.Session["_AccessArea"] = _accessLevel.First().Area;
                        if (_accessLevel.Count() > 1)
                        {
                            System.Web.HttpContext.Current.Session["_SecAccessArea"] = _accessLevel[1].Area;
                        }
                    }
                }

                await GetStores();
                // we are in!
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    //returnUrl = new Uri(returnUrl)?.AbsolutePath;
                    return RedirectToLocal(returnUrl);
                }
                else
                    return RedirectToAction("Index", "Home");

            }
            
            if (string.Equals(Request.UrlReferrer.AbsolutePath.ToString(), "/oos", StringComparison.CurrentCultureIgnoreCase))
            {
                TempData["modelPass"] = model;
                TempData["errorMessage"] = authenticationResult.ErrorMessage;
                return RedirectToAction("Index", "OOS");
            }
            ModelState.AddModelError("", authenticationResult.ErrorMessage);
            return View(model);
        }

        //[ValidateAntiForgeryToken]
        public virtual ActionResult Logoff()
        {
            //IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            //authenticationManager.SignOut(CpwWfmAuthentication.ApplicationCookie);

            //Session.Abandon();
            MvcHelper.LogOut();

            return RedirectToAction("Index", "Home");
        }

        private async Task GetStores()
        {
            var AccessLevel = System.Web.HttpContext.Current.Session["_AccessLevel"].ToString();
            var AccessArea = System.Web.HttpContext.Current.Session["_AccessArea"].ToString();

            if (AccessLevel == "RM")
            {
                var stores = await _storeManager.GetStoresForRegion(AccessArea);
                var storesVm = new StoresMenuViewModel { Stores = stores.Select(x => new SelectListItem { Value = x.CST_CNTR_ID.ToString(), Text = x.CST_CNTR_ID + " - " + x.StoreName }).ToList() };
                storesVm.Stores.Add(new SelectListItem { Value = "Region " + stores[0].RegionNo.ToString(), Text = "Region " + stores[0].RegionNo.ToString() });
                System.Web.HttpContext.Current.Session.Add("_StoresMenu", storesVm);

                if (System.Web.HttpContext.Current.Session["_SecAccessArea"] != null)
                {
                    storesVm.Regions = new List<SelectListItem>();
                    storesVm.Regions.Add(new SelectListItem { Value = "Region " + AccessArea, Text = "Region " + AccessArea });
                    storesVm.Regions.Add(new SelectListItem { Value = "Region " + System.Web.HttpContext.Current.Session["_SecAccessArea"].ToString(), Text = "Region " + System.Web.HttpContext.Current.Session["_SecAccessArea"].ToString() });
                }

                var newSelectedStore = await _storeManager.GetStoreDetails(stores.Select(x => x.CST_CNTR_ID).First());
                if (newSelectedStore != null)
                {
                    System.Web.HttpContext.Current.Session.Add("_StoreDetails", newSelectedStore);
                    System.Web.HttpContext.Current.Session.Add("_ROIFlag", newSelectedStore.Channel == "ROI");
                }
            }
            else if(AccessLevel == "DD")
            {
                var regions = await _storeManager.GetRegionsForDivision(AccessArea);
                regions = regions.OrderBy(x => x.RegionNo).ToList();
                var stores = await _storeManager.GetStoresForRegion(regions.First().RegionNo);
                
                var storesVm = new StoresMenuViewModel {
                    Stores = regions.Select(x => new SelectListItem { Value = "Region " + x.RegionNo, Text = "Region " + x.RegionNo }).ToList(),
                    Regions = regions.Select(x => new SelectListItem { Value = "Region " + x.RegionNo, Text = "Region " + x.RegionNo }).ToList()
                };
                storesVm.Stores.Add(new SelectListItem { Value = stores[0].Division.ToString(), Text = stores[0].Division.ToString() });
                storesVm.Regions.Add(new SelectListItem { Value = regions.First().Division.ToString(), Text = regions.First().Division.ToString() });
                System.Web.HttpContext.Current.Session.Add("_StoresMenu", storesVm);
                var newSelectedStore = await _storeManager.GetStoreDetails(stores.Select(x => x.CST_CNTR_ID).First());
                if (newSelectedStore != null)
                {
                    System.Web.HttpContext.Current.Session.Add("_StoreDetails", newSelectedStore);
                    System.Web.HttpContext.Current.Session.Add("_ROIFlag", newSelectedStore.Channel == "ROI");
                }
                System.Web.HttpContext.Current.Session.Add("_DivisionName", AccessArea);
            }
            else if(AccessLevel == "RD")
            {
                var stores = await _storeManager.GetDivisionsForChannel(AccessArea);
                var storesVm = new StoresMenuViewModel { Stores = new List<SelectListItem>() };
                
                foreach(var item in stores)
                {
                    storesVm.Stores.Add(new SelectListItem { Value = item.Division, Text = item.Division });
                }

                storesVm.Stores.Add(new SelectListItem { Value = stores[0].Channel.ToString(), Text = stores[0].Channel.ToString() });
                System.Web.HttpContext.Current.Session.Add("_StoresMenu", storesVm);
                System.Web.HttpContext.Current.Session.Add("_DivisionName", stores.First().Division.ToString());
            }
            else if(AccessLevel == "Admin" || AccessLevel == "TPC")
            {
                var stores = await _storeManager.GetStoresForRegion(AccessArea);
                var regions = await _storeManager.GetAllRegions();
                regions = regions.OrderBy(x => (int)Enum.Parse(typeof(RegionOrder), x.Channel, true)).ThenBy(x => x.RegionNo).ToList();
                var storesVm = new StoresMenuViewModel {
                    Stores = stores.Select(x => new SelectListItem { Value = x.CST_CNTR_ID.ToString(), Text = x.CST_CNTR_ID + " - " + x.StoreName }).ToList(),
                    Regions = regions.Select(x => new SelectListItem { Value = x.RegionNo, Text = x.RegionNo}).ToList()
                };
                storesVm.Stores.Add(new SelectListItem { Value = "Region " + stores[0].RegionNo.ToString(), Text = "Region " + stores[0].RegionNo.ToString() });
                System.Web.HttpContext.Current.Session.Add("_StoresMenu", storesVm);

                var newSelectedStore = await _storeManager.GetStoreDetails(stores.Select(x => x.CST_CNTR_ID).First());
                if (newSelectedStore != null)
                {
                    System.Web.HttpContext.Current.Session.Add("_ROIFlag", newSelectedStore.Channel == "ROI");
                    System.Web.HttpContext.Current.Session.Add("_StoreDetails", newSelectedStore);
                }
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }


    public class LoginViewModel
    {
        [Required, AllowHtml]
        public string Username { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}