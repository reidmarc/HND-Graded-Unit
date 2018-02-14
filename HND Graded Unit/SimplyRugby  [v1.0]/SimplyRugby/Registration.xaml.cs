// Commented

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Data.SQLite;

namespace SimplyRugby
{
    public partial class Registration : Page
    {
        #region Objects and Variables

        // Instantiates a new object of the Database Class.
        private Database simplyRugbyDatabaseRegistrationPage = new Database();

        // Instantiates a new object of the Validation Class.
        private Validation validation = new Validation();

        // Instantiates a new object of the PasswordSecurity Class.
        private PasswordSecurity newPassword = new PasswordSecurity();

        // An integer set up to store the loginID that is associated with the username which is passed through the constructor
        private int loginLoginID;

        // A string set up to store the username value passed through the constructor
        private string loginUsernameEntered;

        // Sets the path of the stored database as a string called 'SimplyRugbyDBConnectionString'.
        private string SimplyRugbyDatabaseConnectionString = @"Data Source=Database/SimplyRugbyDatabase.db;Version=3;";

        #endregion

        #region Constructor

        public Registration(string username)
        {
            InitializeComponent();

            // The string 'username' value is passed from the Login page
            loginUsernameEntered = username;
        }

        #endregion

        #region Register Button Method

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            // Initialises a connection to the Database based on the path which is stored as the string 'SimplyRugbyDBConnectionString'.
            SQLiteConnection sqliteConnection = new SQLiteConnection(SimplyRugbyDatabaseConnectionString);

            // An string set up to store the username the user wants to register
            string registrationUsername = usernameInput.Text;

            // An string set up to store the username the user wants to register
            string registrationPasswordEntered = passwordInput.Password;

            // An IF statment which handles if no input has been made for the new username.
            if (registrationUsername == "")
            {
                MessageBox.Show("You cannot leave the username field blank");
            }

            // An IF statment which handles if no input has been made for the new password.
            else if (registrationPasswordEntered == "")
            {
                MessageBox.Show("You cannot leave the password field blank");
            }

            // An IF statment which handles if the new username entered fails it's validation
            // Calls the AccountUsernameCreationValidation() method from the Validation class 
            // Passes the string 'registrationUsername'
            // Enters the IF statement when the boolean value 'false' is returned
            else if (validation.AccountUsernameCreationValidation(registrationUsername) == false)
            {
                return;
            }

            // An ELSE IF statment which handles if the Username exists on the database
            // Calls the DoesThisAccountUsernameExist() method from the Validation class 
            // Passes the value stored for the string 'loginUsernameEntered'.
            // Enters the IF statement when the boolean value 'false' is returned
            else if (validation.DoesItExist("SELECT * FROM login WHERE Username='" + registrationUsername + "'") == true)
            {
                MessageBox.Show("That username already exists.\n\nPlease try another username");

                usernameInput.Text = "";
                passwordInput.Password = "";

                return;
            }

            // An IF statment which handles if the new password entered, fails it's validation
            // Calls the AccountPasswordCreationValidation() method from the Validation class 
            // Passes the string 'registrationPasswordEntered'            
            // Enters the IF statement when the boolean value 'false' is returned
            else if (validation.AccountPasswordCreationValidation(registrationPasswordEntered) == false)
            {
                return;
            }

            // The program will only enter this IF ELSE statement if the user enters the same value for their new username and password            
            else if (registrationUsername == registrationPasswordEntered)
            {
                MessageBox.Show("You cannot set your username and password as the same values.");
                return;
            }

            // The program will only enter this ELSE statement if the new username entered and the new password entered
            // Are not equal to each other and if the username and password pass all validation
            else
            {
                // A string set up to store the value of the new password entered after its been hashed
                string newHashedPassword = newPassword.HashThePassword(registrationPasswordEntered);

                // try-catch incase there are any issues with retrieving values from the database                
                try
                {
                    // Calls the Open() method from the SQLiteConnection class to open a connection with the database.
                    sqliteConnection.Open();

                    // A string set up to store a database query.
                    string query = "SELECT * FROM login WHERE Username= '" + loginUsernameEntered + "' ";

                    // Instantiates a new object of the SQLiteCommand Class, using the string 'query' and the database connection
                    SQLiteCommand sqliteCommand = new SQLiteCommand(query, sqliteConnection);

                    // Calls the method ExecuteReader() from the SQLiteCommand class, to create the SQLiteDataReader object  
                    SQLiteDataReader dataReader = sqliteCommand.ExecuteReader();

                    // A while loop, which loops while there are values in the dataReader to be read
                    while (dataReader.Read())
                    {
                        // Reads the Integer value for the 1st column returned, then stores it as the string 'loginLoginID'
                        loginLoginID = dataReader.GetInt32(0);
                    }

                    // Calls the Close() method from the SQLiteConnection class to close the connection with the database
                    sqliteConnection.Close();
                }

                // In the event of an error a MessageBox displays the specific SQLite error and does not allow the application crash.
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }

                // Calls the UpdateRecord() method from the Database class
                // Sends the database connection, new validated username, new validated then hashed password
                // And the loginID associated with the username, that was passed into the constructor
                simplyRugbyDatabaseRegistrationPage.UpdateRecord(sqliteConnection, registrationUsername, newHashedPassword, loginLoginID);

                // A messagebox informing the user that the registration process has been completed
                MessageBox.Show("" + registrationUsername + "'s account has been successfully registered.\n\nYou will now be taken to the Login page.");

                // Instantiate an object of the LogIn page
                LogIn logIn = new LogIn();

                // Navigates to the LogIn page
                NavigationService.Navigate(logIn);
            }
        }

        #endregion

        #region Navigation

        // Method which handles the 'Logout' button being clicked
        private void BackToLoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Instantiate an object of the LogIn page
            LogIn logIn = new LogIn();

            // Navigates to the LogIn page
            NavigationService.Navigate(logIn);
        }

        // Method which handles the 'Exit Application' button being clicked
        private void ExitAppButton_Click(object sender, RoutedEventArgs e)
        {
            // Calls the method ExitApplicationValidation() from the Validation class.
            validation.ExitApplicationValidation();
        }

        #endregion
    }
}