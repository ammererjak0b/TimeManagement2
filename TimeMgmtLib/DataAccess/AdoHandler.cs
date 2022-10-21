using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeMgmtLib.Appconfig;

namespace TimeMgmtLib.DataAccess
{
    public class AdoHandler : IDBAccessable
    {
        private readonly string _conString;

        public AdoHandler()
        {
            _conString = TimeMgmtFactory.Instance.Resolve<IAppConfigHandler>().GetDbConnectionString();
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_conString);
        }

        public DataTable GetTable(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            try
            {
                using (cmd.Connection = GetConnection())
                {
                    cmd.Connection.Open();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Error while executing following sql command: { cmd.CommandText } | Errormessage: { e.Message } | InnerException: { e.InnerException } | Stacktrace: {e.StackTrace}");
            }
            finally
            {
                cmd.Connection.Close();
            }

            return dt;
        }

        public int ExecuteSqlCmd(SqlCommand cmd)
        {
            int affectedRows = 0;
            try
            {
                using (cmd.Connection = GetConnection())
                {
                    cmd.Connection.Open();
                    affectedRows = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Error while executing following sql command: { cmd.CommandText } | Errormessage: { e.Message } | InnerException: { e.InnerException } | Stacktrace: {e.StackTrace}");
            }
            finally
            {
                cmd.Connection.Close();
            }
            return affectedRows;
        }
    }
}
