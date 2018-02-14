using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BCrypt.Net; // Install-Package BCrypt.Net

namespace SimplyRugby
{
    public class PasswordSecurity
    {
        #region Variable

        // An integer set up to store a value for the strength of the salt (the higher the more secure, but more time consuming)
        private int saltStrength = 12;

        #endregion

        #region Salting and Hashing the Password

        // Creates a random value (salt)
        private string CreateRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(saltStrength);
        }

        // Takes the password entered then concatenates it with the salt created in GetRandomSalt() 
        // then hashes the whole string then returns the hashed string
        public string HashThePassword(string enteredPassword)
        {
            return BCrypt.Net.BCrypt.HashPassword(enteredPassword, CreateRandomSalt());

        }

        #endregion

        #region Verifying Password

        // Takes the password entered, then compares it with the hashed password stored for the username entered
        // returns a boolean value as to whether the password is verified or not.
        public bool IsThePasswordAMatch(string enteredPassword, string storedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(enteredPassword, storedPassword);
        }

        #endregion
    }
}