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

using System.Data.SQLite; // Needed for SQLite functionality


namespace SimplyRugby
{
    public partial class AdminAdd : Page
    {
        #region Objects, Constants and Variables

        // Instantiates a new object of the Database Class.
        private Database simplyRubgyDatabaseAdminAddPage = new Database();

        // Instantiates a new object of the Validation Class.
        private Validation inputDataValidation = new Validation();

        // Instantiates a new object of the PasswordSecurity Class.
        private PasswordSecurity accountPassword = new PasswordSecurity();

        // A string set up to store the selection from the member type radio buttons
        // The variable string 'membershipType' is later stored in the database and determines whether
        // the coach user can see the members information or not.
        private string membershipType;

        // A string set up to store the selection from the account type radio buttons
        // The variable string 'accountType' is later stored in the database and determines the
        // Access priviledges the new account will have.
        private string accountType;

        // Three constant strings set up to display the format for the input of the date of birth.  
        private const string dateOfBirthYearText = "YYYY";
        private const string dateOfBirthMonthText = "MM";
        private const string dateOfBirthDayText = "DD";

        // Sets the path of the stored database as a string called 'SimplyRugbyDBConnectionString'.
        private string SimplyRugbyDBConnectionString = @"Data Source=Database/SimplyRugbyDatabase.db;Version=3;";

        #endregion

        #region Constructor

        public AdminAdd()
        {
            InitializeComponent();
        }

        #endregion

        #region Radiobutton Methods

        // Method which sets the variable string 'membershipType' as "Playing Member" when the radiobutton
        // Called PlayingMemberRadioButton is checked.
        // Resets the textbox 'SRUNoTxt' to not being readonly, incase the user selects 'non-playing member'
        // By mistake, they can now reselect playing member and edit the sru number accordingly
        private void PlayingMemberRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            membershipType = "Playing Member";
            SRUNoTxt.IsReadOnly = false;
        }

        // Method which sets the variable string 'membershipType' as "Non-Playing Member" when the radiobutton
        // Called NonPlayingMemberRadioButton is checked and sets the field 'SRU No' as "N/A".
        // And makes the textbox 'SRUNoTxt' readonly so the value cannot be changed from "N/A" whilst the 
        // NonPlayingMemberRadioButton is Checked
        private void NonPlayingMemberRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            membershipType = "Non-Playing Member";
            SRUNoTxt.Text = "N/A";
            SRUNoTxt.IsReadOnly = true;
        }

        // Method which sets the variable string 'accountType' as "admin" when the radiobutton
        // Called AdminAccountRadioButton is checked.
        private void AdminAccountRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            accountType = "Admin";
        }

        // Method which sets the variable string 'accountType' as "coach" when the radiobutton
        // Called CoachAccountRadioButton is checked.
        private void CoachAccountRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            accountType = "Coach";
        }

        #endregion

        #region Add Member / Account Methods

        // Method which handles the user clicking the 'Add Record' button.
        private void AddRecordButton_Click(object sender, RoutedEventArgs e)
        {
            // Initialises a connection to the Database based on the path which is stored as the string 'SimplyRugbyDBConnectionString'.
            SQLiteConnection sqliteConnection = new SQLiteConnection(SimplyRugbyDBConnectionString);

            // An ELSE IF statment which handles if the Member Name input fails it's validation
            // Calls the MemberNameValidation() method from the Validation class 
            // Passes the value entered in the 'MemberNameTxt' textbox.
            // Enters the ELSE IF statement when the boolean value 'false' is returned
            if (inputDataValidation.MemberNameValidation(MemberNameTxt.Text) == false)
            {
                // Returns focus to the 'MemberNameTxt' text box, if validation has failed.
                MemberNameTxt.Focus();
                return;
            }

            // An ELSE IF statment which handles if the SRU Number input fails it's validation
            // Calls the SRUNoValidation() method from the Validation class 
            // Passes the value entered in the 'SRUNoTxt' textbox and the current value for membershipType
            // Set by the selected radiobutton.
            // Enters the ELSE IF statement when the boolean value 'false' is returned
            else if (inputDataValidation.SRUNoValidation(SRUNoTxt.Text) == false)
            {
                // Returns focus to the 'SRUNoTxt' text box, if validation has failed.
                SRUNoTxt.Focus();
                return;
            }

            // An ELSE IF statment which handles if the User tries to create the record with a
            // Playing Member having N/A as their SRU No
            else if (membershipType == "Playing Member" && SRUNoTxt.Text == "N/A")
            {
                MessageBox.Show("A Playing Member must have a SRU No.");
                return;

            }

            // An ELSE IF statment which handles if the SRU Number already exists on the database
            // Calls the DoesItExist() method from the Validation class 
            // Passes a string of the query with the value entered in the 'SRUNoTxt' textbox            
            // Enters the ELSE IF statement when the boolean value 'true' is returned, meaning the value in 'SRUNoTxt'
            // textbox has been found in the database.
            else if (inputDataValidation.DoesItExist(SRUNoTxt.Text, "SELECT * FROM members WHERE SRUno='" + SRUNoTxt.Text + "'") == true)
            {
                MessageBox.Show("The SRU No you have entered is already stored on the database, please try again.");
                // Returns focus to the 'SRUNoTxt' text box, if validation has failed.
                SRUNoTxt.Focus();
                return;
            }

            // An ELSE IF statment which handles if the Email Address input fails it's validation
            // Calls the EmailValidation() method from the Validation class 
            // Passes the value entered in the 'EmailAddressTxt' textbox.
            // Enters the ELSE IF statement when the boolean value 'false' is returned
            else if (inputDataValidation.EmailValidation(EmailAddressTxt.Text) == false)
            {
                // Returns focus to the 'EmailAddressTxt' text box, if validation has failed.
                EmailAddressTxt.Focus();
                return;
            }

            // An ELSE IF statment which handles if the Phone Number input fails it's validation
            // Calls the PhoneNoValidation() method from the Validation class 
            // Passes the value entered in the 'PhoneNoTxt' textbox.
            // Enters the ELSE IF statement when the boolean value 'false' is returned
            else if (inputDataValidation.PhoneNoValidation(PhoneNoTxt.Text) == false)
            {
                // Returns focus to the 'PhoneNoTxt' text box, if validation has failed.
                PhoneNoTxt.Focus();
                return;
            }

            // An ELSE IF statment which handles if the Date of Birth Year input fails it's validation
            // Calls the DateOfBirthYearValidation() method from the Validation class 
            // Passes the value entered in the 'DateOfBirthYearTxt' textbox.
            // Enters the ELSE IF statement when the boolean value 'false' is returned
            //else 
            if (inputDataValidation.DateOfBirthYearValidation(DateOfBirthYearTxt.Text) == false)
            {
                // Returns focus to the 'DateOfBirthYearTxt' text box, if validation has failed.
                DateOfBirthYearTxt.Focus();
                return;
            }

            // An ELSE IF statment which handles if the Date of Birth Month input fails it's validation
            // Calls the DateOfBirthMonthValidation() method from the Validation class 
            // Passes the value entered in the 'DateOfBirthMonthTxt' textbox.
            // Enters the ELSE IF statement when the boolean value 'false' is returned
            else if (inputDataValidation.DateOfBirthMonthValidation(DateOfBirthMonthTxt.Text) == false)
            {
                // Returns focus to the 'DateOfBirthMonthTxt' text box, if validation has failed.
                DateOfBirthMonthTxt.Focus();
                return;
            }

            // An IF statment which handles if the Date of Birth Day input fails it's validation
            // Calls the DateOfBirthDayValidation() method from the Validation class 
            // Passes the value entered in the 'DateOfBirthDayTxt' textbox and the validated values entered in the 
            // 'DateOfBirthYearTxt' and 'DateOfBirthMonthTxt' textboxes.
            // Enters the ELSE IF statement when the boolean value 'false' is returned
            else if (inputDataValidation.DateOfBirthDayValidation(DateOfBirthDayTxt.Text, DateOfBirthMonthTxt.Text, DateOfBirthYearTxt.Text) == false)
            {
                // Returns focus to the 'DateOfBirthDayTxt' text box, if validation has failed.
                DateOfBirthDayTxt.Focus();
                return;
            }

            // An ELSE IF statment which handles if the Membership Type input fails it's validation
            // Calls the MembershipTypeValidation() method from the Validation class 
            // Passes the value stored in the 'membershipType' variable string.
            // Enters the ELSE IF statement when the boolean value 'false' is returned
            else if (inputDataValidation.MembershipTypeValidation(membershipType) == false)
            {
                return;
            }

            // The program will only enter this ELSE statement when all the input information meets the validation criteria set. ELSE 
            else
            {
                // Concatenates the day string value, month string value and the year string value together with forward slashes to seperate the values.
                // Stores the concatenated value as a string called fullDateOfBirth.
                string fullDateOfBirth = string.Format("{0}/{1}/{2}", DateOfBirthYearTxt.Text, DateOfBirthMonthTxt.Text, DateOfBirthDayTxt.Text);

                // Calls the AddRecord() method from the Database class
                // Sends the database connection, validated Member Name, validated Email Address, 
                // validated Phone No, validated concatenated Date of Birth 
                // validated SRU No and validated Membership Type.
                simplyRubgyDatabaseAdminAddPage.AddRecord
                (sqliteConnection, MemberNameTxt.Text, EmailAddressTxt.Text, PhoneNoTxt.Text, fullDateOfBirth, SRUNoTxt.Text, membershipType);

                // MessageBox displays a message informing the user that the record has been added to the database, successfully.
                MessageBox.Show("A record for '" + MemberNameTxt.Text + "' has been successfully added.");

                // Sets the textboxes MemberNameTxt, EmailAddressTxt, PhoneNoTxt and SRUNoTxt to display their defualt values (an empty string).
                MemberNameTxt.Text = EmailAddressTxt.Text = PhoneNoTxt.Text = SRUNoTxt.Text = "";

                // Sets the DateOfBirthYearTxt textbox to display the default value (constant string 'dateOfBirthYearText').
                DateOfBirthYearTxt.Text = dateOfBirthYearText;

                // Sets the DateOfBirthMonthTxt textbox to display the default value (constant string 'dateOfBirthMonthText').
                DateOfBirthMonthTxt.Text = dateOfBirthMonthText;

                // Sets the DateOfBirthDayTxt textbox to display the default value (constant string 'dateOfBirthDayText').
                DateOfBirthDayTxt.Text = dateOfBirthDayText;

                // Sets both radiobuttons to default selection (none).
                PlayingMemberRadioButton.IsChecked = false;
                NonPlayingMemberRadioButton.IsChecked = false;

                // Sets the variable string 'membershipType' to null, this avoids any validation conflictions
                // With adding multiple records in the same sitting.
                membershipType = null;
            }
        }

        // Method which handles the user clicking the 'Add Account' button
        private void AddAccountButton_Click(object sender, RoutedEventArgs e)
        {
            // Initialises a connection to the Database based on the path which is stored as the string 'SimplyRugbyDBConnectionString'.
            SQLiteConnection sqliteConnection = new SQLiteConnection(SimplyRugbyDBConnectionString);

            // Set the varabile string 'username' as the value which has been entered in the 
            // 'AddAccountUsernameTxt' textbox.
            string username = AddAccountUsernameTxt.Text;

            // An IF statement that checks a value has been entered for the username.
            // Enters the IF statement if username is equal to an empty string.
            if (username == "")
            {
                // IF there has been no username entered, a MessageBox will display a message 
                // Informing the user that they must enter a username to continue.
                MessageBox.Show("You must enter a username, to create a new account");
                return;
            }

            // An ELSE IF statment which handles if 'username' value fails it's validation
            // Calls the AccountUsernameCreationValidation() method from the Validation class 
            // Passes the value of the variable string 'username'
            // Enters the IF statement when the boolean value 'false' is returned.
            else if (inputDataValidation.AccountUsernameCreationValidation(username) == false)
            {
                return;
            }

            // An ELSE IF statment which handles if 'username' value fails it's validation
            // Calls the DoesThisAccountUsernameExist() method from the Validation class 
            // Passes the value of the variable string 'username'
            // Enters the IF statement when the boolean value 'true' is returned.
            else if (inputDataValidation.DoesItExist("SELECT * FROM login WHERE Username='" + username + "'") == true)
            {
                // Displays a message informing the user that the username they have entered already exists
                // And they will have to select another if they wish to continue.
                MessageBox.Show("That username already exists.\n\nPlease try another username");
                return;
            }

            else
            {
                // Calls the method HashPassword() from the Password Security class and then the variable string
                // value for username is passed. The username value is hashed then returned and stored as a variable string
                // named 'hashedPassword'.
                string hashedPassword = accountPassword.HashThePassword(username);


                // An IF statement which the program will enter, only if the variable string is
                // Equal to "admin".
                if (accountType == "Admin")
                {
                    // Calls the AddRecord() method from the Database class
                    // Sends the database connection, validated username, hashedPassword values and 'admin' as the accountType value.
                    simplyRubgyDatabaseAdminAddPage.AddRecord(sqliteConnection, username, hashedPassword, accountType);
                }

                // An ELSE IF statement which the program will enter, only if the variable string is
                // Equal to "admin"
                else if (accountType == "Coach")
                {
                    // Calls the AddRecord() method from the Database class
                    // Sends the database connection, validated username, hashedPassword values and 'coach' as the accountType value.
                    simplyRubgyDatabaseAdminAddPage.AddRecord(sqliteConnection, username, hashedPassword, accountType);
                }

                // An ELSE statement which will handle if the user has no selected an account type.                
                else
                {
                    // MessageBox displays a message informing the user that they must select one of the radiobuttons.
                    MessageBox.Show("You must select an account type before creating a new account.");
                    return;
                }

                // Displays a message informing the user that a new account has been created and it displays the new accounts username and password as confirmation.
                MessageBox.Show("" + accountType + " Account Created\n\nUsername: '" + username + "'\nPassword: '" + username + "'\n\nThe new " + accountType + " user must reset their password ASAP.");

                // Sets the AddAccountUsernameTxt textbox as blank
                AddAccountUsernameTxt.Text = "";


                // Removes the selection of the radiobutton
                CoachAccountRadioButton.IsChecked = false;
                AdminAccountRadioButton.IsChecked = false;
            }
        }

        #endregion

        #region Methods To Display The Format For Date of Birth

        // Method which handles what happens when the textbox 'DateOfBirthDayTxt' has focus.
        private void DateOfBirthDayTxt_GotFocus(object sender, RoutedEventArgs e)
        {
            // Allows the value of the constant string 'dateOfBirthDayText' (DD) to display until the textbox 'DateOfBirthDayTxt' has focus
            // Then the the textbox 'DateOfBirthDayTxt' displays an empty string until the user enters a value
            DateOfBirthDayTxt.Text = DateOfBirthDayTxt.Text == dateOfBirthDayText ? string.Empty : DateOfBirthDayTxt.Text;
        }

        // Method which handles what happens when the textbox 'DateOfBirthDayTxt' has lost focus.
        private void DateOfBirthDayTxt_LostFocus(object sender, RoutedEventArgs e)
        {
            // Only if the textbox 'DateOfBirthDayTxt' is empty when focus is lost will the value of the constant string 'dateOfBirthDayText' (DD)
            // display in the textbox 'DateOfBirthDayTxt'
            DateOfBirthDayTxt.Text = DateOfBirthDayTxt.Text == string.Empty ? dateOfBirthDayText : DateOfBirthDayTxt.Text;
        }

        // Method which handles what happens when the textbox 'DateOfBirthMonthTxt' has focus.
        private void DateOfBirthMonthTxt_GotFocus(object sender, RoutedEventArgs e)
        {
            // Allows the value of the constant string 'dateOfBirthMonthText' (MM) to display until the textbox 'DateOfBirthMonthTxt' has focus
            // Then the the textbox 'DateOfBirthMonthTxt' displays an empty string until the user enters a value
            DateOfBirthMonthTxt.Text = DateOfBirthMonthTxt.Text == dateOfBirthMonthText ? string.Empty : DateOfBirthMonthTxt.Text;
        }

        // Method which handles what happens when the textbox 'DateOfBirthMonthTxt' has lost focus.
        private void DateOfBirthMonthTxt_LostFocus(object sender, RoutedEventArgs e)
        {
            // Only if the textbox 'DateOfBirthMonthTxt' is empty when focus is lost will the value of the constant string 'dateOfBirthMonthText' (MM)
            // display in the textbox 'DateOfBirthMonthTxt'
            DateOfBirthMonthTxt.Text = DateOfBirthMonthTxt.Text == string.Empty ? dateOfBirthMonthText : DateOfBirthMonthTxt.Text;
        }

        // Method which handles what happens when the textbox 'DateOfBirthYearTxt' has focus.
        private void DateOfBirthYearTxt_GotFocus(object sender, RoutedEventArgs e)
        {
            // Allows the value of the constant string 'dateOfBirthYearText' (YYYY) to display until the textbox 'DateOfBirthYearTxt' has focus
            // Then the the textbox 'DateOfBirthYearTxt' displays an empty string until the user enters a value
            DateOfBirthYearTxt.Text = DateOfBirthYearTxt.Text == dateOfBirthYearText ? string.Empty : DateOfBirthYearTxt.Text;
        }

        // Method which handles what happens when the textbox 'DateOfBirthYearTxt' has lost focus.
        private void DateOfBirthYearTxt_LostFocus(object sender, RoutedEventArgs e)
        {
            // Only if the textbox 'DateOfBirthYearTxt' is empty when focus is lost will the value of the constant string 'dateOfBirthYearText' (YYYY)
            // display in the textbox 'DateOfBirthYearTxt'
            DateOfBirthYearTxt.Text = DateOfBirthYearTxt.Text == string.Empty ? dateOfBirthYearText : DateOfBirthYearTxt.Text;
        }

        #endregion

        #region Navigation

        // Method which handles the 'Back' button being clicked
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Instantiate an object of the AdminStart page
            AdminStart adminStartPage = new AdminStart();

            // Navigates to the AdminStart page
            NavigationService.Navigate(adminStartPage);
        }

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
            inputDataValidation.ExitApplicationValidation();
        }

        #endregion
    }
}
