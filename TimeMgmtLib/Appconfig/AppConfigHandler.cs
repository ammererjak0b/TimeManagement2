using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using TimeMgmtLib.Models;

namespace TimeMgmtLib.Appconfig
{
    public sealed class AppConfigHandler : IAppConfigHandler
    {

        public string GetDbConnectionString()
        {
            return GetValueFromKey("dBConnectionString");
        }

        public string GetUserPersonalId()
        {
            return GetValueFromKey("userPersonalId");
        }

        public void SetUserPersonalId(string value)
        {
            UpdateAppSettings("userPersonalId", value);
        }

        public string GetPasswordHashed()
        {
            return GetValueFromKey("userPasswordHashed");
        }

        public void SetPasswordHashed(string value)
        {
            UpdateAppSettings("userPasswordHashed", value);
        }

        public string GetColorTheme()
        {
            return GetValueFromKey("colorTheme");
        }

        public void SetColorTheme(string value)
        {
            UpdateAppSettings("colorTheme", value);
        }

        public bool GetStayLoggedIn()
        {
            return Convert.ToBoolean(GetValueFromKey("stayLoggedIn"));
        }

        public void SetStayLoggedIn(bool value)
        {
            UpdateAppSettings("stayLoggedIn", value.ToString());
        }

        public CheckTime GetDailyCheckIn()
        {
            int hour = Convert.ToInt32(GetValueFromKey("dailyCheckInHour"));
            int minute = Convert.ToInt32(GetValueFromKey("dailyCheckInMinute"));

            return new CheckTime(hour, minute);
        }

        public void SetDailyCheckIn(CheckTime value)
        {
            UpdateAppSettings("dailyCheckInHour", value.Hour.ToString());
            UpdateAppSettings("dailyCheckInMinute", value.Minute.ToString());
        }

        public CheckTime GetDailyCheckOut()
        {
            int hour = Convert.ToInt32(GetValueFromKey("dailyCheckOutHour"));
            int minute = Convert.ToInt32(GetValueFromKey("dailyCheckOutMinute"));

            return new CheckTime(hour, minute);
        }

        public void SetDailyCheckOut(CheckTime value)
        {
            UpdateAppSettings("dailyCheckOutHour", value.Hour.ToString());
            UpdateAppSettings("dailyCheckOutMinute", value.Minute.ToString());
        }

        /// <summary>
        /// The function returns the stored value in the app config from the given key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private string GetValueFromKey(string key)
        {
            try
            {
                return ConfigurationManager.AppSettings[key];
            }
            catch (Exception e)
            {
                throw new Exception($"Error while trying to read the folling appconfig Key: { key } | Errormessage: { e.Message } | InnerException: { e.InnerException } | Stacktrace: {e.StackTrace}");
            }
        }

        public static void UpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException ex)
            {
                throw new ConfigurationErrorsException($"Error writing to the app config | {ex.Message}");
            }
        }


    }
}
