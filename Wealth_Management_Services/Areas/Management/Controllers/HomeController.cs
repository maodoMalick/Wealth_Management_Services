using System.Linq;
using System.Web.Mvc;
using Wealth_Management_Services.Models;
using Wealth_Management_Services.ViewModel;


namespace Wealth_Management_Services.Areas.Management.Controllers
{
    public class HomeController : Controller
    {
        // Entity Framework Data Object
        DataContext dataContext = new DataContext();

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
                // Get Authentication from Database
                int result = dataContext.Management_Login(mgmt);
                //MyViewModel.Warning = result.ToString();
                if (result == 1)
                {
                    //string title = mgmt.gender ? "Male" : "Female";
                    management managerial = DataConnector.managements.SingleOrDefault(x => x.username == mgmt.username);
                    MyViewModel.management = managerial;
                    MyViewModel.Welcome = "Welcome to your Dashboard " + (managerial.name);
                    // Send Authenticated user to his/her Dashboard
                    return View("~/Views/Home/Dashboard.cshtml");
                }
                else
                {
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