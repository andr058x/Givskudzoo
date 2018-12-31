using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GivskudZoo.Web.Controllers
{
    public class HomeController : Controller  //Here we have the ActionResult that they are public methods
    {
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Events()
        {
            return View();
        }

        public ActionResult CreateEvent()
        {
            return View();
        }

        public ActionResult News()
        {
            return View();
        }

        public ActionResult Article()
        {
            return View();
        }
        public ActionResult Schedule()
        {
            return View();
        }
        public ActionResult CreateActivity()
        {
            return View();
        }
        public ActionResult EditActivity(int id)
        {
            return View();
        }
        public ActionResult Options()
        {
            return View();
        }
    }
}