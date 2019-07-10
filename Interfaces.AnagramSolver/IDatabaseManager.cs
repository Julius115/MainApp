using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.Contracts
{
    public interface IDatabaseManager
    {
        List<string> GetTablesNames(string tableName);
        void DeleteTableData(string tableName);


    }
}
