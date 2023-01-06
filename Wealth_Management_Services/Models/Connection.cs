using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Wealth_Management_Services.Models
{
    public static class Connection
    {
        public static SqlConnection getConnection()
        {
            // Connection String
            string cstr = ConfigurationManager.ConnectionStrings["CONN"].ConnectionString;
            SqlConnection conn = new SqlConnection(cstr);
            return conn;
        }

    }
}