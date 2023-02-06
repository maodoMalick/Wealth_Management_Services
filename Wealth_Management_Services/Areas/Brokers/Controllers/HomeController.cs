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
            return View("Index");
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
                    MyViewModel.Welcome = "Welcome " + bkr.name;

                    // Send Authenticated broker user to the 'Main' Dashboard
                    return View("Dashboard", bkr);
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

        // For 'Ajax' section in Broker Dashboard
        public PartialViewResult ClientsList()
        {
            // Display title with results
            MyViewModel.Message = "List of all Clients";
            // Display the list of all investors
            List<investor> investors = DataConnector.investors.ToList();
            return PartialView("_ClientsList", investors);
        }

        public PartialViewResult GotoMarketplace()
        {
            // Display title with results
            MyViewModel.Message = "Stocks & Bonds Marketplace";
            // Display the list of all investors
            return PartialView("_StocksPage");
        }

        public PartialViewResult HighestDividend()
        {
            // Display title with results
            MyViewModel.Message = "List of Best Clients";
            // Show investors with higher returns
            IEnumerable<investor> investors = from i in DataConnector.investors
                                              where i.latestDividend >= 3200
                                              select i;

            return PartialView("_ClientsList", investors);
        }

        public PartialViewResult ClientsByGender()
        {
            // Display title with results
            MyViewModel.Message = "Clients by Gender";
            // Show investors with higher returns
            IEnumerable<investor> investors = from i in DataConnector.investors
                                              where i.gender == "male"
                                              select i;

            return PartialView("_ClientsList", investors);
        }

        public PartialViewResult ClientsBySeniority()
        {
            // Display title with results
            MyViewModel.Message = "Clients by Seniority";
            // Show investors with higher returns
            IEnumerable<investor> investors = from i in DataConnector.investors
                                              orderby i.memberSince ascending
                                              select i;
            
            return PartialView("_ClientsList", investors);
        }

        // STOCKS & BONDS PAGE
        public PartialViewResult GetStocksList()
        {
            MyViewModel.Message = "Stocks & Bonds Marketplace";
            return PartialView("_StocksPage");
        }

        public PartialViewResult GetBondsList()
        {
            MyViewModel.Message = "Stocks & Bonds Marketplace";
            return PartialView("_BondsPage");
        }

        public PartialViewResult Purchasing()
        {
            MyViewModel.Message = "Stocks & Bonds Marketplace";
            return PartialView("_PurchasingPage");
        }

        // REGISTRATION 
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