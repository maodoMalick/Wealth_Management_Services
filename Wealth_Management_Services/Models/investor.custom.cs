using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Wealth_Management_Services.Models
{
    [MetadataType(typeof(InvestorMetadata))]
    public partial class investor
    {
        [Required]
        [Compare("password")]
        [DataType(DataType.Password)]
        public string confirmPassword { get; set; }
    }

    class InvestorMetadata
    {
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Name must be made of Alphabet letters")]
        public string firstName { get; set; }

        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Name must be made of Alphabet letters")]
        public string lastName { get; set; }

        [Required]
        public string gender { get; set; }

        [Required]
        [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessage = "Please enter a valid email address")]
        public string email { get; set; }

        [Required]
        [RegularExpression("^[A-Za-z][A-Za-z0-9_]{2,8}$", ErrorMessage = "Username must have between 2 to 8 Alphanumeric characters")]
        public string username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?  &])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Minimum eight   characters, at least one uppercase letter, one lowercase letter, one number and one special character")]
        public string password { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> memberSince { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public Nullable<decimal> capital { get; set; }

        [Required]
        public Nullable<decimal> latestDividend { get; set; }

        [Required]
        [Range(1, 10)]
        public Nullable<int> brokerID { get; set; }

    }
}