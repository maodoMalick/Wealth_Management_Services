using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wealth_Management_Services.Models;
using Wealth_Management_Services.ViewModel;

namespace Wealth_Management_Services.Areas.Investors.Controllers
{
    public class HomeController : Controller
    {
        // Data Object
        DataContext dataContext = new DataContext();

        // Entity Framework Data Connection
        DataConnection DataConnector = new DataConnection();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(investor invest)
        {
            // Get Authentication result from Database
            string result = dataContext.Investor_Login(invest);

            // User will be redirected based on the returned data
            switch (result)
            {
                case "Account_Locked":
                    // Failed! Will return to the Login page
                    return View("Index");
                case "Failed_Login":
                    // Failed! Will return to the Login page
                    return View("Index");
                case "Authenticated":
                    // Success!
                    investor investor = MyViewModel.investor;
                    MyViewModel.Welcome = "Welcome to your Dashboard " + investor.firstName;
                    return View("~/Views/Home/Dashboard.cshtml", MyViewModel.investor);
                default:
                    // Will return to the Login page
                    return View("Index");
            }
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(investor investor)
        {
            if (ModelState.IsValid)
            {
                int returnCode = dataContext.Investor_Registration(investor);

                if (returnCode == 1)
                {
                    // Success! User will be sent to the Broker login page
                    return RedirectToAction("Index");
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