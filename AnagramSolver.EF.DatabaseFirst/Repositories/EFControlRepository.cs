using AnagramSolver.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace AnagramSolver.EF.DatabaseFirst.Repositories
{
    public class EFControlRepository : IDatabaseManager
    {
        private readonly AnagramsDBContext _em;

        public EFControlRepository(AnagramsDBContext dbContext)
        {
            _em = dbContext;
        }

        public List<string> GetTablesNames(string tableName)
        {
            
            List<string> tableNames = _em.Model.GetEntityTypes().Select(t => t.ClrType).Select(a => a.Name).ToList();
            
            return tableNames;
        }

        public void DeleteTableData(string tableName)
        {
            _em.Database.ExecuteSqlCommand("ClearTableData @TABLENAME", new SqlParameter("@TABLENAME", tableName));
        }
    }
}
