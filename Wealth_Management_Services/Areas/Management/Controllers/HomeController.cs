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