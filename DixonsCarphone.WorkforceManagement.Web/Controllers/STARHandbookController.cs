using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.Web.Controllers
{
    public class STARHandbookController : Controller
    {
        // GET: STARHandbook
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult KronosTerminal(int articleNumber = 0)
        {
            if(articleNumber == 0)
            {
                return View();
            }
            else
            {
                return View("KronosTerminal" + articleNumber.ToString());
            }
            
        }

        public ActionResult Scheduling(int articleNumber = 0)
        {
            if (articleNumber == 0)
            {
                return View();
            }
            else
            {
                return View("Scheduling" + articleNumber.ToString());
            }

        }

        public ActionResult ManagingTimecards(int articleNumber = 0)
        {
            if (articleNumber == 0)
            {
                return View();
            }
            else
            {
                return View("ManagingTimecards" + articleNumber.ToString());
            }

        }

        public ActionResult TimecardExceptions(int articleNumber = 0)
        {
            if (articleNumber == 0)
            {
                return View();
            }
            else
            {
                return View("TimecardExceptions" + articleNumber.ToString());
            }

        }

        public ActionResult TimecardSignOff(int articleNumber = 0)
        {
            if (articleNumber == 0)
            {
                return View();
            }
            else
            {
                return View("TimecardSignOff" + articleNumber.ToString());
            }

        }

        public ActionResult EscalationsSupport(int articleNumber = 0)
        {
            if (articleNumber == 0)
            {
                return View();
            }
            else
            {
                return View("EscalationsSupport" + articleNumber.ToString());
            }

        }

        public ActionResult Availability(int articleNumber = 0)
        {
            if (articleNumber == 0)
            {
                return View();
            }
            else
            {
                return View("Availability" + articleNumber.ToString());
            }

        }

        public ActionResult TrainingPacks()
        {
            return View();
        }
    }
}