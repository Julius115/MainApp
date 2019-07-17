using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.EF.CodeFirst.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserIp { get; set; }
        public int SearchesLeft { get; set; } = 10;

        public ICollection<UserLog> UserLogs { get; set; }
    }
}
