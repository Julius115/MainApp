using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.Contracts
{
    public enum WordEditStatus
    {
        Initial,
        EditSuccessful,
        EditUnsuccesful,
        DeleteSuccesful,
        DeleteDenied
    }
    public class WordEditInfoModel
    {
        public string OriginalWord { get; set; }
        public string NewWord { get; set; }
        public WordEditStatus EditStatus { get; set; }
    }
}
