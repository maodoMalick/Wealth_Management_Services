using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wealth_Management_Services.Areas.Brokers.Controllers
{
    public class HomeController : Controller
    {
        // GET: Brokers/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}