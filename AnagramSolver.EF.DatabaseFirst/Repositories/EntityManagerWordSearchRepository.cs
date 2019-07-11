using AnagramSolver.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnagramSolver.EF.DatabaseFirst.Repositories
{
    public class EntityManagerWordSearchRepository : IWordSearch
    {
        AnagramsDBContext em = new AnagramsDBContext();

        public SearchInfoModel GetSearchInfo(string word, DateTime date)
        {
            SearchInfoModel searchInfoModel = new SearchInfoModel();

            // var a = em.Words.Join(em.CachedWords, e => e. == 1, )
            //   var a = em.CachedWords.Join(em.UserLog, c => c.RequestWord == word)

            // var a = from w in em.Words
            //         join c in em.CachedWords
            //         on w.Id equals c.ResponseWord
            //         join u in em.UserLog
            //         on 

            var a = from w in em.Words
                    join c in em.CachedWords on w.Id equals c.ResponseWord
                    join u in em.UserLog on w.Word equals u.RequestWord
                    where w.Word == word && u.RequestDate == date
                    select new SearchInfoModel()
                    {
                        UserIp = u.UserIp,
                        RequestDate = u.RequestDate,
                        RequestWord = word,
                        Anagrams = new List<string>() { w.Word }
                    };

            
                                //select new SearchInfoModel
                                //{
                                //    UserIp = u.UserIp,
                                //    RequestDate = u.RequestDate,
                                //    RequestWord = word,
                                //
                                //};


            //var table1JoinTable2 = table1.Join(table2, t1 => t1.Id1, t2 => t2.Id2, (t1, t2) => new { Id = t1.Id1, Column1 = t1.Column1, Column2 = t2.Column2 });
            //var table1JoinTable2JoinTable3 = table1JoinTable2.Join(table3, t12 => t12.Id, t3 => t3.Id3, (t12, t3) => new { Id = t12.Id, Column1 = t12.Column1, Column2 = t12.Column2, Column3 = t3.Column3 });

            //SearchInfoModel searchInfo = new SearchInfoModel();
            //
            //using (SqlConnection conn = new SqlConnection(_connectionString))
            //{
            //    conn.Open();
            //
            //    string SQLstr = "SELECT u.UserIp, u.RequestDate , u.RequestWord,  w.Word " +
            //    "FROM UserLog AS u " +
            //    "INNER JOIN CachedWords AS c " +
            //    "ON u.RequestWord = c.RequestWord AND u.RequestWord = @WORD AND u.RequestDate = @DATE " +
            //    "INNER JOIN Words AS w " +
            //    "ON w.Id = c.ResponseWord";
            //    SqlCommand cmda = new SqlCommand(SQLstr, conn);
            //
            //    SqlParameter paramas = new SqlParameter();
            //
            //    List<SqlParameter> prm = new List<SqlParameter>()
            //             {
            //                 new SqlParameter("@WORD", SqlDbType.NVarChar) {Value = word},
            //                 new SqlParameter("@DATE", SqlDbType.DateTime) {Value = date},
            //             };
            //    cmda.Parameters.AddRange(prm.ToArray());
            //
            //    SqlDataReader reader;
            //    reader = cmda.ExecuteReader();
            //
            //    if (reader != null)
            //    {
            //        reader.Read();
            //        searchInfo.UserIp = reader.GetString(0);
            //        searchInfo.RequestDate = reader.GetDateTime(1);
            //        searchInfo.RequestWord = reader.GetString(2);
            //        searchInfo.Anagrams.Add(reader.GetString(3));
            //    }
            //
            //    while (reader.Read())
            //    {
            //        searchInfo.Anagrams.Add(reader.GetString(3));
            //    }
            //}
            //
            //return searchInfo;
            return searchInfoModel;
        }

        public List<string> GetWordsContainingPart(string input)
        {
            List<string> wordsResult = em.Words.Where(w => w.Word.Contains(input)).Select(w => w.Word).ToList();

            return wordsResult;
        }

        public List<SearchHistoryInfoModel> GetSearchHistory()
        {
            List<SearchHistoryInfoModel> searchHistoryInfoModels = em.UserLog.Select( u => new SearchHistoryInfoModel() { Ip = u.UserIp, RequestDate = u.RequestDate, RequestWord = u.RequestWord}).ToList();

            return searchHistoryInfoModels;
        }


    }
}
