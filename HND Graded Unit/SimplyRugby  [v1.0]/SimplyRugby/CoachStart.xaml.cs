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
using System.Globalization; // Needed to create an object of the CultureInfo class to then format the date and time.


namespace SimplyRugby
{
    public partial class CoachStart : Page
    {
        #region Objects and Variable

        // Instantiates a new object of the Database Class.
        private Database simplyRugbyDatabaseCoachStartPage = new Database();

        // Instantiates a new object of the Validation Class.
        private Validation coachStartValidation = new Validation();

        // A string set up to store the current date and time.
        private string currentDateAndTime;

        // Sets the path of the stored database as a string called 'SimplyRugbyDBConnectionString'.
        private string SimplyRugbyDatabaseConnectionString = @"Data Source=Database/SimplyRugbyDatabase.db;Version=3;";

        #endregion

        #region Constructor

        public CoachStart()
        {
            InitializeComponent();

            // Calls the method PopulateCoachComboBox() when the CoachStart page loads.
            PopulateCoachComboBox();
        }

        #endregion

        #region Update Player Profile Method

        // Method which handles the user clicking the 'Update Player Profile' button
        private void UpdatePlayerProfileButton_Click(object sender, RoutedEventArgs e)
        {
            // Initialises a connection to the Database based on the path which is stored as the string 'SimplyRugbyDBConnectionString'.
            SQLiteConnection sqliteConnection = new SQLiteConnection(SimplyRugbyDatabaseConnectionString);

            // If the user hasn't selected an existing member profile from the combobox, then the value for the textbox 'MembershipNoTxt' 
            // Will be blank and the program will enter this IF statement
            if (MembershipNoTxt.Text == "")
            {
                MessageBox.Show("You must select a player member profile to update.");
                return;
            }

            // Calls the CurrentDateAndTime() method
            CurrentDateAndTime();

            // Calls the UpdateRecord() method from the Database class.
            // Passes in the Database connection, all the skill values, comments and the member no.
            simplyRugbyDatabaseCoachStartPage.UpdateRecord(sqliteConnection, StandardSkillValueTextBox.Text, SpinSkillValueTextBox.Text, PopSkillValueTextBox.Text, FrontSkillValueTextBox.Text, RearSkillValueTextBox.Text, SideSkillValueTextBox.Text, ScrabbleSkillValueTextBox.Text, DropSkillValueTextBox.Text, PuntSkillValueTextBox.Text, GrubberSkillValueTextBox.Text, GoalSkillValueTextBox.Text, PassingCommentsTxt.Text, TacklingCommentsTxt.Text, KickingCommentsTxt.Text, currentDateAndTime, MembershipNoTxt.Text);

            // Sets the textbox '' to show the value stored for the date and time at which the profile was last updated
            LastUpdatedTxt.Text = currentDateAndTime;

            // MessageBox displays a message informing the user that the profile has been updated, successfully.
            MessageBox.Show("The profile for " + PlayerComboBox.Text + " has been successfully updated.");
        }

        #endregion

        #region Populate ComboBox

        // Method which populates the combobox 'PlayerComboBox' with the current values
        private void PopulateCoachComboBox()
        {
            // Initialises a connection to the Database based on the path which is stored as the string 'SimplyRugbyDBConnectionString'.
            SQLiteConnection sqliteConnection = new SQLiteConnection(SimplyRugbyDatabaseConnectionString);

            // A string set up to store a database query.
            string query = "SELECT MemberName FROM members WHERE MembershipType='Playing Member' ORDER BY MemberName ASC";

            // try-catch incase there are any issues with retrieving values from the database
            // And then adding the values to the combobox 'PlayerComboBox'
            try
            {
                // Calls the method SetUpDataReader() sends the database connection and the string 'query'
                // Returns with the values from the query and stores these values as a SQLiteDataReader called dataReader
                SQLiteDataReader dataReader = simplyRugbyDatabaseCoachStartPage.SetUpDataReader(sqliteConnection, query);

                // A while loop, which loops while there are values in the dataReader to be read
                while (dataReader.Read())
                {
                    // Adds each value for 'MemberName' that is read, to the combobox called 'PlayerComboBox'
                    PlayerComboBox.Items.Add(dataReader["MemberName"]);
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

        #region ComboBoxes Closed Method

        // Method which deals with the combobox 'PlayerComboBox' being closed
        private void PlayerComboBox_DropDownClosed(object sender, EventArgs e)
        {
            // Initialises a connection to the Database based on the path which is stored as the string 'SimplyRugbyDBConnectionString'.
            SQLiteConnection sqliteConnection = new SQLiteConnection(SimplyRugbyDatabaseConnectionString);

            // try-catch incase there are any issues with querying the database or populating the playing member record textbox/sliders with their current values.
            try
            {
                // Calls the Open() method from the SQLiteConnection class to open a connection with the database.
                sqliteConnection.Open();

                // A string set up to store a database query.
                string query = "SELECT * FROM members WHERE MemberName= '" + PlayerComboBox.Text + "' ";

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

                    // Reads the string value for the 3rd column returned
                    // then stores the string as 'EmailAddressTxt.Text' to be displayed in the textbox 'EmailAddressTxt'
                    EmailAddressTxt.Text = dataReader.GetString(2);

                    // Reads the string value for the 4th column returned
                    // then stores the string as 'PhoneNoTxt.Text' to be displayed in the textbox 'PhoneNoTxt'
                    PhoneNoTxt.Text = dataReader.GetString(3);

                    // Reads the string value for the 5th column returned
                    // then stores the string as 'DateOfBirthTxt.Text' to be displayed in the textbox 'DateOfBirthTxt'
                    DateOfBirthTxt.Text = dataReader.GetString(4);

                    // Reads the string value for the 6th column returned
                    // then stores the string as 'SRUNoTxt.Text' to be displayed in the textbox 'SRUNoTxt'
                    SRUNoTxt.Text = dataReader.GetString(5);

                    // Reads the Integer value for the 8th column returned,
                    // Set the slider 'StandardSlider' value to the integer value
                    StandardSlider.Value = dataReader.GetInt32(7);

                    // Reads the Integer value for the 9th column returned,
                    // Set the slider 'SpinSlider' value to the integer value
                    SpinSlider.Value = dataReader.GetInt32(8);

                    // Reads the Integer value for the 10th column returned,
                    // Set the slider 'PopSlider' value to the integer value
                    PopSlider.Value = dataReader.GetInt32(9);

                    // Reads the Integer value for the 11th column returned,
                    // Set the slider 'FrontSlider' value to the integer value
                    FrontSlider.Value = dataReader.GetInt32(10);

                    // Reads the Integer value for the 12th column returned,
                    // Set the slider 'RearSlider' value to the integer value
                    RearSlider.Value = dataReader.GetInt32(11);

                    // Reads the Integer value for the 13th column returned,
                    // Set the slider 'SideSlider' value to the integer value
                    SideSlider.Value = dataReader.GetInt32(12);

                    // Reads the Integer value for the 14th column returned,
                    // Set the slider 'ScrabbleSlider' value to the integer value
                    ScrabbleSlider.Value = dataReader.GetInt32(13);

                    // Reads the Integer value for the 15th column returned,
                    // Set the slider 'DropSlider' value to the integer value
                    DropSlider.Value = dataReader.GetInt32(14);

                    // Reads the Integer value for the 16th column returned,
                    // Set the slider 'PuntSlider' value to the integer value
                    PuntSlider.Value = dataReader.GetInt32(15);

                    // Reads the Integer value for the 17th column returned,
                    // Set the slider 'GrubberSlider' value to the integer value
                    GrubberSlider.Value = dataReader.GetInt32(16);

                    // Reads the Integer value for the 18th column returned,
                    // Set the slider 'GoalSlider' value to the integer value
                    GoalSlider.Value = dataReader.GetInt32(17);

                    // Reads the string value for the 19th column returned
                    // then stores the string as 'PassingCommentsTxt.Text' to be displayed in the textbox 'PassingCommentsTxt'
                    PassingCommentsTxt.Text = dataReader.GetString(18);

                    // Reads the string value for the 20th column returned
                    // then stores the string as 'TacklingCommentsTxt.Text' to be displayed in the textbox 'TacklingCommentsTxt'
                    TacklingCommentsTxt.Text = dataReader.GetString(19);

                    // Reads the string value for the 21st column returned
                    // then stores the string as 'KickingCommentsTxt.Text' to be displayed in the textbox 'KickingCommentsTxt'
                    KickingCommentsTxt.Text = dataReader.GetString(20);

                    // Reads the string value for the 22nd column returned
                    // then stores the string as 'LastUpdatedTxt.Text' to be displayed in the textbox 'LastUpdatedTxt'
                    LastUpdatedTxt.Text = dataReader.GetString(21);
                }
            }

            // In the event of an error a MessageBox displays the specific SQLite error and does not allow the application crash.
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            // Calls the Close() method from the SQLiteConnection class to close the connection with the database.
            sqliteConnection.Close();
        }

        #endregion

        #region Date and Time Method

        // Method which gets the current time and formats it in the style (YYYY/MM/DD)
        // That is being used throughout the application
        private string CurrentDateAndTime()
        {
            // Instantiates a new object of the DateTime Class, names it 'localDate'
            // And sets it as the current Date and Time. 
            DateTime localDate = DateTime.Now;

            // Instantiates a new object of the CultureInfo Class, names it 'culture'
            // Sets the format of 'culture' to 'ja-JP'.
            CultureInfo culture = new CultureInfo("ja-JP");

            // Coverts the value stored as the object localDate, using the culture
            // Object to format the value. Returns the value as a string called 'currentDateAndTime'
            return currentDateAndTime = localDate.ToString(culture);
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
            coachStartValidation.ExitApplicationValidation();
        }

        #endregion
    }
}

