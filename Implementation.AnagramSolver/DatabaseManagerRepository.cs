using AnagramSolver.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AnagramSolver.BusinessLogic
{
    public class DatabaseManagerRepository : IDatabaseManager
    {
        private List<string> anagrams = new List<string>();
        public List<string> GetTablesNames(string tableName)
        {
            using (SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AnagramsDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                conn.Open();

                string SQLstr = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_CATALOG = 'AnagramsDB'";
                SqlCommand cmda = new SqlCommand(SQLstr, conn);

                SqlDataReader reader = cmda.ExecuteReader();
                List<string> anagrams = new List<string>();

                while (reader.Read())
                {
                    anagrams.Add(reader.GetString(0));
                }

                return anagrams;
            }
        }

        public void DeleteTableData(string tableName)
        {
            using (var conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AnagramsDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            using (var command = new SqlCommand("ClearTableData", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                conn.Open();
                command.Parameters.AddWithValue("@TABLENAME", tableName);
                command.ExecuteNonQuery();

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@TABLENAME", "UserLog");
                command.ExecuteNonQuery();
            }
        }
    }
}
