using AnagramSolver.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AnagramSolver.BusinessLogic
{
    public class CachedWordsRepository : ICachedWords
    {
        private readonly IAnagramSolver _anagramSolver;
        private readonly string _connectionString;

        private List<string> anagrams = new List<string>();


        public CachedWordsRepository(IAnagramSolver anagramSolver)
        {
            _anagramSolver = anagramSolver;
            _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AnagramsDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }

        public List<string> CacheWords(string requestWord)
        {
                if (CheckIfCached(requestWord))
                {
                    return GetCachedAnagrams(requestWord);
                }

                SetCachedAnagrams(requestWord);
                return anagrams;
        }

        public bool CheckIfCached(string requestWord)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();


                string SQLstr = "SELECT Count(Id) FROM CachedWords WHERE RequestWord = @WORD";
                SqlCommand sqlCommand = new SqlCommand(SQLstr, conn);
                SqlParameter sqlParameter = new SqlParameter();
                sqlParameter.ParameterName = ("@WORD");
                sqlParameter.Value = requestWord;
                sqlCommand.Parameters.Add(sqlParameter);

                if ((int)sqlCommand.ExecuteScalar() > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public void SetCachedAnagrams(string requestWord)
        {
            anagrams = _anagramSolver.GetAnagrams(requestWord).ToList();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                foreach (string anagram in anagrams)
                {
                    string SQLstr = "INSERT INTO CachedWords (RequestWord, ResponseWord) VALUES" +
                                "( @WORD , (SELECT Id FROM Words WHERE Word = @WORDREQUEST)); ";

                    SqlCommand sqlCommand = new SqlCommand(SQLstr, conn);

                    List<SqlParameter> sqlParameters = new List<SqlParameter>()
                         {
                             new SqlParameter("@WORD", SqlDbType.NVarChar) {Value = requestWord},
                             new SqlParameter("@WORDREQUEST", SqlDbType.NVarChar) {Value = anagram},
                         };
                    sqlCommand.Parameters.AddRange(sqlParameters.ToArray());

                    sqlCommand.ExecuteNonQuery();
                }
            }
        }

        public List<string> GetCachedAnagrams(string requestWord)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string SQLstr = "SELECT b.Word FROM Words AS b INNER JOIN CachedWords as a ON (b.Id = a.ResponseWord and a.RequestWord = @WORD)";
                SqlCommand sqlCommand = new SqlCommand(SQLstr, conn);

                SqlParameter sqlParameter = new SqlParameter();
                sqlParameter.ParameterName = ("@WORD");
                sqlParameter.Value = requestWord;
                sqlCommand.Parameters.Add(sqlParameter);

                SqlDataReader reader;
                reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    anagrams.Add(reader.GetString(0));
                }
            }
            return anagrams;
        }
    }
}
