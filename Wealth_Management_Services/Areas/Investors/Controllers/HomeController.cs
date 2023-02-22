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
            // Reset the warning message
            MyViewModel.Warning = "";
            return View();
        }

        // LOGIN
        [HttpPost]
        public ActionResult Login(investor invest)
        {
            MyViewModel.Warning = "";

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
                    ViewBag.SingleInvestorData = dataContext.Investor_Data( investor.id);
                    //MyViewModel.Investor_ArrList = dataContext.Investor_Data(investor.id);
                    MyViewModel.Welcome = "Welcome investor: " + investor.firstName;
                    MyViewModel.UserId = investor.id; // user id will be needed for the Ajax link in the View
                    MyViewModel.Username = investor.username;
                    return View("~/Views/Home/Dashboard.cshtml", MyViewModel.investor);
                default:
                    // Will return to the Login page
                    return View("Index");
            }
        }

        public PartialViewResult Investments()
        {
            List<investor> investors = DataConnector.investors.ToList();
            return PartialView("~/Views/Shared/_MinStocks.cshtml", investors);
        }

        // METHODS FOR THE 'AJAX' SECTION IN MANAGEMENT DASHBOARD
        [HttpPost]
        public PartialViewResult MyMoneyChart(int MyId)
        {
            // Display title with results
            MyViewModel.Message = "Total Assets in 2022";
            List<decimal> Dividends = dataContext.MyDividends(MyViewModel.UserId); // User 'id' collected from Login ActionMethod
            int num1 = Dividends.Count();
            //var Dividends1 = (from d in dataContext.MyDividends
            //                 where d.id == 1
            //                 select d);
            List<string> FirstNames = dataContext.FirstNames();
            // Data to be feed to the Pie Chart
            ViewBag.DIVIDENDS = Dividends;
            ViewBag.FIRSTNAMES = FirstNames;

            //decimal num = Dividends[1];
            ViewBag.NUM = num1;
            ViewBag.ID = MyId;
            return PartialView("_Graph_Lines_MyMoney");
        }

        public PartialViewResult MyBrokerProfile()
        {
            return PartialView("_Graph_Lines_MyMoney");
        }

        // REGISTRATION 
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

        // Client-Side Validation 'for Registration' to prevent username 'duplication' (see username field in 'investor.custom.cs')
        public JsonResult IsUsernameValid(string username)
        {
            // Will return the opposite of the logic (to be true)
            return Json(!DataConnector.investors.Any(x => x.username == username), JsonRequestBehavior.AllowGet);
        }
    }
}