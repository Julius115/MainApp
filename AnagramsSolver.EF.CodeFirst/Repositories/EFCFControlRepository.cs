using AnagramSolver.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace AnagramSolver.EF.CodeFirst.Repositories
{
    public class EFCFControlRepository : IDatabaseManager
    {
        private readonly AnagramsDbCfContext _em;

        public EFCFControlRepository(AnagramsDbCfContext dbContext)
        {
            _em = dbContext;
        }

        public List<string> GetTablesNames(string tableName)
        {
            List<string> tableNames = _em.Model.GetEntityTypes().Select(t => t.ClrType).Select(a => a.Name + "s").ToList();

            return tableNames;
        }

        public void DeleteTableData(string tableName)
        {
            _em.Database.ExecuteSqlCommand("ClearTableData @TABLENAME", new SqlParameter("@TABLENAME", tableName));
        }
    }
}
