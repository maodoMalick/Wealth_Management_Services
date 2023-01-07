using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Helpers;


namespace Wealth_Management_Services.Models
{
    public class DataContext
    {
        // Database Connection accessible to all methods
        SqlConnection conn = Connection.getConnection();


        // ---------------------* MANAGEMENT SECTION *---------------------------------------------------------//
        public int Management_Registration(management mgmt)
        {
            using (conn)
            {
                SqlCommand cmd = new SqlCommand("Mgmt_Registration_sp", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                // Hashing the password
                string pwdHash = PasswordHash.HashCode(mgmt.password);

                // Set Stored Procedure parameters
                SqlParameter paramName = new SqlParameter("@name", mgmt.name);
                cmd.Parameters.Add(paramName);
                SqlParameter paramUser = new SqlParameter("@user", mgmt.username);
                cmd.Parameters.Add(paramUser);
                SqlParameter paramPwd = new SqlParameter("@pwd", pwdHash); // Hashed password
                cmd.Parameters.Add(paramPwd);
                SqlParameter paramGender = new SqlParameter("@gender", mgmt.gender);
                cmd.Parameters.Add(paramGender);
                SqlParameter paramEmail = new SqlParameter("@email", mgmt.email);
                cmd.Parameters.Add(paramEmail);
                SqlParameter paramSalary = new SqlParameter("@salary", mgmt.salary);
                cmd.Parameters.Add(paramSalary);
                SqlParameter paramHireDate = new SqlParameter("@hireDate", mgmt.hireDate);
                cmd.Parameters.Add(paramHireDate);

                int returnCode = cmd.ExecuteNonQuery();

                // Will return 1 or -1 to the MVC program
                return returnCode;
            }
        }

        public int Management_Login(management mgmt)
        {
            using (conn)
            {
                SqlCommand cmd = new SqlCommand("Mgmt_Login_sp", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                // Password Hashing
                string pwdHash = PasswordHash.HashCode(mgmt.password);

                // Stored Procedure params
                SqlParameter paramUser = new SqlParameter("@user", mgmt.username);
                cmd.Parameters.Add(paramUser);
                SqlParameter paramPwd = new SqlParameter("@pwd", pwdHash);
                cmd.Parameters.Add(paramPwd);

                // Get the validation digit (1 or 0) from SP
                int result = cmd.ExecuteNonQuery();

                // Send digit to the Login Action Method 
                return result;
            }
        }


        // ---------------------* /END MANAGEMENT SECTION *---------------------------------------------------------//

        public int Broker_Registration(broker broker)
        {
            using (conn)
            {
                SqlCommand cmd = new SqlCommand("Broker_Registration_sp", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                // Hashing the password
                string pwdHash = PasswordHash.HashCode(broker.password);

                SqlParameter paramName = new SqlParameter("@name", broker.name);
                cmd.Parameters.Add(paramName);
                SqlParameter paramUser = new SqlParameter("@user", broker.username);
                cmd.Parameters.Add(paramUser);
                SqlParameter paramPwd = new SqlParameter("@pwd", pwdHash);
                cmd.Parameters.Add(paramPwd);  
                SqlParameter paramGender = new SqlParameter("@gender", broker.gender);
                cmd.Parameters.Add(paramGender);
                SqlParameter paramEmail = new SqlParameter("@email", broker.email);
                cmd.Parameters.Add(paramEmail);
                SqlParameter paramSalary = new SqlParameter("@salary", broker.salary);
                cmd.Parameters.Add(paramSalary);
                SqlParameter paramCommission = new SqlParameter("@commission", broker.commission);
                cmd.Parameters.Add(paramCommission);
                SqlParameter paramHireDate = new SqlParameter("@hireDate", broker.hireDate);
                cmd.Parameters.Add(paramHireDate);
                SqlParameter paramMgrID = new SqlParameter("@mgrID", broker.managerID);
                cmd.Parameters.Add(paramMgrID);

                int result = cmd.ExecuteNonQuery();

                return result;
            }

        }

        public int Investor_Registration(investor investor)
        {
            using (conn)
            {
                SqlCommand cmd = new SqlCommand("Investor_Registration_sp", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                // Hashing the password
                string pwdHash = PasswordHash.HashCode(investor.password);

                SqlParameter paramFirst = new SqlParameter("@first", investor.firstName);
                cmd.Parameters.Add(paramFirst);
                SqlParameter paramLast = new SqlParameter("@last", investor.lastName);
                cmd.Parameters.Add(paramLast);
                SqlParameter paramGender = new SqlParameter("@gender", investor.gender);
                cmd.Parameters.Add(paramGender);
                SqlParameter paramEmail = new SqlParameter("@email", investor.email);
                cmd.Parameters.Add(paramEmail);
                SqlParameter paramUser = new SqlParameter("@user", investor.username);
                cmd.Parameters.Add(paramUser);
                SqlParameter paramPwd = new SqlParameter("@pwd", pwdHash);
                cmd.Parameters.Add(paramPwd);
                SqlParameter paramMbrSince = new SqlParameter("@mbrSince", investor.memberSince);
                cmd.Parameters.Add(paramMbrSince);
                SqlParameter paramCapital = new SqlParameter("@capital", investor.capital);
                cmd.Parameters.Add(paramCapital);
                SqlParameter paramlatestDiv = new SqlParameter("@lastDiv", investor.latestDividend);
                cmd.Parameters.Add(paramlatestDiv);               
                SqlParameter parambrokerID = new SqlParameter("@brokerID", investor.brokerID);
                cmd.Parameters.Add(parambrokerID);

                int result = cmd.ExecuteNonQuery();

                return result;
            }

        }
    }
}