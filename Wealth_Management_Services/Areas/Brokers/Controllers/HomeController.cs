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
            MyViewModel.Welcome = "";
            MyViewModel.Thanks = "";
            MyViewModel.InvestorWarning = "";
            return View("Index");
        }

        public ActionResult Login()
        {
            return View("Dashboard");
        }

        [HttpPost]
        public ActionResult Login(broker broker)
        {
            broker bkr = null;

            try
            {
                if (broker != null)
                {
                    // Get Authentication result from Database
                    int result = dataContext.Broker_Login(broker);

                    if (result == 1)
                    {
                        // Data from 'Broker_Login()' method to be sent to the View
                        bkr = MyViewModel.broker;
                        MyViewModel.Welcome = "Welcome Broker: " + bkr.name;

                        MyViewModel.UserId = bkr.id; // Needed for 'ClientsList()' method
                        Session["Password"] = bkr.password; // Needed for Logout button on Dashboard

                        //Send Authenticated broker user to the 'Main' Dashboard
                        return View("Dashboard", bkr);
                    }
                    else
                    {
                        // Not authenticated message back to the View
                        MyViewModel.InvestorWarning = "Invalid username or password";
                    }
                }
                // If it fails, stay in this same Login page
                return View("Index", bkr);
            }
            catch (Exception)
            {
                MyViewModel.Warning = "You must fill all fields.";
                return View("Index");
            }
        }

        // METHODS FOR THE 'AJAX' SECTION IN BROKER DASHBOARD
        public PartialViewResult MyClientsList()
        {
            // Display title with results
            MyViewModel.Message = "List of My Clients";
            // Retrieve the broker's id from Login method
            int MyId = MyViewModel.UserId;
            // Display the list of all investors
            List<investor> my_investors = DataConnector.investors.Where(x => x.brokerID == MyId).ToList();
            return PartialView("_ClientsList", my_investors);
        }

        public PartialViewResult HighestDividend()
        {
            // Display title with results
            MyViewModel.Message = "Clients with Highest Dividends";
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

        // STOCKS & BONDS PAGES
        public PartialViewResult GotoMarketplace()
        {
            // Display title with results
            MyViewModel.Message = "Stocks & Bonds Marketplace";
            // Display the list of all investors
            return PartialView("_StocksPage");
        }

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
            Session["brokerId"] = MyViewModel.UserId;
            return PartialView("_BuyingPage");
        }

        [HttpPost]
        public ActionResult Purchasing(brokerOperation bkrOps)
        {
            try
            {
                if (bkrOps != null)
                {
                    dataContext.PurchasingAssets(bkrOps);
                    MyViewModel.Message = "Stocks & Bonds Marketplace";
                    MyViewModel.Thanks = "Thank you for your Purchase";
                    return View("Index");
                }

                MyViewModel.InvestorWarning = "All Stock Purchase fields must be filled. Please Log in again.";
            }
            catch (Exception)
            {
                MyViewModel.InvestorWarning = "There is an Error. Please Log in again.";
                RedirectToAction("Login");
            }

            return View("Index");
        }

        // REGISTRATION 
        public ActionResult Registration()
        {
            MyViewModel.Warning = "";
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