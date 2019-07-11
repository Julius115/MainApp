using System;
using System.Collections.Generic;

namespace AnagramSolver.EF.DatabaseFirst
{
    public partial class CachedWords
    {
        public int Id { get; set; }
        public string RequestWord { get; set; }
        public int ResponseWord { get; set; }

        public Words ResponseWordNavigation { get; set; }
    }
}
