using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeMgmtLib.Models
{
    public  class CheckTime
    {
        public int Hour { get; set; }
        public int Minute { get; set; }

        public CheckTime(int hour, int minute)
        {
            Hour = hour;
            Minute = minute;
        }
    }
}
