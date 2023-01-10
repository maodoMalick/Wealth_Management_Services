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
        DataConnector DataConnector = new DataConnector();

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
                    // Get the corresponding row from the database
                    management manager = DataConnector.managements.SingleOrDefault(x => x.username == mgmt.username && x.password == mgmt.password);
                    // Data to be sent to the View
                    MyViewModel.management = manager;
                    //ViewBag.management = manager;
                    MyViewModel.Welcome = "Welcome to your Dashboard " + manager.name;

                    // Send Authenticated user to his/her Dashboard
                    return View("~/Views/Home/Dashboard.cshtml", MyViewModel.management);
                }
                else
                {
                    // Not authenticated message back to view
                    MyViewModel.Warning = "Invalid username or password";
                }
            }

            return View("Index");
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