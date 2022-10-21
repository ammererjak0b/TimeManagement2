using System;

namespace TimeMgmtLib.Models
{
    /// <summary>
    /// This interface contains all the functionalities of a user
    /// Because using interfaces we are not dependant of specific implementations
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// Sets the login state on true
        /// </summary>
        void Login();

        /// <summary>
        /// registers the user and safes it so he can login from now on
        /// </summary>
        /// <param name="prename"></param>
        /// <param name="surname"></param>
        /// <param name="registrationDate"></param>
        /// <param name="flexibleHoursAtRegistration"></param>
        void Register(string prename, string surname, DateTime registrationDate, double flexibleHoursAtRegistration);

        int GetPersonalId();
        string GetPrename();
        void SetPrename(string value);
        string GetSurname();
        void SetSurname(string value);
        string GetHashedPassword();
        void SetPassword(string value);
        DateTime GetRegistrationDate();
        bool GetIsLoggedIn();
        double GetFlexibleHoursAtRegistration();
    }
}