using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeMgmtLib.Models;

namespace TimeMgmtLib.DataAccess
{
    /// <summary>
    /// This class is implementing the IDataAccessable interface. 
    /// It contains all the funktionalities that are needed when accessing the data in the database.
    /// </summary>
    public class DBDataHandler : IDataAccessable
    {
        IDBAccessable _db;

        /// <summary>
        /// This constructor will be called when resolving the class in TimeMgmtFactory. IDBAccessable will then be given automaticly.
        /// </summary>
        /// <param name="db"></param>
        public DBDataHandler(IDBAccessable db)
        {
            _db = db;
        }

        /// <summary>
        /// With the parameters date and user a specific timestamp can be selected and returned
        /// </summary>
        /// <param name="date"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ITimestamp GetSpecificTimestamp(DateTime date, IUser user)
        {
            string cmdText = "SELECT Timestamp_Id, Personal_Id, Date, checkIn, checkOut" +
                             "FROM dbo.Timestamps WHERE Personal_Id = @personalId and Date = @date";

            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@personalId", user.GetPersonalId());
            cmd.Parameters.AddWithValue("@date", date);

            DataTable dt = _db.GetTable(cmd);

            if (dt.Rows.Count != 1)
            {
                throw new Exception("Error accessing db | sql did not give back only one timestamp");
            }

            return new Timestamp(dt.Rows[0]);
        }

        /// <summary>
        /// A list of timestamps of that user will be selected and returned
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<ITimestamp> GetTimestamp(IUser user)
        {
            string cmdText = "SELECT Timestamp_Id, Personal_Id, Date, checkIn, checkOut" +
                             "FROM dbo.Timestamps WHERE Personal_Id = @personalId";

            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@personalId", user.GetPersonalId());

            DataTable dt = _db.GetTable(cmd);

            if (dt == null)
            {
                throw new Exception("Error accessing db | sql did not give back user");
            }
            
            List<ITimestamp> timestamps = new List<ITimestamp>();
            foreach (DataRow item in dt.Rows)
            {
                timestamps.Add(new Timestamp(item));
            }
            timestamps.OrderBy(x => x.GetDate()).ToList();
            return timestamps;
        }

        /// <summary>
        /// With the parameters date and user a list of timestamps(beginning with the given date and ending with today) can be selected and 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<ITimestamp> GetTimestamp(DateTime date, IUser user)
        {
            string cmdText = "SELECT Timestamp_Id, Personal_Id, Date, checkIn, checkOut" +
                             "FROM dbo.Timestamps WHERE Personal_Id = @personalId AND Date <= @date";

            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@personalId", user.GetPersonalId());
            cmd.Parameters.AddWithValue("@date", date);

            DataTable dt = _db.GetTable(cmd);

            if (dt == null)
            {
                throw new Exception("Error accessing db | sql did not give back user");
            }

            List<ITimestamp> timestamps = new List<ITimestamp>();
            foreach (DataRow item in dt.Rows)
            {
                timestamps.Add(new Timestamp(item));
            }
            timestamps.OrderBy(x => x.GetDate()).ToList();
            return timestamps;
        }

        /// <summary>
        /// Selects and returns the user from the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public IUser GetUser(IUser user)
        {
            string cmdText = "SELECT Personal_Id,Prename,Surname,RegistrationDate,flexibleHoursAtRegistration,pw_hashed " +
                             "FROM dbo.Users WHERE Personal_Id = @personalId";

            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@personalId", user.GetPersonalId());
            
            DataTable dt = _db.GetTable(cmd);
            
            if (dt.Rows.Count != 1)
            {
                throw new Exception("Error accessing db | sql did not give back only one user");
            }

            return new User(dt.Rows[0]);
        }

        /// <summary>
        /// inserts a new user in the database table users
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="Exception"></exception>
        public void RegisterUser(IUser user)
        {
            string cmdText = "INSERT INTO dbo.Users (Personal_Id, Prename, Surname, RegistrationDate, flexibleHoursAtRegistration, pw_hashed) " +
                             $"Values(@personalId, @prename, @surname, @registrationDate, @flexhours, @pw);";

            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@personalId", user.GetPersonalId());
            cmd.Parameters.AddWithValue("@prename", user.GetPrename());
            cmd.Parameters.AddWithValue("@surname", user.GetSurname());
            cmd.Parameters.AddWithValue("@registrationDate", user.GetRegistrationDate());
            cmd.Parameters.AddWithValue("@flexhours", user.GetFlexibleHoursAtRegistration());
            cmd.Parameters.AddWithValue("@pw", user.GetHashedPassword());

            if(_db.ExecuteSqlCmd(cmd) != 1)
            {
                throw new Exception("Error while registration | more than 1 affected row");
            }
        }

        /// <summary>
        /// updates an existing user in the database
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="Exception"></exception>
        public void ModifyUser(IUser user)
        {
            string cmdText = "Update dbo.Users " +
            "SET Prename = @prenameToUpdate, Surname = 'surname', flexhours = 2, pw_hashed = 'pw' " +
            "WHERE Personal_Id = @prename;";

            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@prenameToUpdate", user.GetPrename());
            cmd.Parameters.AddWithValue("@surname", user.GetSurname());
            cmd.Parameters.AddWithValue("@flexhours", user.GetFlexibleHoursAtRegistration());
            cmd.Parameters.AddWithValue("@pw", user.GetHashedPassword());

            cmd.Parameters.AddWithValue("@prename", TimeMgmtFactory.Instance.UserInstance.GetPersonalId());

            if (_db.ExecuteSqlCmd(cmd) != 1)
            {
                throw new Exception("Error while registration | more than 1 affected row");
            }
        }

        public int CreateTimeStamp(ITimestamp timestamp)
        {
            string cmdText = "INSERT INTO UserTimestamps(Personal_Id, Date, checkIn, checkOut) " +
            "Values(@personalId, @date, @checkIn, @checkOut);";

            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@personalId", timestamp.GetPersonalId());
            cmd.Parameters.AddWithValue("@date", timestamp.GetDate());
            cmd.Parameters.AddWithValue("@checkIn", $"{timestamp.GetCheckIn().Hour}:{timestamp.GetCheckIn().Minute}:00");
            cmd.Parameters.AddWithValue("@checkOut", $"{timestamp.GetCheckOut().Hour}:{timestamp.GetCheckOut().Minute}:00");

            DataTable dt = _db.GetTable(cmd);
            return dt.Rows.Count;
        }

        public int ModifyTimeStamp(ITimestamp timestamp)
        {
            string cmdText = "Update dbo.UserTimestamps " +
            "SET checkIn = @checkIn, checkOut = @checkOut " +
            "WHERE Timestamp_Id = @timestamp_Id;";

            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@checkIn", $"{timestamp.GetCheckIn().Hour}:{timestamp.GetCheckIn().Minute}:00");
            cmd.Parameters.AddWithValue("@checkOut", $"{timestamp.GetCheckOut().Hour}:{timestamp.GetCheckOut().Minute}:00");
            cmd.Parameters.AddWithValue("@timestamp_Id", timestamp.GetTimeStampId());

            int affectedRows = _db.ExecuteSqlCmd(cmd);
            if (affectedRows != 1)
            {
                throw new Exception("Error while registration | more than 1 affected row");
            }

            return affectedRows;
        }
    }
}
