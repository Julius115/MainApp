using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace AnagramSolver.LoadToDB.Console
{
    class Program
    {
        private static IWordRepository _wordRepository;

        static void Main(string[] args)
        {
            _wordRepository = new FileWordRepository("zodynas.txt");

            Dictionary<string, int> wordsList = _wordRepository.GetWordsDictionary();

            string sql = @"INSERT INTO dbo.Words (Word) VALUES (test)";

            SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AnagramsDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            
            conn.Open();

            string SQLstr = "INSERT INTO Words (Word)" +
               "VALUES (@WORD)";
            SqlCommand cmd = new SqlCommand(SQLstr, conn);
            cmd.Parameters.Add("@WORD", System.Data.SqlDbType.VarChar);

            foreach (KeyValuePair<string, int> key in _wordRepository.GetWordsDictionary())
            {
                cmd.Parameters["@WORD"].Value = key.Key.ToString();
                cmd.ExecuteNonQuery();
            }

            conn.Close();
        }
    }
}
