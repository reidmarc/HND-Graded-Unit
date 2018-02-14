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

using System.Windows.Media.Animation; // Needed for animated logo
using System.Data.SQLite; // Needed for SQLite functionality

namespace SimplyRugby
{
    public partial class LogIn : Page
    {
        #region Objects and Variables

        // Instantiates a new object of the Validation Class.
        private Validation validation = new Validation();

        // Instantiates a new object of the PasswordSecurity Class.
        private PasswordSecurity hashedPassword = new PasswordSecurity();

        // A string set up to store the value of the account type based on the username entered.
        private string loginAccountType;

        // A string set up to store the hashed password retrieved for the username entered, so that it can be compared with the entered password.
        private string hashedPasswordOfLoginUsername;

        // Sets the path of the stored database as a string called 'SimplyRugbyDBConnectionString'.
        private string SimplyRugbyDatabaseConnectionString = @"Data Source=Database/SimplyRugbyDatabase.db;Version=3;";

        // A string set up to store the username entered
        string loginUsernameEntered;

        // A string set up to store the password entered
        string loginPasswordEntered;

        #endregion

        #region Constructor

        public LogIn()
        {
            InitializeComponent();
        }

        #endregion

        #region Login Button Method

        // Method which handles the 'Login' button being clicked
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // A string set up to store the username entered in the textbox 'usernameInput'
            loginUsernameEntered = usernameInput.Text;

            // A string set up to store the password entered in the passwordbox 'passwordInput'
            loginPasswordEntered = passwordInput.Password;

            // An IF statment which handles if the Username exists on the database
            // Calls the DoesThisAccountUsernameExist() method from the Validation class 
            // Passes the value stored for the string 'loginUsernameEntered'.
            // Enters the IF statement when the boolean value 'false' is returned
            if (validation.DoesItExist("SELECT * FROM login WHERE Username='" + loginUsernameEntered + "'") == false)
            {
                MessageBox.Show("There is problem with your login details.\n\nPlease try again.");

                // Clears the content of the textbox 'usernameInput'
                usernameInput.Text = "";

                // Clears the content of the passwordbox 'passwordInput'
                passwordInput.Password = "";

                return;
            }

            GetPasswordAndAccountType();

            // An ELSE IF statment which handles if the Username entered and the password entered are the same
            // If they are the same, the application will display a message informing the user that this is the first time that account
            // Has logged in and will have to register the account before being able to use the account further.
            // The user will be navigated to the registration page.
            if (loginUsernameEntered == loginPasswordEntered)
            {

                // The program will only enter this IF statement if the method ValidatePassword() returns false
                // Meaning that the stored password and the entered password do not match.
                if (hashedPassword.IsThePasswordAMatch(loginPasswordEntered, hashedPasswordOfLoginUsername) == false)
                {
                    MessageBox.Show("There is problem with your login details.\n\nPlease try again.");

                    // Clears the content of the textbox 'usernameInput'
                    usernameInput.Text = "";

                    // Clears the content of the passwordbox 'passwordInput'
                    passwordInput.Password = "";

                    return;
                }
                // ELSE if the passwords do match then the program will enter this ELSE
                else
                {
                    // A message to the user informing them that they must register first before being able to login
                    MessageBox.Show("As this is your first time logging in with this account, you will be redirected to the registration page where you can set your username and password.");

                    // Clears the content of the textbox 'usernameInput'
                    usernameInput.Text = "";

                    // Clears the content of the passwordbox 'passwordInput'
                    passwordInput.Password = "";

                    // Instantiate an object of the Registration page
                    Registration registrationPage = new Registration(loginUsernameEntered);

                    // Navigates to the Registration page
                    NavigationService.Navigate(registrationPage);
                }
            }

            // An ELSE statement which handles If the Username is found on the database and when the password does not match the username
            // (This should be the standard log in)
            else
            {
                // The program will only enter this IF statement if the method ValidatePassword() returns false
                // Meaning that the stored password and the entered password do not match.
                if (hashedPassword.IsThePasswordAMatch(loginPasswordEntered, hashedPasswordOfLoginUsername) == false)
                {
                    MessageBox.Show("There is problem with your login details.\n\nPlease try again.");

                    // Clears the content of the textbox 'usernameInput'
                    usernameInput.Text = "";

                    // Clears the content of the passwordbox 'passwordInput'
                    passwordInput.Password = "";

                    return;
                }

                // If the stored password and entered password match then the program will enter this ELSE
                else
                {
                    // The program will only enter this IF statement if the value for the string 'loginAccountType' is set to "Admin"
                    if (loginAccountType == "Admin")
                    {
                        // Instantiate an object of the AdminStart page
                        AdminStart adminPage = new AdminStart();

                        // Navigates to the AdminStart page
                        NavigationService.Navigate(adminPage);
                    }

                    // The program will only enter this ELSE IF statement if the value for the string 'loginAccountType' is set to "Coach"
                    else if (loginAccountType == "Coach")
                    {
                        // Instantiate an object of the CoachStart page
                        CoachStart coachPage = new CoachStart();

                        // Navigates to the CoachStart page
                        NavigationService.Navigate(coachPage);
                    }

                    // The program will only enter this ELSE statement if there has been a problem setting the value for the string 'loginAccountType'
                    else
                    {
                        MessageBox.Show("There has been an error, please try again.");
                        return;
                    }
                }
            }
        }

        private void GetPasswordAndAccountType()
        {
            // Initialises a connection to the Database based on the path which is stored as the string 'SimplyRugbyDBConnectionString'.
            SQLiteConnection sqliteConnection = new SQLiteConnection(SimplyRugbyDatabaseConnectionString);

            // try-catch incase there are any issues with retrieving values from the database
            try
            {
                // Calls the Open() method from the SQLiteConnection class to open a connection with the database.
                sqliteConnection.Open();

                // A string set up to store a database query.
                string query = "SELECT * FROM login WHERE Username= '" + loginUsernameEntered + "' ";

                // Instantiates a new object of the SQLiteCommand class, using the string 'query' and the database connection
                SQLiteCommand createCommand = new SQLiteCommand(query, sqliteConnection);

                // Calls the method ExecuteReader() from the SQLiteCommand class, to create the SQLiteDataReader object  
                SQLiteDataReader dataReader = createCommand.ExecuteReader();

                // A while loop, which loops while there are values in the dataReader to be read
                while (dataReader.Read())
                {
                    // Reads the value stores in the 3rd column and then sets the string 'hashedPasswordOfLoginUsername'
                    // To equal the value read.
                    hashedPasswordOfLoginUsername = dataReader.GetString(2);

                    // Reads the value stores in the 4th column and then sets the string 'loginAccountType'
                    // To equal the value read.
                    loginAccountType = dataReader.GetString(3);
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
        }

        #endregion

        #region Logo Animation

        // Method which handles what happens when the mouse hovers over the logo
        private void LogoPicture_MouseEnter(object sender, MouseEventArgs e)
        {
            Image logo = (Image)sender;
            DoubleAnimation animation = new DoubleAnimation(0, TimeSpan.FromSeconds(2));

            logo.BeginAnimation(Image.OpacityProperty, animation);
        }

        // Method which handles what happens when the mouse moves away from the logo after having hovered over the logo
        private void LogoPicture_MouseLeave(object sender, MouseEventArgs e)
        {
            Image logo = (Image)sender;
            DoubleAnimation animation = new DoubleAnimation(1, TimeSpan.FromSeconds(1));

            logo.BeginAnimation(Image.OpacityProperty, animation);
        }

        #endregion

        #region Exit Button

        // Method which handles the 'Exit Application' button being clicked
        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            // Calls the method ExitApplicationValidation() from the Validation class.
            validation.ExitApplicationValidation();
        }

        #endregion





        // FOLLOWING METHODS ARE NOT FOR FULL RELEASE

        private void adminBtn_Click(object sender, RoutedEventArgs e)
        {
            AdminStart adminPage = new AdminStart();
            NavigationService.Navigate(adminPage);
        }

        private void coachBtn_Click(object sender, RoutedEventArgs e)
        {
            CoachStart coachPage = new CoachStart();
            NavigationService.Navigate(coachPage);
        }
    }
}

