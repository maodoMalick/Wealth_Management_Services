using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Wealth_Management_Services.ViewModel;
using System.Net;
using System.Net.Mail; // To send Email
using System.Web;
using System.IO;

namespace Wealth_Management_Services.Models
{
    public class DataContext
    {
        // Database Connection accessible to all methods
        SqlConnection conn = Connection.getConnection();

        // Entity Framework Data Connection
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
                //  *** COME BACK LATER TO RESET IT ***
                // Password Hashing
                //string pwdHash = PasswordHash.HashCode(mgmt.password); 

                // Stored Procedure params
                SqlParameter paramUser = new SqlParameter("@user", mgmt.username);
                cmd.Parameters.Add(paramUser);
                SqlParameter paramPwd = new SqlParameter("@pwd", mgmt.password);
                cmd.Parameters.Add(paramPwd);
                
                // Get the validation digit (1 or 0) from SP
                int result = (int)cmd.ExecuteScalar(); // Returns a single value from a column 
                
                if (result == 1)
                {
                    // Get the corresponding row from the database
                    MyViewModel.management = DataConnector.managements.SingleOrDefault(x => x.username == mgmt.username && x.password == mgmt.password); // Be extra careful here about the 'Hashed' Password
                }

                return result;
            }
        }

        public void TransactionsData()
        {
            using(conn)
            {
                SqlCommand cmd = new SqlCommand("BrokerTransactionsReport_sp", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                cmd.ExecuteNonQuery();
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
        
        public int Broker_Login(broker broker)
        {
            //broker bkr = null;
            
            using (conn)
            {
                SqlCommand cmd = new SqlCommand("Broker_Login_sp", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                // Password Hashing
                string pwdHash = PasswordHash.HashCode(broker.password);

                SqlParameter paramUser = new SqlParameter("@user", broker.username);
                cmd.Parameters.Add(paramUser);
                SqlParameter paramPwd = new SqlParameter("@pwd", pwdHash);
                cmd.Parameters.Add(paramPwd);

                // Get the validation digit (1 or 0) from SP
                int returnCode = (int)cmd.ExecuteScalar();

                // Get the validation digit (1 or 0) from SP
                int result = (int)cmd.ExecuteScalar(); // Returns a single value from a column 

                if (result == 1)
                {
                    // Get the corresponding row from the database
                    MyViewModel.broker = DataConnector.brokers.SingleOrDefault(x => x.username == broker.username && x.password == pwdHash); // Be extra careful here about the 'Hashed' Password
                }

                return result;
            }
            
        }

        public void PurchasingAssets(brokerOperation bkrO)
        {
            using (conn)
            {
                SqlCommand cmd = new SqlCommand("Stock_Operations_sp", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                SqlParameter paramTrader = new SqlParameter("@trader", bkrO.trader);
                cmd.Parameters.Add(paramTrader);
                SqlParameter paramShares = new SqlParameter("@shares", bkrO.shares);
                cmd.Parameters.Add(paramShares);
                SqlParameter paramItem = new SqlParameter("@item", bkrO.item);
                cmd.Parameters.Add(paramItem);
                SqlParameter paramAmount = new SqlParameter("@amount", bkrO.amount);
                cmd.Parameters.Add(paramAmount);
                SqlParameter paramPrice = new SqlParameter("@price", bkrO.price);
                cmd.Parameters.Add(paramPrice);
                SqlParameter paramBkrID = new SqlParameter("@brokerID", bkrO.brokerID);
                cmd.Parameters.Add(paramBkrID);
                SqlParameter paramPoDate = new SqlParameter("@purchaseDate", bkrO.purchaseDate);
                cmd.Parameters.Add(paramPoDate);
                SqlParameter paramCtID = new SqlParameter("@clientID", bkrO.clientID);
                cmd.Parameters.Add(paramCtID);
                SqlParameter paramTotal = new SqlParameter("@total", bkrO.total);
                cmd.Parameters.Add(paramTotal);

                //int result = (int)cmd.ExecuteScalar();
                //return result;
                cmd.ExecuteNonQuery();
            }
        }

        public List<decimal?> BrokerPerformance()
        {
            // Retrieve the exact number of brokers
            int bkrCount = DataConnector.brokers.Count();
            // Retrieve all brokers
            List<broker> brokers = DataConnector.brokers.ToList();
            // This list will hold the total dividends received by each investor
            List<decimal?> result = new List<decimal?>();
            int count = 1;  // For tracking 
            int index = 0;  // for the Array indexes
            while (count <= bkrCount)
            {
                count = brokers[index].id;
                // Collecting the total dividends made by any specific broker for a given investor
                decimal? sum = (DataConnector.investors.Where(x => x.brokerID == count)).Sum(x => x.latestDividend);
                // Register the total amount of each broker's returns to investor
                result.Add(sum);
                index++;
                count++;
            }

            return result;
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

                // Get the validation digit (1 or 0) from SP
                int result = (int)cmd.ExecuteScalar(); // Returns a single value from a column 

                return result;
            }

        }

        public string Investor_Login(investor investor)
        {
            // Will be sent to Action method with result 
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
                SqlParameter paramPwd = new SqlParameter("@pwd", pwdHash);
                cmd.Parameters.Add(paramPwd);

                SqlDataReader ReadMe = cmd.ExecuteReader();

                while (ReadMe.Read())
                {
                    // Get the current number of the user's failed login attempts
                    int failedAttempts = (int)ReadMe["LoginAttempts"];

                    // The 'Reader' is expecting 3 values from the Stored Procedure
                    if (Convert.ToBoolean(ReadMe["IsLocked"]))
                    {
                        MyViewModel.InvestorWarning = "YOUR ACCOUNT HAS BEEN LOCKED. PLEASE WAIT 5MN AND RETRY OR CALL CUSTOMER SERVICE.";
                        return result = "Account_Locked";
                    }
                    else if (Convert.ToBoolean(ReadMe["IsAuthenticated"]))
                    {
                        MyViewModel.investor = DataConnector.investors.SingleOrDefault(x => x.username == investor.username && x.password == pwdHash); // Be extra careful here about the 'Hashed' Password

                        return result = "Authenticated";
                    }
                    else if(failedAttempts <= 3)
                    {
                        int remainingLoginAttempts = 4 - failedAttempts;
                        MyViewModel.InvestorWarning = "Invalid Username/Password. You have " + remainingLoginAttempts + " login attempt(s) left.";
                        return result = "Failed_Login";
                    }                    
                }

                return result;
            }

            
        }

        public ArrayList Investor_Data(int id)
        {
            ArrayList InvArrList = new ArrayList();
            SqlConnection conn1 = Connection.getConnection();

            using (conn1)
            {
                SqlCommand cmd = new SqlCommand("InvestorData_sp", conn1);
                cmd.CommandType = CommandType.StoredProcedure;
                conn1.Open();

                SqlParameter paramId = new SqlParameter("@id", id);
                cmd.Parameters.Add(paramId);

                SqlDataReader iReader = cmd.ExecuteReader();
                while (iReader.Read())
                {
                    InvArrList.Add(iReader["firstName"]);
                    InvArrList.Add(iReader["lastName"]);
                    InvArrList.Add(iReader["email"]);
                    InvArrList.Add(Convert.ToDateTime(iReader["memberSince"]));
                    InvArrList.Add(Convert.ToDecimal(iReader["capital"]));
                    InvArrList.Add(Convert.ToDecimal(iReader["last profit"]));
                    InvArrList.Add(iReader["name"]);
                }

                return InvArrList;
            }
        }

        public List<decimal> MyDividends(int id)
        {
            // New Sql Connection
            SqlConnection conn2 = Connection.getConnection();
            // Container to be sent to Controller
            List<decimal> decimals = new List<decimal>();

            using (conn2)
            {
                SqlCommand cmd = new SqlCommand("GetMyDividends_sp", conn2);
                cmd.CommandType = CommandType.StoredProcedure;
                conn2.Open();

                SqlParameter paramID = new SqlParameter("@id", id);
                cmd.Parameters.Add(paramID);

                //int index = 0;
                SqlDataReader readMe = cmd.ExecuteReader();
                while (readMe.Read())
                {
                    decimals.Add(Convert.ToDecimal(readMe["january"]));
                    decimals.Add(Convert.ToDecimal(readMe["february"]));
                    decimals.Add(Convert.ToDecimal(readMe["march"]));
                    decimals.Add(Convert.ToDecimal(readMe["april"]));
                    decimals.Add(Convert.ToDecimal(readMe["may"]));
                    decimals.Add(Convert.ToDecimal(readMe["june"]));
                    decimals.Add(Convert.ToDecimal(readMe["july"]));
                    decimals.Add(Convert.ToDecimal(readMe["august"]));
                    decimals.Add(Convert.ToDecimal(readMe["september"]));
                    decimals.Add(Convert.ToDecimal(readMe["october"]));
                    decimals.Add(Convert.ToDecimal(readMe["november"]));
                    decimals.Add(Convert.ToDecimal(readMe["december"]));
                }

                // return the dividends of the single investor
                return decimals;
             }
        }

        public List<string> getMonthNames()
        {
            // New Sql Connection
            SqlConnection conn1 = Connection.getConnection();
            // Container to be sent to Controller
            List<string> months = new List<string>();

            using (conn1)
            {
                SqlCommand cmd = new SqlCommand("GetMonthNames_sp", conn1);
                cmd.CommandType = CommandType.StoredProcedure;
                conn1.Open();

                SqlDataReader ReadMe = cmd.ExecuteReader();
                while (ReadMe.Read())
                {
                    // collect the first names of all investors
                    months.Add(ReadMe["Months"].ToString());
                }

                return months;
            }
        }

        // Investor Sending Email to their Broker
        public void SendEmail(email e, HttpPostedFileBase fileUploader)
        {            
            MailMessage mail = new MailMessage(e.From, e.To);
            mail.Subject = e.Subject;
            mail.Body= e.Body;
            mail.IsBodyHtml = false;

            // Uploading an Attachment
            if (fileUploader != null)
            {
                string fname = Path.GetFileName(fileUploader.FileName);
                mail.Attachments.Add(new Attachment(fileUploader.InputStream, fname));
            }

            NetworkCredential credo = new NetworkCredential("test@libidoor.com", "Dev_C#_22");

            SmtpClient smtp = new SmtpClient("smtp.ionos.com", 587);
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = credo;
            smtp.Send(mail);
            MyViewModel.EmailMsg = "Your email has been sent successfully!";
        }
    }
}