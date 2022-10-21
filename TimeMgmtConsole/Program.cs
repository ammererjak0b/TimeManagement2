using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeMgmtLib;
using TimeMgmtLib.DataAccess;
using TimeMgmtLib.Models;

namespace TimeMgmtConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TimeMgmtFactory timebFac = TimeMgmtFactory.Instance;
            timebFac.Initialise();

            //ITimestamp timestamp = new Timestamp(5167, new DateTime(2021, 12, 6), new CheckTime(7, 10), new CheckTime(16, 15));

            timebFac.UserInstance = new User(5167, "Liebherr187");
            timebFac.UserInstance.Login();
            ITimestamp timestamp = timebFac.Resolve<IDataAccessable>().GetSpecificTimestamp(new DateTime(2021, 12, 6), timebFac.UserInstance);
            timebFac.Resolve<IDataAccessable>().ModifyTimeStamp(timestamp);
            
            Console.ReadLine();
        }
    }
}
