using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginPanel_Host.Operation.Entity
{
    public class UserProfile
    {
        public string CustomerId { get; set; }
        public string Password { get; set; }
        public DateTime LastLoginTime { get; set; }
        public int Stat { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public int RecordStat { get; set; }
        public string HashType { get; set; }
    }
}
