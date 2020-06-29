using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginPanel_Host.Operation.Data
{
    public class DBHelper
    {
        public static string GetConnectionString()
        {
            const string connectionString = "Data Source=DESKTOP-LM92FKU\\MSSQLSERVER01; Initial Catalog=LoginPanel; Integrated Security=True; Trusted_Connection=Yes;";
            return connectionString;
        }
    }
}
