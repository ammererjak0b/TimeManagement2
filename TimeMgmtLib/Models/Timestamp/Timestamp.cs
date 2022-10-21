using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeMgmtLib.Models
{
    /// <summary>
    /// This class represents one Timestamp that the user submits to the database.
    /// </summary>
    public class Timestamp : ITimestamp
    {
        private int _timestampId;
        private int _personalId;
        private DateTime _date;
        private DateTime _checkIn;
        private DateTime _checkOut;

        private TimeSpan _workHours;

        /// <summary>
        /// this constructor will be called when a user creates a new timestamp
        /// </summary>
        /// <param name="timestampId"></param>
        /// <param name="personalId"></param>
        /// <param name="date"></param>
        /// <param name="checkIn"></param>
        /// <param name="checkOut"></param>
        public Timestamp(int personalId, DateTime date, CheckTime checkIn, CheckTime checkOut)
        {
            _personalId = personalId;
            _date = date;
            SetCheckIn(checkIn);
            SetCheckOut(checkOut);
        }

        /// <summary>
        /// this contsturctor will be called when a timestamp is taken from the database
        /// </summary>
        /// <param name="row"></param>
        public Timestamp(DataRow row)
        {
            _timestampId = Convert.ToInt32(row["Timestamp_Id"]);
            _personalId = Convert.ToInt32(row["Personal_Id"]);
            _date = Convert.ToDateTime(row["Date"]);
            _checkIn = Convert.ToDateTime(row["checkIn"]);
            _checkOut = Convert.ToDateTime(row["checkOut"]);
            
            CalculateWorkHours();
        }


        public int GetTimeStampId()
        {
            return _timestampId;
        }
        public int GetPersonalId()
        {
            return _personalId;
        }
        public DateTime GetDate()
        {
            return _date;
        }
        public CheckTime GetCheckIn()
        {
            return new CheckTime(_checkIn.Hour, _checkIn.Minute);
        }
        public CheckTime GetCheckOut()
        {
            return new CheckTime(_checkOut.Hour, _checkOut.Minute);
        }
        public TimeSpan GetFlexibleHours()
        {
            return _workHours.Subtract(new TimeSpan(8, 0, 0));
        }
        public TimeSpan GetWorkHours()
        {
            return _workHours;
        }

        /// <summary>
        /// sets checkIn and recalculates workhours
        /// </summary>
        /// <param name="checkIn"></param>
        public void SetCheckIn(CheckTime checkIn)
        {
            DateTime today = DateTime.Today;
            _checkIn = new DateTime(today.Year, today.Month, today.Day, checkIn.Hour, checkIn.Minute, 0);
            CalculateWorkHours();
        }

        /// <summary>
        /// sets checkOut and recalculates workhours
        /// </summary>
        /// <param name="checkOut"></param>
        public void SetCheckOut(CheckTime checkOut)
        {
            DateTime today = DateTime.Today;
            _checkOut = new DateTime(today.Year, today.Month, today.Day, checkOut.Hour, checkOut.Minute, 0);
            CalculateWorkHours();
        }
        /// <summary>
        /// With the values of checkOut and checkIn the workHours can be calculated
        /// </summary>
        private void CalculateWorkHours()
        {
            if(_checkIn != DateTime.MinValue && _checkOut != DateTime.MinValue)
            {
                _workHours = (_checkOut - _checkIn).Subtract(new TimeSpan(0, GetBreakMinutes(), 0));
            }
        }

        /// <summary>
        /// Gets the exact amount of break minutes on this day
        /// </summary>
        private int GetBreakMinutes()
        {
            int minutes = 0;

            if (_checkIn.Hour <= 9 && _checkOut.Hour >= 9)
            {
                if (_checkOut.Hour == 9 && _checkOut.Minute < 10)
                {
                    minutes += _checkOut.Minute;
                }
                else if (_checkIn.Hour == 9 && _checkIn.Minute < 10)
                {
                    minutes += (10 - _checkIn.Minute);
                }
                else if (_checkOut.Hour == 9 && _checkOut.Minute >= 10)
                {
                    minutes += 10;
                }
                else if (_checkIn.Hour == 9 && _checkIn.Minute >= 10)
                {

                }
                else if (_checkOut.Hour > 9)
                {
                    minutes += 10;
                }
            }

            if (_checkIn.Hour <= 12 && _checkOut.Hour >= 12)
            {
                if (_checkOut.Hour == 12 && _checkOut.Minute < 30)
                {
                    minutes += _checkOut.Minute;
                }
                else if (_checkIn.Hour == 12 && _checkIn.Minute < 30)
                {
                    minutes += (30 - _checkIn.Minute);
                }
                else if (_checkOut.Hour == 12 && _checkOut.Minute >= 30)
                {
                    minutes += 30;
                }
                else if (_checkIn.Hour == 12 && _checkIn.Minute >= 30)
                {

                }
                else if (_checkOut.Hour > 12)
                {
                    minutes += 30;
                }
            }
            return minutes;
        }


    }
}
