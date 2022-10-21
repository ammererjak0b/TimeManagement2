using System.Data;
using System.Data.SqlClient;

namespace TimeMgmtLib.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDBAccessable
    {
        int ExecuteSqlCmd(SqlCommand cmd);
        DataTable GetTable(SqlCommand cmd);
    }
}