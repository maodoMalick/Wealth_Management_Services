using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wealth_Management_Services.Models;

namespace Wealth_Management_Services.Areas.Management.Controllers
{
    public class HomeController : Controller
    {
        // GET: Management/Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(management management)
        {
            return View("~/Views/Home/Dashboard.cshtml");
        }

        public ActionResult Registration()
        {
            return View();
        }
    }
}