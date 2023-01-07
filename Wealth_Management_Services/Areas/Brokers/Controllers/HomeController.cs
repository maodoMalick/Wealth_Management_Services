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
        // Entity Framework Data Object
        DataContext dataContext = new DataContext();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(broker broker)
        {
            return View("~/Views/Home/Dashboard.cshtml");
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

                if (returnCode > 0)
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