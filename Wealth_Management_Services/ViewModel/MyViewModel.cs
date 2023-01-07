using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wealth_Management_Services.Models;

namespace Wealth_Management_Services.ViewModel
{
    public static class MyViewModel
    {
        public static int id { get; set; } 
        public static string Warning { get; set; }
        public static string Welcome { get; set; }

        // Models
        //static broker broker = new broker();
        //static investor investor = new investor();
        //static management management = new management();

        public static List<broker> brokers { get; set; }
        public static List<investor> investors { get; set; }
        public static List<management> managements { get; set; }
    }
}