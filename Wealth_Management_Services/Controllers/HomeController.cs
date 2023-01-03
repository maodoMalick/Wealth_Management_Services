using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wealth_Management_Services.Models;

namespace Wealth_Management_Services.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //Departments departments = new Departments();
            //return View(departments.getDepartments());
            //IEnumerable<SelectListItem> selectListItems = Departments.getDepartments;
            return View(new Departments());
        }

        //[HttpPost]
        //public ActionResult Index(string value)
        //{
        //    switch (value)
        //    {
        //        case "investors":
        //            return RedirectToAction("Index");
        //    }
        //}
    }
}