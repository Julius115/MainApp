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

        private List<string> anagrams = new List<string>();


        public CachedWordsRepository(IAnagramSolver anagramSolver)
        {
            _anagramSolver = anagramSolver;
        }

        public List<string> CacheWords(string requestWord)
        {
            using (SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AnagramsDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                conn.Open();

                if (CheckIfCached(requestWord, conn))
                {
                    return GetCachedAnagrams(requestWord, conn);
                }

                SetCachedAnagrams(requestWord, conn);
                return anagrams;
            }
        }

        public bool CheckIfCached(string requestWord, SqlConnection conn)
        {
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

            return false;
        }

        public void SetCachedAnagrams(string requestWord, SqlConnection conn)
        {
            anagrams = _anagramSolver.GetAnagrams(requestWord).ToList();

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

        public List<string> GetCachedAnagrams(string requestWord, SqlConnection conn)
        {
            string SQLstr = "SELECT b.Word FROM Words AS b INNER JOIN CachedWords as a ON (b.Id = a.ResponseWord and a.RequestWord = @WORD)";
            SqlCommand sqlCommand = new SqlCommand(SQLstr, conn);

            SqlParameter sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = ("@WORD");
            sqlParameter.Value = requestWord;
            sqlCommand.Parameters.Add(sqlParameter);

            SqlDataReader reader;
            reader = sqlCommand.ExecuteReader();

            //anagrams = new List<string>();

            while (reader.Read())
            {
                anagrams.Add(reader.GetString(0));
            }

            return anagrams;
        }
    }
}
