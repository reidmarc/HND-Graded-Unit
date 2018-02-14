// Commented

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using System.Data.SQLite; // Needed for SQLite functionality

namespace SimplyRugby
{
    public class Validation
    {
        #region Object and Variable

        // Instantiates a new object of the Database Class.
        private Database simplyRugbyDatabaseValidationClass = new Database();

        // Sets the path of the stored database as a string called 'simplyRugbyDBConnectionString'.
        private string simplyRugbyDatabaseConnectionString = @"Data Source=Database/SimplyRugbyDatabase.db;Version=3;";



        #endregion

        #region Account Creation / Registration Validation Methods

        // Method which checks if the entered username is made up from
        // Only letters and numbers and that it is 4 or more characters long.
        // Returns a boolean value - false, username failed validation - true, username passed validation
        public bool AccountUsernameCreationValidation(string username)
        {
            if (username.Length < 4)
            {
                MessageBox.Show("The username must be 4 or more characters long.");
                return false;
            }

            else if (username.All(char.IsLetterOrDigit) == false)
            {
                MessageBox.Show("You have entered an incorrect character for the username.");
                return false;
            }

            else
            {
                return true;
            }
        }

        // Method which checks if the entered username is already stored on the database
        // Returns a boolean value - true, username exists - false, username does not exist
        public bool DoesItExist(string query)
        {
            // Initialises a connection to the Database based on the path which is stored as the string 'SimplyRugbyDBConnectionString'.
            SQLiteConnection sqliteConnection = new SQLiteConnection(simplyRugbyDatabaseConnectionString);

            // try-catch incase there are any issues with retrieving values from the database
            try
            {
                // Calls the method SetUpDataReader() sends the database connection and the string 'query'
                // Returns with the values from the query and stores these values as a SQLiteDataReader called doesItExistCheck
                SQLiteDataReader doesItExistCheck = simplyRugbyDatabaseValidationClass.SetUpDataReader(sqliteConnection, query);

                // The program will enter this IF statement, only when there are rows for the data reader to read through
                // Which means the username was found
                if (doesItExistCheck.HasRows)
                {
                    // Calls the Close method from the SQLiteDataReader class to finish reading.
                    doesItExistCheck.Close();

                    // Calls the Close() method from the SQLiteConnection class to close the connection with the database
                    sqliteConnection.Close();
                    return true;
                }
                else
                {
                    // Calls the Close method from the SQLiteDataReader class to finish reading.
                    doesItExistCheck.Close();

                    // Calls the Close() method from the SQLiteConnection class to close the connection with the database
                    sqliteConnection.Close();
                    return false;
                }
            }

            // In the event of an error a MessageBox displays the specific SQLite error and does not allow the application crash.
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);

                // Calls the Close() method from the SQLiteConnection class to close the connection with the database
                sqliteConnection.Close();
                return true;
            }

        }

        // Method which checks if the entered SRU number is already stored on the database
        // Returns a boolean value - true, SRU number exists - false, SRU number does not exist
        public bool DoesItExist(string sruNo, string query)
        {
            // Initialises a connection to the Database based on the path which is stored as the string 'SimplyRugbyDBConnectionString'.
            SQLiteConnection sqliteConnection = new SQLiteConnection(simplyRugbyDatabaseConnectionString);

            // This IF statement allows the multiple enteries of "N/A" for every non playing members SRU number.
            if (sruNo == "N/A")
            {
                return false;
            }

            else
            {

                // try-catch incase there are any issues with retrieving values from the database
                try
                {
                    // Calls the method SetUpDataReader() sends the database connection and the string 'query'
                    // Returns with the values from the query and stores these values as a SQLiteDataReader called doesItExistCheck
                    SQLiteDataReader doesItExistCheck = simplyRugbyDatabaseValidationClass.SetUpDataReader(sqliteConnection, query);

                    // The program will enter this IF statement, only when there are rows for the data reader to read through
                    // Which means the username was found
                    if (doesItExistCheck.HasRows)
                    {
                        // Calls the Close method from the SQLiteDataReader class to finish reading.
                        doesItExistCheck.Close();

                        // Calls the Close() method from the SQLiteConnection class to close the connection with the database
                        sqliteConnection.Close();
                        return true;
                    }
                    else
                    {
                        // Calls the Close method from the SQLiteDataReader class to finish reading.
                        doesItExistCheck.Close();

                        // Calls the Close() method from the SQLiteConnection class to close the connection with the database
                        sqliteConnection.Close();
                        return false;
                    }
                }

                // In the event of an error a MessageBox displays the specific SQLite error and does not allow the application crash.
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.Message);

                    // Calls the Close() method from the SQLiteConnection class to close the connection with the database
                    sqliteConnection.Close();
                    return true;
                }
            }
        }

        // Method which checks that only allowed characters, numbers, symbols and punctuation are used
        // And that there are no spaces used
        // And that the length of the password is 6 or more
        // Returns a boolean value - false, password has failed validation - true, password has passed validation
        public bool AccountPasswordCreationValidation(string password)
        {
            if ((password.Any(char.IsSymbol) || password.Any(char.IsPunctuation)) == false)
            {
                MessageBox.Show("You must enter a symbol or punctuation for the password.");
                return false;
            }

            else if ((password.Any(char.IsLetterOrDigit) == false))
            {
                MessageBox.Show("You must enter letters or numbers for the password.");
                return false;
            }

            else if ((password.Any(char.IsWhiteSpace) == true))
            {
                MessageBox.Show("You cannot enter spaces for the password.");
                return false;
            }

            else if (password.Length >= 6 == false)
            {
                MessageBox.Show("The password must be 6 or more characters long .");
                return false;
            }

            else
            {
                return true;
            }
        }

        #endregion

        #region Member Record Creation / Update Validation Methods

        // Method which checks the name entered is 2 or more characters long, that none of the characters
        // Are numbers or punctuation and that only spaces haven't been entered
        // Returns a boolean value - false, name has failed validation - true, name has passed validation
        public bool MemberNameValidation(string name)
        {
            if (name.Length < 2)
            {
                MessageBox.Show("You have entered an incorrect amount of characters for the member's name.");
                return false;
            }

            else if (name.Any(char.IsDigit) == true)
            {
                MessageBox.Show("You cannot enter a number for the member's name.");
                return false;
            }

            else if (name.Any(char.IsPunctuation) == true)
            {
                MessageBox.Show("You cannot enter punctuation for the member's name.");
                return false;
            }

            else if (name.Any(char.IsSymbol) == true)
            {
                MessageBox.Show("You cannot enter a symbol for the member's name.");
                return false;
            }

            else if (name.All(char.IsWhiteSpace) == true)
            {
                MessageBox.Show("You cannot all spaces for the member's name.");
                return false;
            }

            else
            {
                return true;
            }
        }

        // Method which checks if the member record needs a SRU no by making sure the membershipType is Playing Member
        // Then the method checks the length of the string is 9 characters long and is made up from only numbers
        // Returns a boolean value - true, the member is either a Non-Playing member or the sruNo has passed validation
        // And False, the sruNo has failed validation
        public bool SRUNoValidation(string sruNo)
        {
            if (sruNo == "N/A")
            {
                return true;
            }

            else if (sruNo.Length != 9)
            {
                MessageBox.Show("The SRU Number must contain 9 digits.");
                return false;
            }

            else if (sruNo.All(char.IsDigit) == false)
            {
                MessageBox.Show("The SRU Number must contain only numbers.");
                return false;
            }

            else
            {
                return true;
            }
        }

        // Method which checks that the email address enter has a '@' symbol and a '.' 
        // Then checks for no spaces and that the length of the email address is longer than 3 characters long.
        // Returns a boolean value - false, emailAddress has failed validation - true, emailAddress has pass validation
        public bool EmailValidation(string emailAddress)
        {
            if (!emailAddress.Contains("@"))
            {
                MessageBox.Show("The Email Address must contain a '@' symbol.");
                return false;
            }

            if (!emailAddress.Contains("."))
            {
                MessageBox.Show("The Email Address must contain a '.' symbol.");
                return false;
            }

            else if (emailAddress.Any(char.IsWhiteSpace))
            {
                MessageBox.Show("The Email Address must not contain any spaces.");
                return false;
            }

            else if (emailAddress.Length < 4)
            {
                MessageBox.Show("The Email Address is not long enough.");
                return false;
            }

            else
            {
                return true;
            }
        }

        // Method which checks the length of the phone number entered is equal to 11 and that
        // All characters are numbers.
        // Returns a boolean value - false, phoneNo has failed validation - true, phoneNo has passed validation
        public bool PhoneNoValidation(string phoneNo)
        {
            if (phoneNo.Length != 11)
            {
                MessageBox.Show("The Phone Number must contain 11 digits only.");
                return false;
            }

            else if (phoneNo.All(char.IsDigit) == false)
            {
                MessageBox.Show("The Phone Number must contain only numbers.");
                return false;
            }

            else
            {
                return true;
            }
        }

        // Method which checks that the value entered for the year of the date of birth is equal to 4 characters long
        // Then the method checks that all 4 characters are numbers, then the method compares the year entered with the current year
        // to make sure the year entered is not in the future
        // Returns a boolean value - false, the year has failed validation - true, the year has passed validation
        public bool DateOfBirthYearValidation(string year)
        {
            if (year.Length != 4)
            {
                MessageBox.Show("You have entered an incorrect value for the year of the date of birth, it must be 4 digits.");
                return false;
            }

            else if (year.All(char.IsDigit) == false)
            {
                MessageBox.Show("You have entered an incorrect value for the year of the date of birth, it must all numbers.");
                return false;
            }

            // Sets up an int called 'currentYear' to be equal to the current year.
            int currentYear = DateTime.Now.Year;

            // Converts the string 'year' to an int and calls it 'enteredYear' 
            //(Don't forsee any problems converting, as the string has already been validated to only contain numbers)
            int enteredYear = Convert.ToInt32(year);

            // Compares both int values
            if (currentYear < enteredYear)
            {
                MessageBox.Show("The year you have entered for the date of birth is in the future");
                return false;
            }

            else
            {
                return true;
            }
        }

        // Method which checks the value entered for the month of date of birth is contained in the array allowedMonths
        // Returns a boolean value - false, month is not in the array - true, month is in the array
        public bool DateOfBirthMonthValidation(string month)
        {
            string[] allowedMonths = new[] { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" };

            if (!allowedMonths.Any(month.Contains))
            {
                MessageBox.Show("You have entered an incorrect value for the month of the date of birth");
                return false;
            }

            else
            {
                return true;
            }
        }

        // Method which checks the value entered for the day of date of birth is valid based on the previously validated month and year values.
        // Returns a boolean value - false, day has failed validation - true, day has passed validation
        public bool DateOfBirthDayValidation(string day, string month, string year)
        {
            // Set up an array of strings to hold the valid values of days, for months with 31 days
            string[] allowedDaysFor31DayMonths = new[] { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31" };

            // Set up an array of strings to hold the valid values of days, for months with 30 days
            string[] allowedDaysFor30DayMonths = new[] { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30" };

            // Set up an array of strings to hold the valid values of days, for months with 29 days
            string[] allowedDaysFor29DayMonths = new[] { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29" };

            // Set up an array of strings to hold the valid values of days, for months with 28 days
            string[] allowedDaysFor28DayMonths = new[] { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28" };

            // Set up an array of strings to hold the valid values which represent the  months with 31 days
            string[] monthsWith31Days = new[] { "01", "03", "05", "07", "08", "10", "12" };

            // Set up an array of strings to hold the valid values which represent the  months with 30 days
            string[] monthsWith30Days = new[] { "04", "06", "09", "11" };

            // Set up an array of strings to hold the valid values which represent the  months with 28 and 29 days
            string[] monthsWith28and29Days = new[] { "02" };

            // Converts the string 'year' to an int and calls it 'enteredYear' 
            //(Don't forsee any problems converting, as the string has already been validated to only contain numbers)
            int enteredYear = Convert.ToInt32(year);


            // The program will enter this IF statement if the value for 'month' is found in the 'monthsWith31Days' array
            if (monthsWith31Days.Any(month.Contains))
            {
                // The program will enter this IF statement if the value for 'day' is not found in the 'allowedDaysFor31DayMonths' array
                if (!allowedDaysFor31DayMonths.Any(day.Contains))
                {
                    MessageBox.Show("You have entered an incorrect value for the day of the date of birth");
                    return false;
                }

                else
                {
                    return true;
                }
            }

            // The program will enter this ELSE IF statement if the value for 'month' is found in the 'monthsWith30Days' array
            else if (monthsWith30Days.Any(month.Contains))
            {
                if (!allowedDaysFor30DayMonths.Any(day.Contains))
                {
                    // The program will enter this IF statement if the value for 'day' is not found in the 'allowedDaysFor30DayMonths' array
                    MessageBox.Show("You have entered an incorrect value for the day of the date of birth");
                    return false;
                }

                else
                {
                    return true;
                }
            }

            // The program will enter this ELSE IF statement if the value for 'month' is found in the 'monthsWith28and29Days' array
            else if (monthsWith28and29Days.Any(month.Contains))
            {
                // The program will enter this IF statement if the value for 'enteredYear' is not a leap year
                if (DateTime.IsLeapYear(enteredYear) == false)
                {
                    // The program will enter this IF statement if the value for 'day' is not found in the 'allowedDaysFor28DayMonths' array
                    if (!allowedDaysFor28DayMonths.Any(day.Contains))
                    {
                        MessageBox.Show("You have entered an incorrect value for the day of the date of birth");
                        return false;
                    }

                    else
                    {
                        return true;
                    }
                }

                // The program will enter this ELSE statement if the value for 'enteredYear' is a leap year
                else
                {
                    // The program will enter this IF statement if the value for 'day' is not found in the 'allowedDaysFor29DayMonths' array
                    if (!allowedDaysFor29DayMonths.Any(day.Contains))
                    {
                        MessageBox.Show("You have entered an incorrect value for the day of the date of birth");
                        return false;
                    }

                    else
                    {
                        return true;
                    }
                }
            }

            // The program will enter this ELSE statement if there is an issue with the value for 'month' entered.
            // Previous validation should ensure that this else is not needed.
            else
            {
                MessageBox.Show("There has been an issue with the date of birth entered, please try again.");
                return false;
            }
        }

        // Method which makes sure the user has selected a radiobutton to indicate which membership type the
        // member is
        // Returns a boolean value - false, the user has not made a selection and must do so to continue
        // And - true, the user has made a selection.
        public bool MembershipTypeValidation(string membershipType)
        {
            if (membershipType == null)
            {
                MessageBox.Show("You must select from the two choices of membership type.");
                return false;
            }

            else
            {
                return true;
            }
        }

        #endregion

        #region User's Choice Validation Methods

        // Method which displays a messagebox asking the user if they are sure they want to exit the application
        // And gives them the option of answering 'yes' or 'no'
        public void ExitApplicationValidation()
        {
            MessageBoxResult yesOrNo = MessageBox.Show("Are you sure you want to exit the application?", "Exit Application", MessageBoxButton.YesNo);

            if (yesOrNo == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }

            else
            {
                return;
            }
        }

        // Method which displays a messagebox asking the user if they are sure they want to delete the selected members record
        // And gives them the option of answering 'yes' or 'no'
        public bool DeleteRecordValidation(string memberName)
        {
            MessageBoxResult yesOrNo = MessageBox.Show("Are you sure you want to delete the record for '" + memberName + "'?", "Delete Record", MessageBoxButton.YesNo);

            if (yesOrNo == MessageBoxResult.No)
            {
                return false;
            }

            else
            {
                return true;
            }
        }

        // Method which displays a messagebox asking the user if they are sure they want to delete the selected account
        // And gives them the option of answering 'yes' or 'no'
        public bool DeleteAccountValidation(string username)
        {
            MessageBoxResult yesOrNo = MessageBox.Show("Are you sure you want to delete the account of username: '" + username + "'?", "Delete Account", MessageBoxButton.YesNo);

            if (yesOrNo == MessageBoxResult.No)
            {
                return false;
            }

            else
            {
                return true;
            }
        }

        #endregion
    }
}
