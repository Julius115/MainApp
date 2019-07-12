using AnagramSolver.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AnagramSolver.EF.DatabaseFirst.Repositories
{
    public class EFControlRepository : IDatabaseManager
    {
        AnagramsDBContext em = new AnagramsDBContext();

        public List<string> GetTablesNames(string tableName)
        {
            List<string> tableNames = em.Model.GetEntityTypes().Select(t => t.ClrType).Select(a => a.Name).ToList();
            
            return tableNames;
        }

        public void DeleteTableData(string tableName)
        {
            DbCommand cmd = em.Database.GetDbConnection().CreateCommand();
            em.Database.ExecuteSqlCommand("ClearTableData @TABLENAME", new SqlParameter("@TABLENAME", tableName));
        }
    }
}
