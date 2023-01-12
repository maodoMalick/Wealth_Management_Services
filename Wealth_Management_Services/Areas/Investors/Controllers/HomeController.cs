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
            string result = dataContext.Investor_Login(invest);

            switch (result)
            {
                case "Account_Locked":
                    return View("Index");
                    //break;
                case "Failed_Login":
                    return View("Index");
                    //break;
                case "Authenticated":
                    //investor investor = DataConnector.investors.SingleOrDefault(x => x.username == invest.username && x.password == invest.password);
                    investor investor = MyViewModel.investor;
                    MyViewModel.Welcome = "Welcome to your Dashboard " + investor.firstName;
                    return View("~/Views/Home/Dashboard.cshtml", MyViewModel.investor);
                default: 
                    return View("Index");
            }

            //return View();
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