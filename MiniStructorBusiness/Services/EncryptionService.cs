using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace MiniStructorBusiness.Services
{
    public class EncryptionService
    {
        public string EncryptPassword(string password, string userName)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltedString = string.Format("{0}{1}", password, userName);
                byte[] saltedStringAsBytes = Encoding.UTF8.GetBytes(saltedString);
                return Convert.ToBase64String(sha256.ComputeHash(saltedStringAsBytes));
            }
        }
    }
}
