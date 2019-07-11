using System;
using System.Collections.Generic;

namespace AnagramSolver.EF.DatabaseFirst
{
    public partial class UserLog
    {
        public int Id { get; set; }
        public string UserIp { get; set; }
        public string RequestWord { get; set; }
        public DateTime RequestDate { get; set; }
    }
}
