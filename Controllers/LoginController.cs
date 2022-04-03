using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using TelerivetExample.Managers;
using TelerivetExample.Models;

namespace TelerivetExample.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index(string ReturnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            TempData["ReturnUrl"] = ReturnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(LoginModel model, string ReturnUrl)
        {
            string message = "";

            int userID = UserManager.GetUserByPhoneAndPin(model);
            if (userID > 0)
            {
                //var ticket = new FormsAuthenticationTicket(userID.ToString(), true, 60);
                //string encrypted = FormsAuthentication.Encrypt(ticket);
                //var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                //cookie.Expires = DateTime.Now.AddDays(1);
                //cookie.Secure = true;
                //Response.Cookies.Add(cookie);
                FormsAuthentication.SetAuthCookie(userID.ToString(), false);


                if (Url.IsLocalUrl(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    //return RedirectToRoute("~/plesk-site-preview/aloi.driving-school-system.com/Home");
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                message = "Invalid credential Provided";
            }


            ViewBag.Message = message;

            return View();

        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            //return Redirect("~/plesk-site-preview/aloi.driving-school-system.com/Login");
            return View();
        }

    }
}
