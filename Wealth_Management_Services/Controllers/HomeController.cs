using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using Wealth_Management_Services.Models;
using Wealth_Management_Services.ViewModel;

namespace Wealth_Management_Services.Controllers
{
    public class HomeController : Controller
    {
        // Instantiate the DropdownList
        Departments departments = new Departments();

        public ActionResult Index()
        {
            return View(departments);
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult LogoutUser()
        {
            Session["Password"] = null;
            Session.Abandon();
            return RedirectToAction("Index", "Home", new { area = ""});
        }

    }
}