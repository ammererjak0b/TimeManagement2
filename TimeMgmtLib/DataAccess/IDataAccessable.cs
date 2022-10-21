using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeMgmtLib.Models;

namespace TimeMgmtLib.DataAccess
{
    /// <summary>
    /// This interface contains all the functionalities of a data accessable class
    /// Because using interfaces we are not dependant of specific implementations
    /// </summary>
    public interface IDataAccessable
    {
        /// <summary>
        /// Selects and returns the user from the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IUser GetUser(IUser user);

        
        void RegisterUser(IUser user);

        
        void ModifyUser(IUser user);

        /// <summary>
        /// With the parameters date and user a specific timestamp can be selected and returned
        /// </summary>
        /// <param name="date"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        ITimestamp GetSpecificTimestamp(DateTime date, IUser user);

        /// <summary>
        /// A list of timestamps of that user will be selected and returned
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        List<ITimestamp> GetTimestamp(IUser user);

        /// <summary>
        /// With the parameters date and user a list of timestamps(beginning with the given date and ending with today) can be selected and 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        List<ITimestamp> GetTimestamp(DateTime date, IUser user);



        int CreateTimeStamp(ITimestamp timestamp);


        int ModifyTimeStamp(ITimestamp timestamp);
    }
}
