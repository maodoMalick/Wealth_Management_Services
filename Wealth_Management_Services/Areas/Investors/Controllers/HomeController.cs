using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wealth_Management_Services.Models;

namespace Wealth_Management_Services.Areas.Investors.Controllers
{
    public class HomeController : Controller
    {
        // GET: Investors/Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(investor investor)
        {
            return View("~/Views/Home/Dashboard.cshtml");
        }

        public ActionResult Registration()
        {
            return View();
        }
    }
}