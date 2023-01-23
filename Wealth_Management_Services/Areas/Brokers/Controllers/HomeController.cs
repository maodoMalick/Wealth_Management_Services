using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wealth_Management_Services.Models;
using Wealth_Management_Services.ViewModel;

namespace Wealth_Management_Services.Areas.Brokers.Controllers
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
        public ActionResult Login(broker broker)
        {
            if (broker != null)
            {
                // Get Authentication result from Database
                broker bkr = dataContext.Broker_Login(broker);

                if (bkr != null)
                {
                    // Data to be sent to the View
                    MyViewModel.broker = bkr;
                    MyViewModel.Welcome = "Welcome to your Dashboard " + bkr.name;

                    // Send Authenticated user to his/her Dashboard
                    return View("~/Views/Home/Dashboard.cshtml", MyViewModel.broker);
                }
                else
                {
                    // Not authenticated message back to view
                    MyViewModel.Warning = "Invalid username or password";
                }
            }

            // If it fails, stay in this same Login page
            return View("Index");
        }

        public ActionResult Registration()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Registration(broker broker)
        {
            if (ModelState.IsValid)
            {
                // Is user registration accepted?
                int returnCode = dataContext.Broker_Registration(broker);

                if (returnCode == 1)
                {
                    // Success! User will be sent to the Broker login page
                    // *** CREATE A JS ALERT FOR SUCCESS
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

        // Client-Side Validation 'for Registration' to prevent username 'duplication' (see username field in 'broker.custom.cs')
        public JsonResult IsUsernameValid(string username)
        {
            // Will return the opposite of the logic (to be true)
            return Json(!DataConnector.brokers.Any(x => x.username == username), JsonRequestBehavior.AllowGet);
        }
    }
}