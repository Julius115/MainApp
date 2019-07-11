using AnagramSolver.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AnagramSolver.BusinessLogic
{
    public class LoggerRepository : ILogger
    {
        private readonly string _connectionString;

        public LoggerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void Log(string requestWord, string userIp)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string SQLstr = "INSERT INTO UserLog (UserIp, RequestWord, RequestDate) VALUES" +
                                "( @USERIP , @REQUESTWORD, @REQUESTDATE); ";

                SqlCommand sqlCommand = new SqlCommand(SQLstr, conn);
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                         {
                             new SqlParameter("@USERIP", SqlDbType.NVarChar) {Value = userIp},
                             new SqlParameter("@REQUESTWORD", SqlDbType.NVarChar) {Value = requestWord},
                             new SqlParameter("@REQUESTDATE", SqlDbType.DateTime) {Value = DateTime.Now }
                         };
                sqlCommand.Parameters.AddRange(sqlParameters.ToArray());
                sqlCommand.ExecuteNonQuery();
            }
        }
    }
}
