using AnagramSolver.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AnagramSolver.BusinessLogic
{
    public class WordSearchRepository : IWordSearch
    {
        private readonly string _connectionString;

        public WordSearchRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public SearchInfoModel GetSearchInfo(string word, DateTime date)
        {
            SearchInfoModel searchInfo = new SearchInfoModel();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string SQLstr = "SELECT u.UserIp, u.RequestDate , u.RequestWord,  w.Word " +
                "FROM UserLog AS u " +
                "INNER JOIN CachedWords AS c " +
                "ON u.RequestWord = c.RequestWord AND u.RequestWord = @WORD AND u.RequestDate = @DATE " +
                "INNER JOIN Words AS w " +
                "ON w.Id = c.ResponseWord";
                SqlCommand cmda = new SqlCommand(SQLstr, conn);

                SqlParameter paramas = new SqlParameter();

                List<SqlParameter> prm = new List<SqlParameter>()
                         {
                             new SqlParameter("@WORD", SqlDbType.NVarChar) {Value = word},
                             new SqlParameter("@DATE", SqlDbType.DateTime) {Value = date},
                         };
                cmda.Parameters.AddRange(prm.ToArray());

                SqlDataReader reader;
                reader = cmda.ExecuteReader();

                if (reader != null)
                {
                    reader.Read();
                    searchInfo.UserIp = reader.GetString(0);
                    searchInfo.RequestDate = reader.GetDateTime(1);
                    searchInfo.RequestWord = reader.GetString(2);
                    searchInfo.Anagrams.Add(reader.GetString(3));
                }

                while (reader.Read())
                {
                    searchInfo.Anagrams.Add(reader.GetString(3));
                }
            }

            return searchInfo;
        }

        public List<string> GetWordsContainingPart(string input)
        {
            List<string> wordsResult = new List<string>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string SQLstr = "SELECT Word FROM Words WHERE Word LIKE '%' + @WORD + '%'";
                SqlCommand cmd = new SqlCommand(SQLstr, conn);

                SqlParameter param = new SqlParameter();
                param.ParameterName = ("@WORD");
                param.Value = input;
                cmd.Parameters.Add(param);

                SqlDataReader reader;
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    wordsResult.Add(reader.GetString(0));
                }
            }

            return wordsResult;
        }

        public List<SearchHistoryInfoModel> GetSearchHistory()
        {
            List<SearchHistoryInfoModel> searchHistory = new List<SearchHistoryInfoModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string SQLstr = "SELECT UserIp, RequestWord, RequestDate FROM UserLog";
                SqlCommand cmd = new SqlCommand(SQLstr, conn);

                SqlDataReader reader;
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    searchHistory.Add(new SearchHistoryInfoModel() { Ip = reader.GetString(0), RequestWord = reader.GetString(1), RequestDate = reader.GetDateTime(2) });

                }
            }

            return searchHistory;
        }


    }
}
