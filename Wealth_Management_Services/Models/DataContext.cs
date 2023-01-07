using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace Wealth_Management_Services.Models
{
    public class DataContext
    {
        // Database Connection
        SqlConnection conn = Connection.getConnection();

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

        public int Broker_Registration(management mgmt)
        {
            using (conn)
            {
                SqlCommand cmd = new SqlCommand("Investor_Registration_sp", conn);

            }

            return 
        }
    }
}