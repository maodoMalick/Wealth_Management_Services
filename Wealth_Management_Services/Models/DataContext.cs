﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Wealth_Management_Services.ViewModel;


namespace Wealth_Management_Services.Models
{
    public class DataContext
    {
        // Database Connection accessible to all methods
        SqlConnection conn = Connection.getConnection();

        DataConnection DataConnector = new DataConnection();


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

                // Get the validation digit (1 or 0) from SP
                int returnCode = (int)cmd.ExecuteScalar(); 

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
                int result = (int)cmd.ExecuteScalar(); // Returns a single value from a column 
                
                if (result == 1)
                {
                    // Get the corresponding row from the database
                    MyViewModel.management = DataConnector.managements.SingleOrDefault(x => x.username == mgmt.username && x.password == pwdHash); // Be extra careful here about the 'Hashed' Password
                }

                return result;
            }
        }


        // ---------------------* BROKER SECTION *---------------------------------------------------------//

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

                // Get the validation digit (1 or 0) from SP
                int result = (int)cmd.ExecuteScalar(); // Returns a single value from a column 

                return result;
            }

        }
        
        public broker Broker_Login(broker broker)
        {
            broker bkr = null;
            
            using (conn)
            {
                SqlCommand cmd = new SqlCommand("Broker_Login_sp", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                // Password Hashing
                //string pwdHash = PasswordHash.HashCode(broker.password);

                SqlParameter paramUser = new SqlParameter("@user", broker.username);
                cmd.Parameters.Add(paramUser);
                SqlParameter paramPwd = new SqlParameter("@pwd", broker.password);
                cmd.Parameters.Add(paramPwd);

                // Get the validation digit (1 or 0) from SP
                int returnCode = (int)cmd.ExecuteScalar();

                if(returnCode == 1)
                {
                    // Fetch the corresponding row from the database
                    bkr = DataConnector.brokers.SingleOrDefault(x => x.username == broker.username && x.password == broker.password);
                }

                // Send model to the Login Action Method 
                return bkr;
            }
            
        }
        


        // ---------------------* INVESTOR SECTION *---------------------------------------------------------//
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
                SqlParameter paramPwd = new SqlParameter("@pwd", investor.password);
                cmd.Parameters.Add(paramPwd);
                SqlParameter paramMbrSince = new SqlParameter("@mbrSince", investor.memberSince);
                cmd.Parameters.Add(paramMbrSince);
                SqlParameter paramCapital = new SqlParameter("@capital", investor.capital);
                cmd.Parameters.Add(paramCapital);
                SqlParameter paramlatestDiv = new SqlParameter("@lastDiv", investor.latestDividend);
                cmd.Parameters.Add(paramlatestDiv);               
                SqlParameter parambrokerID = new SqlParameter("@brokerID", investor.brokerID);
                cmd.Parameters.Add(parambrokerID);

                // Get the validation digit (1 or 0) from SP
                int result = (int)cmd.ExecuteScalar(); // Returns a single value from a column 

                return result;
            }

        }

        public string Investor_Login(investor investor)
        {
            // Container to be sent to Action method with result 
            string result = "";

            using (conn)
            {
                SqlCommand cmd = new SqlCommand("Investor_Login_sp", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                // Password Hashing
                string pwdHash = PasswordHash.HashCode(investor.password);

                SqlParameter paramUser = new SqlParameter("@user", investor.username);
                cmd.Parameters.Add(paramUser);
                SqlParameter paramPwd = new SqlParameter("@pwd", investor.password);
                cmd.Parameters.Add(paramPwd);

                SqlDataReader ReadMe= cmd.ExecuteReader();

                while (ReadMe.Read())
                {
                    int failedAttempts = (int)ReadMe["LoginAttempts"];

                    // The 'Reader' is expecting 3 values from the Stored Procedure
                    if (Convert.ToBoolean(ReadMe["IsLocked"]))
                    {
                        MyViewModel.Warning = "YOUR ACCOUNT HAS BEEN LOCKED. PLEASE WAIT 5MN AND RETRY OR CALL CUSTOMER SERVICE.";
                        return result = "Account_Locked";
                    }
                    else if (Convert.ToBoolean(ReadMe["IsAuthenticated"]))
                    {
                        MyViewModel.investor = DataConnector.investors.SingleOrDefault(x => x.username == investor.username && x.password == investor.password);
                        return result = "Authenticated";
                    }
                    else if(failedAttempts <= 3)
                    {
                        int remainingLoginAttempts = 4 - failedAttempts;
                        MyViewModel.Warning = "Invalid Username/Password. You have " + remainingLoginAttempts + " login attempt(s) left.";
                        return result = "Failed_Login";
                    }                    
                }
            }

            return result;
        }
    }
}