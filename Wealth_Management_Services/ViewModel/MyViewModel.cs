﻿using System.Collections;
using Wealth_Management_Services.Models;

namespace Wealth_Management_Services.ViewModel
{
    public class MyViewModel
    {
        public static int id { get; set; } 
        public static string Warning { get; set; }
        public static string InvestorWarning { get; set; }
        public static string Welcome { get; set; }
        public static string Message { get; set; }
        public static int UserId { get; set; }
        public static int BrokerId { get; set; }
        public static string EmailMsg { get; set; }
        public static string Thanks { get; set; }


        // Models
        public static broker broker = null;
        public static investor investor = null;
        public static management management = null;

        // Collections
        public static ArrayList Investor_ArrList { get; set; }

    }
}