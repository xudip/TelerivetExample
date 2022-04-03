using System.Web.Mvc;

namespace TelerivetExample.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Error()
        {
            return View();
        }
        public ActionResult NoPage()
        {
            return View();
        }
        public ActionResult Unauthorized()
        {
            return View();
        }
    }
}
