using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Hashing
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "hasan1994";
            SHA1 hash = SHA1.Create();
            byte[] data = hash.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            string password = sBuilder.ToString();
            Console.WriteLine(password);
            Console.ReadKey();
        }
    }
}
