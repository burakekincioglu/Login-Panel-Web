using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Web;
using System.Text;

namespace LoginPanel_UI
{
    public class Hashing
    {
        public static string getSHA1Hash(string input)  //SHA1 Hashing metodu
        {
            SHA1 hash = SHA1.Create();
            byte[] data = hash.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            string password = sBuilder.ToString();
            return password;
        }
    }
}