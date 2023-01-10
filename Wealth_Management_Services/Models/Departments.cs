using System.Collections.Generic;
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
                new SelectListItem(){ Text = "INVESTORS", Value = "Investors"},
                new SelectListItem(){ Text = "BROKERS", Value = "Brokers"},
                new SelectListItem(){ Text = "MANAGEMENT", Value = "Management"}
                };
            }
        }
    }
}