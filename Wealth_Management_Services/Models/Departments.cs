using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wealth_Management_Services.Models
{
    public class Departments
    {
        public IEnumerable<SelectListItem> getDepartments
        {
            get
            {
                return new List<SelectListItem> {
                new SelectListItem(){ Text = "INVESTORS", Value = "investors"},
                new SelectListItem(){ Text = "BROKERS", Value = "brokers"},
                new SelectListItem(){ Text = "MANAGEMENT", Value = "management"}
                };
            }
        }
    }
}