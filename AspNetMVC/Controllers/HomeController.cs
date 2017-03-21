using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Redirect(Url.Action("About"));
        }

        public ActionResult About()
        {
            ViewBag.Title = "關於本站";
            ViewBag.Message = "Your application description page.";

            return View();
        }

    }
}