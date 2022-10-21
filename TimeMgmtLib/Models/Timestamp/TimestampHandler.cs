using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeMgmtLib.DataAccess;

namespace TimeMgmtLib.Models
{
    /// <summary>
    /// This enum is implemented, so the form just needs to set one of this 4 values
    /// </summary>
   public enum TimeStampPeriod
    {
        /// <summary>
        /// _timestamps will be filled with rows of last week of the user
        /// </summary>
        OneWeek,
        /// <summary>
        /// _timestamps will be filled with rows of last month of the user
        /// </summary>
        OneMonth,
        /// <summary>
        /// _timestamps will be filled with rows of last year of the user
        /// </summary>
        OneYear,
        /// <summary>
        /// _timestamps will be filled with every row of the user
        /// </summary>
        Everything
    }

    /// <summary>
    /// This class implements the functionality of ITimestampHandler
    /// and handles the information in form of lists for graphs
    /// </summary>
    public class TimestampHandler : ITimestampHandler
    {
        private TimeStampPeriod _duration;
        private DateTime _date;
        private List<ITimestamp> _timestamps;


        public TimeStampPeriod GetTimeDuration()
        {
            return _duration;
        }

        /// <summary>
        /// this method must be called before trying to get any Timstamp Information
        /// </summary>
        /// <param name="value"></param>
        public void SetTimeDuration(TimeStampPeriod value)
        {
            _duration = value;
            CalcDate();
            FillTimeStamps();
        }

        /// <summary>
        /// filling _timestamps with database data of current user
        /// </summary>
        private void FillTimeStamps()
        {
            _timestamps = TimeMgmtFactory.Instance.Resolve<IDataAccessable>().GetTimestamp(_date, TimeMgmtFactory.Instance.UserInstance);
        }

        /// <summary>
        /// substract requested timespan from current date
        /// </summary>
        private void CalcDate()
        {
            if (_duration == TimeStampPeriod.OneWeek)
            {
                _date = DateTime.Today.AddDays(-7);
            }
            else if (_duration == TimeStampPeriod.OneMonth)
            {
                _date = DateTime.Today.AddMonths(-1);
            }
            else if (_duration == TimeStampPeriod.OneYear)
            {
                _date = DateTime.Today.AddYears(-1);
            }
            else if (_duration == TimeStampPeriod.Everything)
            {
                _date = TimeMgmtFactory.Instance.UserInstance.GetRegistrationDate();
            }
        }

        /// <summary>
        /// This method returns the checkIn time in form of a double List, so it can be directly put in the graph
        /// </summary>
        /// <returns>double List</returns>
        public List<double> GetCheckIn()
        {
            List<double> checkIn = new List<double>();

            foreach (var timestamp in _timestamps)
            {
                if (timestamp.GetDate() >= _date)
                {
                    checkIn.Add(timestamp.GetCheckIn().Hour + timestamp.GetCheckIn().Minute / 60);
                }
            }
            return checkIn;
        }

        /// <summary>
        /// This method returns the checkIn time in form of a double List, so it can be directly put in the graph
        /// </summary>
        /// <returns>double List</returns>
        public List<double> GetCheckOut()
        {
            List<double> checkOut = new List<double>();

            foreach (var timestamp in _timestamps)
            {
                if (timestamp.GetDate() >= _date)
                {
                    checkOut.Add(timestamp.GetCheckOut().Hour + timestamp.GetCheckOut().Minute / 60);
                }
            }
            return checkOut;
        }

        /// <summary>
        /// This method returns the flexibleHours in form of a double List, so it can be directly put in the graph
        /// </summary>
        /// <returns>double List</returns>
        public List<double> GetFlexibleHours()
        {
            List<double> flexibleHours = new List<double>();

            double fh = TimeMgmtFactory.Instance.UserInstance.GetFlexibleHoursAtRegistration();
            foreach (var timestamp in _timestamps)
            {
                fh += timestamp.GetFlexibleHours().TotalHours;
                flexibleHours.Add(fh);
            }
            return flexibleHours;
        }

        /// <summary>
        /// This method returns the flexibleHours/day in form of a double List, so it can be directly put in the graph
        /// </summary>
        /// <returns>double List</returns>
        public List<double> GetFlexibleHoursPerDay()
        {
            List<double> flexibleHours = new List<double>();

            foreach (var timestamp in _timestamps)
            {
                if (timestamp.GetDate() >= _date)
                {
                    flexibleHours.Add(timestamp.GetFlexibleHours().TotalHours);
                }
            }
            return flexibleHours;
        }
    }
}
