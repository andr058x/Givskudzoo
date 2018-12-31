using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using GivskudZoo.Bll;
using GivskudZoo.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;

namespace GivskudZoo.Web.Controllers  //Here we have the ActionResult that they are public methods
{
    public class UserController : Controller
    {
        private readonly UserManager _manager;

        public UserController()
        {
            _manager = new UserManager();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return PartialView("_Login");
        }

        [HttpPost]
        public JsonResult Login(UserModel user)
        {
            if (!ModelState.IsValid)
                return Json(new ResultModel
                {
                    Error = "Data model is invalid!"
                });

            if (!_manager.CheckIfUserExists(user.UserName, user.Password))
                return Json(new ResultModel
                {
                    Error = "Credentials are invalid!"
                });

            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
            }, "ApplicationCookie");

            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignIn(new AuthenticationProperties {IsPersistent = user.RememberMe}, identity);

            return Json(new ResultModel());
        }

        [HttpPost]
        public JsonResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie,
                                                DefaultAuthenticationTypes.ExternalCookie);
            return Json(true);
        }
    }
}