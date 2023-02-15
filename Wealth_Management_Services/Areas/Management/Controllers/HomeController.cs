using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wealth_Management_Services.Models;
using Wealth_Management_Services.ViewModel;


namespace Wealth_Management_Services.Areas.Management.Controllers
{
    public class HomeController : Controller
    {
        // Data Object
        DataContext dataContext = new DataContext();

        // Entity Framework Data Connection
        DataConnection DataConnector = new DataConnection();

        public ActionResult Index()
        {
            MyViewModel.Warning = "";
            return View();
        }

        // User Login
        [HttpPost]
        public ActionResult Login(management mgmt)
        {
            if (mgmt != null)
            {
                // Get Authentication result from Database
                int result = dataContext.Management_Login(mgmt);
                
                if (result == 1)
                {
                    // Data to be sent to the View
                    management manager = MyViewModel.management;
                    MyViewModel.Welcome = "Welcome Manager: " + manager.name;

                    // Send Authenticated user to his/her Dashboard
                    return View("~/Views/Home/Dashboard.cshtml", MyViewModel.management);
                }
                else
                {
                    // Not authenticated message back to the View
                    MyViewModel.Warning = "Invalid username or password";
                }
            }

            // If it fails, stay in this same Login page
            return View("Index");
        }

        // METHODS FOR THE 'AJAX' SECTION IN MANAGEMENT DASHBOARD
        public PartialViewResult ClientsList()
        {
            // Display title with results
            MyViewModel.Message = "List of all Clients";
            // Display the list of all investors
            List<investor> investors = DataConnector.investors.ToList();
            return PartialView("_ClientsList", investors);
        }

        public PartialViewResult ClientsChart()
        {
            // Display title with results
            MyViewModel.Message = "Investors Assets";
            List<string> firstNames = DataConnector.investors.Select(x => x.firstName).ToList();
            List<decimal?> capitals = DataConnector.investors.Select(x => x.capital).ToList();
            ViewBag.FIRSTNAMES = firstNames;
            ViewBag.CAPITALS = capitals;

            return PartialView("_Graph_Investors");
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

        public PartialViewResult BrokersList()
        {
            // Display title with results
            MyViewModel.Message = "List of all Brokers";
            // Display the list of all brokers
            List<broker> brokers = DataConnector.brokers.ToList();
            return PartialView("_BrokersList", brokers);
        }

        public PartialViewResult Transactions()
        {
            // Display title with results
            MyViewModel.Message = "All Brokers Financial Transactions";
            // Display the list of all brokers
            List<mgmtBillboard> billboard = DataConnector.mgmtBillboards.ToList();
            return PartialView("_TransactionsReport", billboard);
        }

        public PartialViewResult BrokersInfo()
        {
            // Display the list of all brokers
            List<broker> brokers = DataConnector.brokers.ToList();
            return PartialView("_BrokersInfo", brokers);
        }

        public PartialViewResult BrokersPerformance()
        {
            // Display title with results
            MyViewModel.Message = "Brokers Performance";
            // Display the list of all brokers
            List<decimal?> dividends = DataConnector.investors.Select(x => x.latestDividend).ToList();
            List<decimal?> capitals = DataConnector.investors.Select(x => x.capital).ToList();
            ViewBag.Dividends = dividends;
            ViewBag.BrokerCapitals = capitals;

            return PartialView("_BrokersInfo");
        }

        // REGISTRATION
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

        // Client-Side Validation 'for Registration' to prevent username 'duplication' (see username field in 'management.custom.cs')
        public JsonResult IsUsernameValid(string username)
        {
            // Will return the opposite of the logic (to be true)
            return Json(!DataConnector.managements.Any(x => x.username == username), JsonRequestBehavior.AllowGet);
        }

    }
}