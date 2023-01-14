using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Wealth_Management_Services.Models
{
    [MetadataType(typeof(BrokerMetatdata))]
    public partial class broker
    {
        [Required]
        [System.Web.Mvc.Compare("password")]
        [DataType(DataType.Password)]
        [DisplayName("confirm password")]
        public string confirmPassword { get; set; }
    }

    public class BrokerMetatdata
    {
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Name must be made of Alphabet letters.")]
        public string name { get; set; }

        [Required]
        [RegularExpression("^[A-Za-z][A-Za-z0-9_]{2,8}$", ErrorMessage = "Username must be made between 2 to 8 Alphanumeric characters.")]
        [Remote("IsUsernameValid", "Home", ErrorMessage = "Username has already been taken")] // 'Json' Client-Side Validation
        public string username { get; set; }

        [Required]
        //[DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?  &])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Minimum eight   characters, at least one uppercase letter, one lowercase letter, one number and one special character")]
        public string password { get; set; }

        [Required]
        public string gender { get; set;}
        
        [Required]
        [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessage = "Please enter a valid email address")]
        public string email { get; set;}

        [Required]
        [DataType(DataType.Currency)]
        public decimal salary { get; set; }

        [Required]
        public decimal commission { get; set; }

        [Required]
        [Display( Name= "hire date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime hireDate { get; set; }

        [Required]
        [Range(1, 10)]
        public int managerID { get; set; }

    }
}