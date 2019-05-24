using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ProjektDS
{
    public class Crypto
    {
        public static string Szyfro(string haslo)
        {
            string sol = "^&%^$#%%#$##$(*)(*%&^%$#$%#^%&%&%^^%^";
            return Convert.ToBase64String(System.Security.Cryptography.SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(haslo + sol)));
        }
    }
}