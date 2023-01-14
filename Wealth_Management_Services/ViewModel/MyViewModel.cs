using Wealth_Management_Services.Models;

namespace Wealth_Management_Services.ViewModel
{
    public class MyViewModel
    {
        public static int id { get; set; } 
        public static string Warning { get; set; }
        public static string Welcome { get; set; }

        // Models
        public static broker broker = null;
        public static investor investor = null;
        public static management management = null;

    }
}