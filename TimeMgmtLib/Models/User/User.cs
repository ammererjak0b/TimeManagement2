using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TimeMgmtLib.DataAccess;

namespace TimeMgmtLib.Models
{
    /// <summary>
    /// This user class represents the user that is currently loged in.
    /// </summary>
    public class User : IUser
    {
        private int _personalId;
        private string _prename;
        private string _surname;
        private DateTime _registrationDate;
        private double _flexibleHoursAtRegistration;
        private string _hashedPassword;

        private bool _isLoggedIn = false;


        public User()
        {
            
        }

        /// <summary>
        /// this constructor will be called before the user logs in
        /// </summary>
        /// <param name="personalId"></param>
        /// <param name="password"></param>
        public User(int personalId, string password)
        {
            _personalId = personalId;
            SetPassword(password);
        }

        /// <summary>
        /// this constructor will be called when a user is pulled from the database
        /// </summary>
        /// <param name="row"></param>
        public User(System.Data.DataRow row)
        {
            _personalId = Convert.ToInt32(row["Personal_Id"]);
            _prename = row["Prename"].ToString();
            _surname = row["Surname"].ToString();
            _registrationDate = Convert.ToDateTime(row["RegistrationDate"]);
            _flexibleHoursAtRegistration = Convert.ToDouble(row["flexibleHoursAtRegistration"]);
            _hashedPassword = row["pw_hashed"].ToString();
        }

        /// <summary>
        /// Sets the login state on true
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void Login()
        {
            IUser userInDb = TimeMgmtFactory.Instance.Resolve<IDataAccessable>().GetUser(this);
            if(userInDb == null)
            {
                throw new Exception("Error while user login | user could not found");
            }
            else if(_personalId == userInDb.GetPersonalId() && CompareHashValues(userInDb.GetHashedPassword(), _hashedPassword))
            {
                _prename = userInDb.GetPrename();
                _surname = userInDb.GetSurname();
                _registrationDate = userInDb.GetRegistrationDate();
                _flexibleHoursAtRegistration = userInDb.GetFlexibleHoursAtRegistration();
                _isLoggedIn = true;
            }
        }

        /// <summary>
        /// registers the user and safes it to the database, so he can log in from now on
        /// </summary>
        /// <param name="prename"></param>
        /// <param name="surname"></param>
        /// <param name="registrationDate"></param>
        /// <param name="flexibleHoursAtRegistration"></param>
        public void Register(string prename, string surname, DateTime registrationDate, double flexibleHoursAtRegistration)
        {
            _prename = prename;
            _surname = surname;
            _registrationDate = registrationDate;
            _flexibleHoursAtRegistration = flexibleHoursAtRegistration;

            TimeMgmtFactory.Instance.Resolve<IDataAccessable>().RegisterUser(this);
        }

        public int GetPersonalId()
        {
            return _personalId;
        }
        public string GetPrename()
        {
            return _prename;
        }
        public void SetPrename(string value)
        {
            _prename = value;
        }
        public string GetSurname()
        {
            return _surname;
        }
        public void SetSurname(string value)
        {
            _surname = value;
        }
        public double GetFlexibleHoursAtRegistration()
        {
            return _flexibleHoursAtRegistration;
        }
        public DateTime GetRegistrationDate()
        {
            return _registrationDate;
        }
        public bool GetIsLoggedIn()
        {
            return _isLoggedIn;
        }
        public string GetHashedPassword()
        {
            return _hashedPassword;
        }
        
        /// <summary>
        /// password will be hashed before assigned to the field
        /// </summary>
        /// <param name="value"></param>
        public void SetPassword(string value)
        {
            _hashedPassword = HashString(value);
        }
        
        /// <summary>
        /// turns a string into a hashed string, using the MD5CryptoServiceProvider
        /// </summary>
        /// <param name="value"></param>
        /// <returns>hashedString</returns>
        private string HashString(string value)
        {
            byte[] tmpSource = ASCIIEncoding.ASCII.GetBytes(value);
            byte[] tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
            return ByteArrayToString(tmpHash);
        }

        /// <summary>
        /// comparing 2 hash strings. If they are the same true will be returned, if not then false.
        /// </summary>
        /// <param name="soll"></param>
        /// <param name="ist"></param>
        /// <returns>bool</returns>
        private bool CompareHashValues(string soll, string ist)
        {
            bool bEqual = false;
            if (ist.Length == soll.Length)
            {
                int i = 0;
                while ((i < soll.Length) && (soll[i] == ist[i]))
                {
                    i += 1;
                }
                if (i == soll.Length)
                {
                    bEqual = true;
                }
            }

            return bEqual;
        }

        private string ByteArrayToString(byte[] arrInput)
        {
            int i;
            StringBuilder sOutput = new StringBuilder(arrInput.Length);
            for (i = 0; i < arrInput.Length - 1; i++)
            {
                sOutput.Append(arrInput[i].ToString("X2"));
            }
            return sOutput.ToString();
        }
    }
}
