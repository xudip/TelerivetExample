using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerivet.Client;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using TelerivetExample.Models;
using TelerivetExample.Services;
using TelerivetExample.Managers;
using System.Web.Security;

namespace TelerivetExample.Controllers
{
    [Authorize]
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles="Admin")]
        [HandleError]
        public ActionResult User()
        {
            ViewBag.Users = UserManager.SelectAll();
            return View();
        }

        public ActionResult Project()
        {
            ViewBag.UserProjects = UserProjectManager.SelectAll();
            return View();
        }

        public ActionResult Event()
        {
            ViewBag.Events = EventManager.SelectAll();
            return View();
        }

        [AllowAnonymous]
        public ActionResult TelerivetWebHook()
        {
            TelerivetService telerivetService = new TelerivetService();
            telerivetService.HandleIncomingMessage(Request);
            telerivetService.HandleProjectRegistrationAsync("test", new string[] { });
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult MockSMSSend(Event evt)
        //{
        //    TelerivetService telerivetService = new TelerivetService();
        //    var sendMsgResponse = telerivetService.SendMockData(evt);
        //    return RedirectToAction("TelerivetWebHook", "Home");
        //}
    }
}