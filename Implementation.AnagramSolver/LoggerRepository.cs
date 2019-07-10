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
        public void Log(string requestWord, string userIp)
        {
            using (SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AnagramsDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
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
