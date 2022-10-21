using System.Collections.Generic;

namespace TimeMgmtLib.Models
{
    public interface ITimestampHandler
    {
        List<double> GetCheckIn();
        List<double> GetCheckOut();
        List<double> GetFlexibleHours();
        List<double> GetFlexibleHoursPerDay();
        TimeStampPeriod GetTimeDuration();
        void SetTimeDuration(TimeStampPeriod value);
    }
}