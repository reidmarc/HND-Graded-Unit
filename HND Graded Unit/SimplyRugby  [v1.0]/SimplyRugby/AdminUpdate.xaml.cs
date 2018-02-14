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
    public partial class AdminUpdate : Page
    {
        #region Objects and Variable

        // Instantiates a new object of the Database Class.
        private Database simplyRugbyDatabaseAdminUpdatePage = new Database();

        // Instantiates a new object of the Validation Class.
        private Validation updateDataValidation = new Validation();

        // A string set up to store the selection from the member type radio buttons
        // The variable string 'membershipType' is later stored in the database and determines whether
        // the coach user can see the members information or not.
        private string membershipType;

        // Sets the path of the stored database as a string called 'SimplyRugbyDBConnectionString'.
        private string SimplyRugbyDatabaseConnectionString = @"Data Source=Database/SimplyRugbyDatabase.db;Version=3;";

        #endregion

        #region Constructor

        public AdminUpdate()
        {
            InitializeComponent();

            // Calls the method PopulateAdminUpdateMembershipNoComboBox() when the AdminUpdate page loads.
            PopulateAdminUpdateMembershipNoComboBox();

            // Calls the method PopulateAdminUpdateMemberNameComboBox() when the AdminUpdate page loads.
            PopulateAdminUpdateMemberNameComboBox();
        }

        #endregion

        #region Checked Radio Button

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

        #endregion

        #region Update Record Method

        // Method which handles the user clicking the 'Update Record' button
        private void UpdateRecordButton_Click(object sender, RoutedEventArgs e)
        {
            // Initialises a connection to the Database based on the path which is stored as the string 'SimplyRugbyDBConnectionString'.
            SQLiteConnection sqliteConnection = new SQLiteConnection(SimplyRugbyDatabaseConnectionString);

            // If the user hasn't selected an existing member record from the combobox, then the value for the textbox 'MembershipNoTxt' 
            // Will be blank and the program will enter this IF statement
            if (MembershipNoTxt.Text == "")
            {
                MessageBox.Show("You must select a member record to update.");
                return;
            }

            // An ELSE IF statment which handles if the Member Name input fails it's validation
            // Calls the MemberNameValidation() method from the Validation class 
            // Passes the value entered in the 'MemberNameTxt' textbox.
            // Enters the IF statement when the boolean value 'false' is returned
            else if (updateDataValidation.MemberNameValidation(MemberNameTxt.Text) == false)
            {
                // Returns focus to the 'MemberNameTxt' text box, if validation has failed.
                MemberNameTxt.Focus();
                return;
            }

            // An ELSE IF statment which handles if the User tries to update the record with a
            // Playing Member having N/A as their SRU No
            else if (membershipType == "Playing Member" && SRUNoTxt.Text == "N/A")
            {
                MessageBox.Show("A Playing Member must have a SRU No.");
                return;

            }

            // An ELSE IF statment which handles if the SRU Number input fails it's validation
            // Calls the SRUNoValidation() method from the Validation class 
            // Passes the value entered in the 'SRUNoTxt' textbox and the current value for membershipType
            // Set by the selected radiobutton.
            // Enters the IF statement when the boolean value 'false' is returned   
            else if (updateDataValidation.SRUNoValidation(SRUNoTxt.Text) == false)
            {
                // Returns focus to the 'SRUNoTxt' text box, if validation has failed.
                SRUNoTxt.Focus();
                return;
            }

            // An ELSE IF statment which handles if the Email Address input fails it's validation
            // Calls the EmailValidation() method from the Validation class 
            // Passes the value entered in the 'EmailAddressTxt' textbox.
            // Enters the IF statement when the boolean value 'false' is returned
            else if (updateDataValidation.EmailValidation(EmailAddressTxt.Text) == false)
            {
                // Returns focus to the 'EmailAddressTxt' text box, if validation has failed.
                EmailAddressTxt.Focus();
                return;
            }

            // An ELSE IF statment which handles if the Phone Number input fails it's validation
            // Calls the PhoneNoValidation() method from the Validation class 
            // Passes the value entered in the 'PhoneNoTxt' textbox.
            // Enters the IF statement when the boolean value 'false' is returned
            else if (updateDataValidation.PhoneNoValidation(PhoneNoTxt.Text) == false)
            {
                // Returns focus to the 'PhoneNoTxt' text box, if validation has failed.
                PhoneNoTxt.Focus();
                return;
            }

            // An ELSE IF statment which handles if the Date of Birth Year input fails it's validation
            // Calls the DateOfBirthYearValidation() method from the Validation class 
            // Passes the value entered in the 'DateOfBirthYearTxt' textbox.
            // Enters the IF statement when the boolean value 'false' is returned
            else if (updateDataValidation.DateOfBirthYearValidation(DateOfBirthYearTxt.Text) == false)
            {
                // Returns focus to the 'DateOfBirthYearTxt' text box, if validation has failed.
                DateOfBirthYearTxt.Focus();
                return;
            }

            // An ELSE IF statment which handles if the Date of Birth Month input fails it's validation
            // Calls the DateOfBirthMonthValidation() method from the Validation class 
            // Passes the value entered in the 'DateOfBirthMonthTxt' textbox.
            // Enters the IF statement when the boolean value 'false' is returned
            else if (updateDataValidation.DateOfBirthMonthValidation(DateOfBirthMonthTxt.Text) == false)
            {
                // Returns focus to the 'DateOfBirthMonthTxt' text box, if validation has failed.
                DateOfBirthMonthTxt.Focus();
                return;
            }

            // An ELSE IF statment which handles if the Date of Birth Day input fails it's validation
            // Calls the DateOfBirthDayValidation() method from the Validation class 
            // Passes the value entered in the 'DateOfBirthDayTxt' textbox and the validated values entered in the 
            // 'DateOfBirthYearTxt' and 'DateOfBirthMonthTxt' textboxes.
            // Enters the IF statement when the boolean value 'false' is returned
            else if (updateDataValidation.DateOfBirthDayValidation(DateOfBirthDayTxt.Text, DateOfBirthMonthTxt.Text, DateOfBirthYearTxt.Text) == false)
            {
                // Returns focus to the 'DateOfBirthDayTxt' text box, if validation has failed.
                DateOfBirthDayTxt.Focus();
                return;
            }

            // An ELSE IF statment which handles if the Membership Type input fails it's validation
            // Calls the MembershipTypeValidation() method from the Validation class 
            // Passes the value stored in the 'membershipType' variable string.
            // Enters the IF statement when the boolean value 'false' is returned
            else if (updateDataValidation.MembershipTypeValidation(membershipType) == false)
            {
                return;
            }

            // Concatenates the day value, month value and the year value together with forward slashes to seperate the values.
            // Stores the concatenated value as a string called fullDateOfBirth
            string updatedFullDateOfBirth = string.Format("{0}/{1}/{2}", DateOfBirthYearTxt.Text, DateOfBirthMonthTxt.Text, DateOfBirthDayTxt.Text);

            // Calls the UpdateRecord() method from the Database class
            // Sends the database connection, validated Member Name, validated Email Address, 
            // validated Phone No, validated concatenated Date of Birth 
            // validated SRU No and validated Membership Type.
            simplyRugbyDatabaseAdminUpdatePage.UpdateRecord
            (sqliteConnection, MembershipNoTxt.Text, MemberNameTxt.Text, EmailAddressTxt.Text, PhoneNoTxt.Text, updatedFullDateOfBirth, SRUNoTxt.Text, membershipType);

            // MessageBox displays a message informing the user that the record has been added to the database, successfully.
            MessageBox.Show("The record for '" + MemberNameTxt.Text + "' has been successfully updated.");

            // Sets the textboxes MembershipNoTxt, MemberNameTxt, EmailAddressTxt, PhoneNoTxt, DateOfBirthMonthTxt, DateOfBirthYearTxt, DateOfBirthDayTxt and SRUNoTxt to display their default values (an empty string).
            MembershipNoTxt.Text = MemberNameTxt.Text = EmailAddressTxt.Text = PhoneNoTxt.Text = DateOfBirthDayTxt.Text = DateOfBirthMonthTxt.Text = DateOfBirthYearTxt.Text = SRUNoTxt.Text = "";

            // Sets both radiobuttons to default selection (none).
            PlayingMemberRadioButton.IsChecked = false;
            NonPlayingMemberRadioButton.IsChecked = false;

            // Calls the Clear() method from the ItemCollection class to remove all the enteries in the comboboxes
            MemberNameComboBox.Items.Clear();
            MembershipNoComboBox.Items.Clear();

            // Calls the PopulateAdminUpdateMemberNameComboBox() and PopulateAdminUpdateMembershipNoComboBox() methods
            // to repopulate the comboboxes 'MemberNameComboBox' and 'MembershipNoComboBox' with their current values.
            PopulateAdminUpdateMemberNameComboBox();
            PopulateAdminUpdateMembershipNoComboBox();
        }

        #endregion

        #region Populate ComboBoxes

        // Method which populates the combobox 'MemberNameComboBox' with the current values
        private void PopulateAdminUpdateMemberNameComboBox()
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
                SQLiteDataReader dataReader = simplyRugbyDatabaseAdminUpdatePage.SetUpDataReader(sqliteConnection, query);

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
        private void PopulateAdminUpdateMembershipNoComboBox()
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
                SQLiteDataReader dataReader = simplyRugbyDatabaseAdminUpdatePage.SetUpDataReader(sqliteConnection, query);

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

                // Calls the method PopulateUpdateTextBoxesFromComboBoxSelection() sends the database connection and the string 'query'.
                PopulateUpdateTextBoxesFromComboBoxSelection(sqliteConnection, query);
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
            PopulateAdminUpdateMembershipNoComboBox();
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

                // Calls the method PopulateUpdateTextBoxesFromComboBoxSelection() sends the database connection and the string 'query'.
                PopulateUpdateTextBoxesFromComboBoxSelection(sqliteConnection, query);
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
            PopulateAdminUpdateMemberNameComboBox();
        }

        #endregion

        #region Populate Update TextBoxes With Current Stored Values

        private void PopulateUpdateTextBoxesFromComboBoxSelection(SQLiteConnection sqliteConnection, string sqliteMemberComboBoxQuery)
        {
            // Instantiates a new object of the SQLiteCommand Class, passes the values of the string query and SQLiteConnection sqliteConnection.
            SQLiteCommand createCommand = new SQLiteCommand(sqliteMemberComboBoxQuery, sqliteConnection);

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

                // Reads the string value for the 7th column and if it is equal to "Playing Member"
                // The program will enter the IF statement and check the radiobutton 'PlayingMemberRadioButton'
                if (dataReader.GetString(6) == "Playing Member")
                {
                    PlayingMemberRadioButton.IsChecked = true;
                }

                // Reads the string value for the 7th column and if it is equal to "Non-Playing Member"
                // The program will enter the ELSE IF statement and check the radiobutton 'NonPlayingMemberRadioButton'
                else if (dataReader.GetString(6) == "Non-Playing Member")
                {
                    NonPlayingMemberRadioButton.IsChecked = true;
                }

                // In the unlikely event that there is no value stored for the 7th column, both radiobuttons
                // Will be set to unchecked.
                else
                {
                    PlayingMemberRadioButton.IsChecked = false;
                    NonPlayingMemberRadioButton.IsChecked = false;
                }
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
            updateDataValidation.ExitApplicationValidation();
        }

        #endregion

    }
}
