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
using Wealth_Management_Services.ViewModel;

namespace Wealth_Management_Services.Areas.Management.Controllers
{
    public class HomeController : Controller
    {
        // Entity Framework Data Object
        DataContext dataContext = new DataContext();

        public ActionResult Index()
        {
            return View();
        }

        // User Login
        [HttpPost]
        public ActionResult Login(management mgmt)
        {
            if (ModelState.IsValid)
            {
                // Get Authentication from Database
                int result = dataContext.Management_Login(mgmt);

                if (result == 1)
                {
                    MyViewModel.Welcome = "Welcome to your Dashboard " + mgmt.name;
                    // Send Authenticated user to his/her Dashboard
                    return View("~/Views/Home/Dashboard.cshtml", mgmt);
                }
                else
                {
                    MyViewModel.Warning = "Invalid username or password";
                }
            }

            return View();
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
                //int result = 0;
                //Thread thread = new Thread(result = dataContext.Management_Registration(mgmt));
                int result = dataContext.Management_Registration(mgmt);
                //Validate the user is registered
                if (result == 1)
                {
                    return RedirectToAction("Index", "Home", new { area = "Management" });
                }
                else
                {
                    // Error to be sent back to the Broker Registration page
                    MyViewModel.Warning = "Error! Username is already taken.";
                }
            }
            
            return View();
        }

       
        

    }
}