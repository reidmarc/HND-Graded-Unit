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

using System.Data.SQLite; // Needed for SQLite functionality


namespace SimplyRugby
{
    public partial class AdminDelete : Page
    {
        #region Objects and Variable

        // Instantiates a new object of the Database Class.
        private Database simplyRugbyDatabaseAdminDeletePage = new Database();

        // Instantiates a new object of the Validation Class.
        private Validation validationCheck = new Validation();

        // Sets the path of the stored database as a string called 'SimplyRugbyDBConnectionString'.
        private string SimplyRugbyDatabaseConnectionString = @"Data Source=Database/SimplyRugbyDatabase.db;Version=3;";

        // Sets up an integer to store the LoginId for the account linked to the username selected from the combobox
        private int accountLoginID;

        #endregion

        #region Constructor

        public AdminDelete()
        {
            InitializeComponent();

            // Calls the method PopulateAdminDeleteMemberNameComboBox() when the AdminDelete page loads.
            PopulateAdminDeleteMemberNameComboBox();

            // Calls the method PopulateAdminDeleteMembershipNoComboBox() when the AdminDelete page loads.
            PopulateAdminDeleteMembershipNoComboBox();

            // Calls the method PopulateAccountComboBox() when the AdminDelete page loads.
            PopulateAccountComboBox();
        }

        #endregion

        #region Delete Record / Account Methods

        // Method which handles what happens when the user clicks the delete record button
        private void DeleteRecordButton_Click(object sender, RoutedEventArgs e)
        {
            // Initialises a connection to the Database based on the path which is stored as the string 'SimplyRugbyDBConnectionString'.
            SQLiteConnection sqliteConnection = new SQLiteConnection(SimplyRugbyDatabaseConnectionString);

            // An IF statement that checks the user has selected a member record to delete 
            // Enters the IF statement if the textbox 'MembershipNoTxt' is not displaying a value            
            if (MembershipNoTxt.Text == "")
            {
                // IF there has been no member record selected, a MessageBox will display a message
                // Informing the user that they must select a member record to continue.
                MessageBox.Show("You must select a member record to delete.");
                return;
            }

            // An IF statment which handles if deleting the record validation fails
            // Calls the DeleteRecordValidation() method from the Validation class            
            // Enters the IF statement when the boolean value 'false' is returned.
            if (validationCheck.DeleteRecordValidation(MemberNameTxt.Text) == false)
            {
                return;
            }

            // Calls the DeleteRecord() method from the Database class
            // Sends the database connection, the value that is being displayed in the textbox 'MembershipNoTxt' and 'record'.
            simplyRugbyDatabaseAdminDeletePage.DeleteRecord(sqliteConnection, MembershipNoTxt.Text, "record");

            // Displays a message informing the user that the record for the selected member has been deleted.
            MessageBox.Show("The member record for '" + MemberNameTxt.Text + "' has been deleted.");

            // Sets the textboxes  MembershipNoTxt, MemberNameTxt, EmailAddressTxt, PhoneNoTxt, DateOfBirthDayTxt, DateOfBirthMonthTxt,
            // DateOfBirthYearTxt, SRUNoTxt and MembershipTypeTxt to display their default values (an empty string).
            MembershipNoTxt.Text = MemberNameTxt.Text = EmailAddressTxt.Text =
            PhoneNoTxt.Text = DateOfBirthDayTxt.Text = DateOfBirthMonthTxt.Text =
            DateOfBirthYearTxt.Text = SRUNoTxt.Text = MembershipTypeTxt.Text = "";

            // Calls the Clear() method from the ItemCollection class to remove all the enteries in the comboboxes
            MemberNameComboBox.Items.Clear();
            MembershipNoComboBox.Items.Clear();

            // Calls the PopulateAdminDeleteMemberNameComboBox() and PopulateAdminDeleteMembershipNoComboBox() methods
            // to repopulate the comboboxes 'MemberNameComboBox' and 'MembershipNoComboBox' with their current values.
            PopulateAdminDeleteMemberNameComboBox();
            PopulateAdminDeleteMembershipNoComboBox();
        }

        // Method which handles what happens when the user clicks the delete account button
        private void DeleteAccountButton_Click(object sender, RoutedEventArgs e)
        {
            // Initialises a connection to the Database based on the path which is stored as the string 'SimplyRugbyDBConnectionString'.
            SQLiteConnection sqliteConnection = new SQLiteConnection(SimplyRugbyDatabaseConnectionString);

            // Calls the Open() method from the SQLiteConnection class to open a connection with the database.
            sqliteConnection.Open();

            // A string set up to store a database query.
            string query = "SELECT * FROM login WHERE Username= '" + AccountComboBox.Text + "' ";

            // Instantiates a new object of the SQLiteCommand class, using the string 'query' and the database connection
            SQLiteCommand createCommand = new SQLiteCommand(query, sqliteConnection);

            // Calls the method ExecuteReader() from the SQLiteCommand class, to create the SQLiteDataReader object  
            SQLiteDataReader dataReader = createCommand.ExecuteReader();

            // A while loop, which loops while there are values in the dataReader to be read
            while (dataReader.Read())
            {
                // Reads the value stores in the 4th column and then sets the string 'loginAccountType'
                // To equal the value read.
                accountLoginID = dataReader.GetInt32(0);
            }

            // Calls the Close() method from the SQLiteConnection class to close the connection with the database
            sqliteConnection.Close();

            // An IF statement that checks the user has selected an account record to delete 
            // Enters the IF statement if the combobox 'AccountComboBox' is not displaying a value 
            if (AccountComboBox.Text == "")
            {
                // IF there has been no account record selected, a MessageBox will display a message
                // Informing the user that they must select an account record to continue.
                MessageBox.Show("You must select an account to delete.");
                return;
            }

            // The program will only enter this IF statement if the admin user is attempting to delete the default
            // Admin account 'Admin01'
            else if (accountLoginID == 1)
            {
                MessageBox.Show("You cannot delete the '" + AccountComboBox.Text + "' account.");
                return;
            }

            // An ELSE IF statment which handles if deleting the record validation fails
            // Calls the DeleteAccountValidation() method from the Validation class            
            // Enters the IF statement when the boolean value 'false' is returned.
            else if (validationCheck.DeleteAccountValidation(AccountComboBox.Text) == false)
            {
                return;
            }

            // The program will only enter this ELSE statement as long as the account the user is trying to delete is not the default
            // Admin account
            else
            {
                // Calls the DeleteRecord() method from the Database class
                // Sends the database connection, the value that is being displayed in the combobox 'AccountComboBox' and 'account'.
                simplyRugbyDatabaseAdminDeletePage.DeleteRecord(sqliteConnection, AccountComboBox.Text, "account");
            }

            // Displays a message informing the user that the account for the selected username has been deleted.
            MessageBox.Show("Account of " + AccountComboBox.Text + " has been successfully deleted.");

            // Calls the Clear() method from the ItemCollection class to remove all the enteries in the combobox
            AccountComboBox.Items.Clear();

            // Calls the PopulateAccountComboBox() method to repopulate the combobox 'AccountComboBox' with the current values.
            PopulateAccountComboBox();

        }

        #endregion

        #region Populate ComboBoxes

        // Method which populates the combobox 'MemberNameComboBox' with the current values
        private void PopulateAdminDeleteMemberNameComboBox()
        {
            // Initialises a connection to the Database based on the path which is stored as the string 'SimplyRugbyDBConnectionString'.
            SQLiteConnection sqliteConnection = new SQLiteConnection(SimplyRugbyDatabaseConnectionString);

            // A string set up to store a database query.
            string query = "SELECT * FROM members ORDER BY MemberName ASC";

            // try-catch incase there are any issues with retrieving values from the database
            // And then adding the values to the combobox 'MemberNameComboBox'
            try
            {
                // Calls the method SetUpDataReader() sends the database connection and the string 'query'
                // Returns with the values from the query and stores these values as a SQLiteDataReader called dataReader
                SQLiteDataReader dataReader = simplyRugbyDatabaseAdminDeletePage.SetUpDataReader(sqliteConnection, query);

                // A while loop, which loops while there are values in the dataReader to be read
                while (dataReader.Read())
                {
                    // Adds each value for 'MemberName' that is read, to the combobox called 'MemberNameComboBox'
                    MemberNameComboBox.Items.Add(dataReader["MemberName"]);
                }

                // Calls the Close method from the SQLiteDataReader class to finish reading.
                dataReader.Close();
            }

            // In the event of an error a MessageBox displays the specific SQLite error and does not allow the application crash.
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            // Calls the Close() method from the SQLiteConnection class to close the connection with the database
            sqliteConnection.Close();
        }

        // Method which populates the combobox 'MembershipNoComboBox' with the current values
        private void PopulateAdminDeleteMembershipNoComboBox()
        {
            // Initialises a connection to the Database based on the path which is stored as the string 'SimplyRugbyDBConnectionString'.
            SQLiteConnection sqliteConnection = new SQLiteConnection(SimplyRugbyDatabaseConnectionString);

            // A string set up to store a database query.
            string query = "SELECT * FROM members ORDER BY MembershipNo ASC";

            // try-catch incase there are any issues with retrieving values from the database
            // And then adding the values to the combobox 'MembershipNoComboBox'
            try
            {
                // Calls the method SetUpDataReader() sends the database connection and the string 'query'
                // Returns with the values from the query and stores these values as a SQLiteDataReader called dataReader
                SQLiteDataReader dataReader = simplyRugbyDatabaseAdminDeletePage.SetUpDataReader(sqliteConnection, query);

                // A while loop, which loops while there are values in the dataReader to be read
                while (dataReader.Read())
                {
                    // Reads the Integer value for the 1st column returned, converts the int to a string
                    // then stores the string as 'memNo'
                    string memNo = dataReader.GetInt32(0).ToString();

                    // Adds each value for 'memNo' that is read, to the combobox called 'MembershipNoComboBox'
                    MembershipNoComboBox.Items.Add(memNo);
                }

                // Calls the Close method from the SQLiteDataReader class to finish reading.
                dataReader.Close();
            }

            // In the event of an error a MessageBox displays the specific SQLite error and does not allow the application crash.
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            // Calls the Close() method from the SQLiteConnection class to close the connection with the database
            sqliteConnection.Close();
        }

        // Method which populates the combobox 'AccountComboBox' with the current values
        private void PopulateAccountComboBox()
        {
            // Initialises a connection to the Database based on the path which is stored as the string 'SimplyRugbyDBConnectionString'.
            SQLiteConnection sqliteConnection = new SQLiteConnection(SimplyRugbyDatabaseConnectionString);

            // A string set up to store a database query.
            string query = "SELECT * FROM login ORDER BY LoginID ASC";

            // try-catch incase there are any issues with retrieving values from the database
            // And then adding the values to the combobox 'AccountComboBox'
            try
            {
                // Calls the method SetUpDataReader() sends the database connection and the string 'query'
                // Returns with the values from the query and stores these values as a SQLiteDataReader called dataReader
                SQLiteDataReader dataReader = simplyRugbyDatabaseAdminDeletePage.SetUpDataReader(sqliteConnection, query);

                // A while loop, which loops while there are values in the dataReader to be read
                while (dataReader.Read())
                {
                    // Adds each value for 'Username' that is read, to the combobox called 'AccountComboBox'
                    AccountComboBox.Items.Add(dataReader["Username"]);
                }

                // Calls the Close method from the SQLiteDataReader class to finish reading.
                dataReader.Close();
            }

            // In the event of an error a MessageBox displays the specific SQLite error and does not allow the application crash.
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            // Calls the Close() method from the SQLiteConnection class to close the connection with the database
            sqliteConnection.Close();
        }

        #endregion

        #region ComboBoxes Closed Methods

        // Method which deals with the combobox 'MemberNameComboBox' being closed
        private void MemberNameComboBox_DropDownClosed(object sender, EventArgs e)
        {
            // Initialises a connection to the Database based on the path which is stored as the string 'SimplyRugbyDBConnectionString'.
            SQLiteConnection sqliteConnection = new SQLiteConnection(SimplyRugbyDatabaseConnectionString);

            // try-catch incase there are any issues with querying the database or populating the member record textbox with their current values.
            try
            {
                // Calls the Open() method from the SQLiteConnection class to open a connection with the database.
                sqliteConnection.Open();

                // A string set up to store a database query.
                string query = "SELECT * FROM members WHERE MemberName= '" + MemberNameComboBox.Text + "' ";

                // Calls the method PopulateDeleteTextBoxesFromComboBoxSelection() sends the database connection and the string 'query'.
                PopulateDeleteTextBoxesFromComboBoxSelection(sqliteConnection, query);
            }

            // In the event of an error a MessageBox displays the specific SQLite error and does not allow the application crash.
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            // Calls the Close() method from the SQLiteConnection class to close the connection with the database.
            sqliteConnection.Close();

            // Calls the Clear() method from the ItemCollection class to remove all the enteries in the combobox 'MembershipNoComboBox'.
            MembershipNoComboBox.Items.Clear();

            // Calls the PopulateAdminDeleteMembershipNoComboBox() method to repopulate the combobox 'MembershipNoComboBox' with the current values.
            PopulateAdminDeleteMembershipNoComboBox();
        }

        // Method which deals with the combobox 'MembershipNoComboBox' being closed
        private void MembershipNoComboBox_DropDownClosed(object sender, EventArgs e)
        {
            // Initialises a connection to the Database based on the path which is stored as the string 'SimplyRugbyDBConnectionString'.
            SQLiteConnection sqliteConnection = new SQLiteConnection(SimplyRugbyDatabaseConnectionString);

            // try-catch incase there are any issues with querying the database or populating the member record textbox with their current values.
            try
            {
                // Calls the Open() method from the SQLiteConnection class to open a connection with the database.
                sqliteConnection.Open();

                // A string set up to store a database query.
                string query = "SELECT * FROM members WHERE MembershipNo= '" + MembershipNoComboBox.Text + "' ";

                // Calls the method PopulateDeleteTextBoxesFromComboBoxSelection() sends the database connection and the string 'query'.
                PopulateDeleteTextBoxesFromComboBoxSelection(sqliteConnection, query);
            }

            // In the event of an error a MessageBox displays the specific SQLite error and does not allow the application crash.
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            // Calls the Close() method from the SQLiteConnection class to close the connection with the database.
            sqliteConnection.Close();

            // Calls the Clear() method from the ItemCollection class to remove all the enteries in the combobox 'MemberNameComboBox'.
            MemberNameComboBox.Items.Clear();

            // Calls the PopulateAdminDeleteMemberNameComboBox() method to repopulate the combobox 'MemberNameComboBox' with the current values.
            PopulateAdminDeleteMemberNameComboBox();
        }

        #endregion

        #region Populate Delete TextBoxes With Current Stored Values

        // Method which populates the member records textboxes after a selection has been made from a combobox
        private void PopulateDeleteTextBoxesFromComboBoxSelection(SQLiteConnection sqliteConnection, string query)
        {
            // try-catch incase there are any issues with retrieving values from the database when a record is selected from
            // A combobox and then displaying the values in their associated textboxes
            try
            {
                // Instantiates a new object of the SQLiteCommand Class, passes the values of the string query and SQLiteConnection sqliteConnection.
                SQLiteCommand createCommand = new SQLiteCommand(query, sqliteConnection);

                // Calls the method ExecuteReader() from the SQLiteCommand class 
                // Returns with the values from the query and stores these values as a SQLiteDataReader called dataReader
                SQLiteDataReader dataReader = createCommand.ExecuteReader();

                // A while loop, which loops while there are values in the dataReader to be read
                while (dataReader.Read())
                {
                    // Reads the Integer value for the 1st column returned, converts the int to a string
                    // then stores the string as 'MembershipNoTxt.Text' to be displayed in the textbox 'MembershipNoTxt'
                    MembershipNoTxt.Text = dataReader.GetInt32(0).ToString();

                    // Reads the string value for the 2nd column returned
                    // then stores the string as 'MemberNameTxt.Text' to be displayed in the textbox 'MemberNameTxt'
                    MemberNameTxt.Text = dataReader.GetString(1);

                    // Reads the string value for the 3rd column returned
                    // then stores the string as 'EmailAddressTxt.Text' to be displayed in the textbox 'EmailAddressTxt'
                    EmailAddressTxt.Text = dataReader.GetString(2);

                    // Reads the string value for the 4th column returned
                    // then stores the string as 'PhoneNoTxt.Text' to be displayed in the textbox 'PhoneNoTxt'
                    PhoneNoTxt.Text = dataReader.GetString(3);

                    // Reads the string value for the 6th column returned
                    // then stores the string as 'SRUNoTxt.Text' to be displayed in the textbox 'SRUNoTxt'
                    SRUNoTxt.Text = dataReader.GetString(5);

                    // Reads the string value for the 7th column returned
                    // then stores the string as 'MembershipTypeTxt.Text' to be displayed in the textbox 'MembershipTypeTxt'
                    // As the user has no interaction with the information displayed, I chose to display the membership type
                    // As a string from a textbox instead of with radiobutton.
                    MembershipTypeTxt.Text = dataReader.GetString(6);

                    // Reads the string value for the 5th column returned
                    string dateOfBirth = dataReader.GetString(4);

                    // Removes the first 8 characters from the string 'dateOfBirth' then stores the value as 
                    // DateOfBirthDayTxt.Text to be displayed in the textbox 'DateOfBirthDayTxt'
                    DateOfBirthDayTxt.Text = dateOfBirth.Remove(0, 8);

                    // Removes the first 5 characters from the string 'dateOfBirth' then stores the value as 
                    // A string called 'dateOfBirthMonth'                    
                    string dateOfBirthMonth = dateOfBirth.Remove(0, 5);

                    // Removes the last 3 characters from the string 'dateOfBirthMonth' then stores the value as 
                    // DateOfBirthMonthTxt.Text to be displayed in the textbox 'DateOfBirthMonthTxt'
                    DateOfBirthMonthTxt.Text = dateOfBirthMonth.Remove(2, 3);

                    // Removes the last 6 characters from the string 'dateOfBirth' then stores the value as 
                    // DateOfBirthYearTxt.Text to be displayed in the textbox 'DateOfBirthYearTxt'
                    DateOfBirthYearTxt.Text = dateOfBirth.Remove(4, 6);
                }
            }

            // In the event of an error a MessageBox displays the specific SQLite error and does not allow the application crash.
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
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
            validationCheck.ExitApplicationValidation();
        }

        #endregion
    }
}

