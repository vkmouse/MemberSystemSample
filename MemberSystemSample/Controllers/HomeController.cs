using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MemberSystemSample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Member()
        {
            return View();
        }

        public ActionResult Error(string message)
        {
            ViewBag.Message = message;

            return View();
        }

        public class LoginInfo
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        [HttpPost]
        public ActionResult Login(LoginInfo info)
        {
            var username = info.Username;
            var password = info.Password;

            if (validateUserLogin(username, password))
            {
                Session["IsLoggedIn"] = true;
                return Json(new { success = true });
            } 
            else
            {
                return Json(new { success = false, message = "帳號或密碼輸入錯誤" });
            }
        }

        [HttpPost]
        public ActionResult Logout()
        {
            if (isLoggedIn())
            {
                Session.Remove("IsLoggedIn");
            }
            
            return Json(new { success = true });
        }

        public ActionResult UserInfo()
        {
            if (isLoggedIn())
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false}, JsonRequestBehavior.AllowGet);
            }
        }

        private bool validateUserLogin(string username, string password)
        {
            return username == "test" && password == "test";
        }

        private bool isLoggedIn()
        {
            return Session["IsLoggedIn"] != null && (bool)Session["IsLoggedIn"];
        }
    }
}