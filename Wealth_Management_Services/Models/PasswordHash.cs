using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Wealth_Management_Services.Models
{
    public static class PasswordHash
    {
        public static string HashCode(string value)
        {
            using (MD5CryptoServiceProvider crypto = new MD5CryptoServiceProvider())
            {
                UTF8Encoding code = new UTF8Encoding();
                byte[] hash = crypto.ComputeHash(code.GetBytes(value));
                return Convert.ToBase64String(hash);
            }
        }
    }
}