using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Wealth_Management_Services.Models;

namespace Wealth_Management_Services.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View(new Departments());
        }

        public ActionResult Dashboard()
        {
            return View();
        }
    }
}