using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RepairRequestsBusinessLogic.Services
{
    public class PasswordService
    {
        public static string getHash(string password)
        {
            if(string.IsNullOrEmpty(password)) return string.Empty;

            var bytes = Encoding.UTF8.GetBytes(password);
            var hashBytes = SHA256.HashData(bytes);

            return Convert.ToHexString(hashBytes);
        }
    }
}
