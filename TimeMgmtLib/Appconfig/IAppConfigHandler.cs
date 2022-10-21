using TimeMgmtLib.Models;

namespace TimeMgmtLib.Appconfig
{
    public interface IAppConfigHandler
    {
        string GetColorTheme();
        CheckTime GetDailyCheckIn();
        CheckTime GetDailyCheckOut();
        string GetDbConnectionString();
        string GetPasswordHashed();
        bool GetStayLoggedIn();
        string GetUserPersonalId();
        void SetColorTheme(string value);
        void SetDailyCheckIn(CheckTime value);
        void SetDailyCheckOut(CheckTime value);
        void SetPasswordHashed(string value);
        void SetStayLoggedIn(bool value);
        void SetUserPersonalId(string value);
    }
}