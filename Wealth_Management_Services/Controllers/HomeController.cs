using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Wealth_Management_Services.Models;
using Wealth_Management_Services.ViewModel;

namespace Wealth_Management_Services.Controllers
{
    public class HomeController : Controller
    {
        // Data Object
        DataContext dataContext = new DataContext();

        // Entity Framework Data Connection
        DataConnection DataConnector = new DataConnection();

        public ActionResult Index()
        {
            return View(new Departments());
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        //public PartialViewResult Investments()
        //{
        //    List<investor> investors = DataConnector.investors.ToList();
        //    return PartialView("_MinStocks", investors);
        //    //return PartialView("~/Views/Shared/_MinStocks.cshtml", investors);
        //    //return PartialView("~/Views/Shared/_InvestorPartial.cshtml");
        //}

    }
}