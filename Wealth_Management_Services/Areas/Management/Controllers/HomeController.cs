using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wealth_Management_Services.Models;
using System.Threading;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;



namespace Wealth_Management_Services.Areas.Management.Controllers
{
    public class HomeController : Controller
    {
        // 
        DataContext dataContext = new DataContext();

        public ActionResult Index()
        {
            return View();
        }

        // User Login
        [HttpPost]
        public ActionResult Login(management management)
        {
            return View("~/Views/Home/Dashboard.cshtml");
        }

        public ActionResult Registration()
        {
            return View();
        }

        // New User Registration
        [HttpPost]
        public ActionResult Registration(management mgmt)
        {
            if (ModelState.IsValid)
            {
                //Get results from Database
                //Thread thread = new Thread(dataContext.Management_Registration(mgmt))
                int result = dataContext.Management_Registration(mgmt);
                //Validate the user is registered
                if (result == 1)
                {
                    return RedirectToAction("Index", "Home", new { area = "Management" });
                }
                else
                {
                    ViewBag.Warning = "Username is already taken";
                }
            }
            
            return View();
        }


        

    }
}