using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wealth_Management_Services.Models
{
    public class email
    {
        [HiddenInput(DisplayValue = false)]
        public int id { get; set; }

        [Required]
        [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessage = "Please enter a valid email address")]
        public string To { get; set; }

        [Required]
        public string From { get; set; }

        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Subject must be made of Alphabet letters")]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }
    }
}