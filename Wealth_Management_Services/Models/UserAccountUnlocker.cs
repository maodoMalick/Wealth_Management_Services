using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Wealth_Management_Services.Models
{
    public static class UserAccountUnlocker
    {
        public static SqlConnection conn1 = Connection.getConnection();

        public static void Unlocker()
        {
            using (conn1)
            {
                SqlCommand cmd = new SqlCommand("UnlockAccounts_sp", conn1);
                cmd.CommandType = CommandType.StoredProcedure;
                conn1.Open();

                cmd.ExecuteNonQuery();
            }
        }
    }
}