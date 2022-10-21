using System;

namespace TimeMgmtLib.Models
{
    /// <summary>
    /// This interface contains all the functionalities of a timestamp
    /// Because using interfaces we are not dependant of specific implementations
    /// </summary>
    public interface ITimestamp
    {
        int GetTimeStampId();
        int GetPersonalId();
        DateTime GetDate();
        TimeSpan GetFlexibleHours();
        TimeSpan GetWorkHours();
        CheckTime GetCheckIn();
        /// <summary>
        /// sets checkIn and recalculates workhours
        /// </summary>
        /// <param name="checkIn"></param>
        void SetCheckIn(CheckTime checkIn);
        CheckTime GetCheckOut();
        /// <summary>
        /// sets checkOut and recalculates workhours
        /// </summary>
        /// <param name="checkOut"></param>
        void SetCheckOut(CheckTime checkOut);
    }
}