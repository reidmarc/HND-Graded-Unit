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
using System.Data; // Needed to create an object of the DataTable class

namespace SimplyRugby
{
    public partial class AdminStart : Page
    {
        #region Objects and Variable

        // Instantiates a new object of the Database Class.
        private Database simplyRugbyDatabaseAdminStartPage = new Database();

        // Instantiates a new object of the Validation Class.
        private Validation navigationValidation = new Validation();

        // Sets the path of the stored database as a string called 'SimplyRugbyDBConnectionString'.
        private string SimplyRugbyDatabaseConnectionString = @"Data Source=Database/SimplyRugbyDatabase.db;Version=3;";

        #endregion

        #region Constructor

        public AdminStart()
        {
            InitializeComponent();

            // Calls the method UpdateDataGrid() when the AdminStart page loads.
            UpdateDataGrid();
        }

        #endregion

        #region Update Database View

        private void UpdateDataGrid()
        {
            // Initialises a connection to the Database based on the path which is stored as the string 'SimplyRugbyDBConnectionString'.
            SQLiteConnection sqliteConnection = new SQLiteConnection(SimplyRugbyDatabaseConnectionString);

            // try-catch incase there are any issues with querying the database then loading the information
            // Into a datagrid from the database
            try
            {   // A string set up to store a database query.
                string query = "SELECT MembershipNo AS 'Membership No', MemberName AS 'Member Name', EmailAddress AS 'Email Address', PhoneNo 'Phone Number', DateOfBirth AS 'Date of Birth', SRUNo AS 'SRU Number', MembershipType AS 'Membership Type' FROM members";

                // Calls the UpdateDatabaseView() method from the Database class and passes the database connection and query
                // returns a SQLiteDataAdapter value and stores it as 'dataAdapter'
                SQLiteDataAdapter dataAdapter = simplyRugbyDatabaseAdminStartPage.UpdateDatabaseView(sqliteConnection, query);

                // Instantiates a new object of the DataTable Class and passes the name of the table to be viewed.
                DataTable dataTable = new DataTable("members");

                // Calls the Fill() method from the SQLiteDataAdapter class and passes the object of DataTable to it.
                dataAdapter.Fill(dataTable);

                // Sets the MembersDatabaseDataGrid.ItemsSource property to the dataTable's default view
                MembersDatabaseDataGrid.ItemsSource = dataTable.DefaultView;

                // Calls the Update() method from the SQLiteDataAdapter class and passes the object of DataTable to it.
                dataAdapter.Update(dataTable);
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

        #region WPF Control Methods        

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // Initialises a connection to the Database based on the path which is stored as the string 'SimplyRugbyDBConnectionString'.
            SQLiteConnection sqliteConnection = new SQLiteConnection(SimplyRugbyDatabaseConnectionString);

            // A string set up to store a database query
            // This will change depending on which radiobutton the user selects.
            string query;

            // try-catch incase there are any issues with querying the database then loading the information
            // Into a datagrid from the database
            try
            {

                // If the MemberNameRadioButton is selected then the program will enter this IF statement
                // And set the string 'query' as shown.
                if (MemberNameRadioButton.IsChecked == true)
                {
                    query = "SELECT MembershipNo AS 'Membership No', MemberName AS 'Member Name', EmailAddress AS 'Email Address', PhoneNo 'Phone Number', DateOfBirth AS 'Date of Birth', SRUNo AS 'SRU Number', MembershipType AS 'Membership Type' FROM members WHERE MemberName LIKE '%" + SearchedFieldTxt.Text + "%'";
                }

                // Else if the DateOfBirthRadioButton is selected then the program will enter this IF statement
                // And set the string 'query' as shown.
                else if (DateOfBirthRadioButton.IsChecked == true)
                {
                    query = "SELECT MembershipNo AS 'Membership No', MemberName AS 'Member Name', EmailAddress AS 'Email Address', PhoneNo 'Phone Number', DateOfBirth AS 'Date of Birth', SRUNo AS 'SRU Number', MembershipType AS 'Membership Type' FROM members WHERE DateOfBirth LIKE '%" + SearchedFieldTxt.Text + "%'";
                }

                // Else if the SRUNoRadioButton is selected then the program will enter this IF statement
                // And set the string 'query' as shown.
                else if (SRUNoRadioButton.IsChecked == true)
                {
                    query = "SELECT MembershipNo AS 'Membership No', MemberName AS 'Member Name', EmailAddress AS 'Email Address', PhoneNo 'Phone Number', DateOfBirth AS 'Date of Birth', SRUNo AS 'SRU Number', MembershipType AS 'Membership Type' FROM members WHERE SRUNo LIKE '%" + SearchedFieldTxt.Text + "%'";
                }

                // Else if the user selects none of the radiobuttons, the query is set to default and a messagebox will display a message
                // Informing the user that they must select a radiobox to use the search function.
                else
                {
                    query = "SELECT MembershipNo AS 'Membership No', MemberName AS 'Member Name', EmailAddress AS 'Email Address', PhoneNo 'Phone Number', DateOfBirth AS 'Date of Birth', SRUNo AS 'SRU Number', MembershipType AS 'Membership Type' FROM members";
                    MessageBox.Show("You must select a radiobox to make a successful search");
                }

                // Calls the UpdateDatabaseView() method from the Database class and passes the database connection and query
                // returns a SQLiteDataAdapter value and stores it as 'dataAdapter'
                SQLiteDataAdapter dataAdapter = simplyRugbyDatabaseAdminStartPage.UpdateDatabaseView(sqliteConnection, query);

                // Instantiates a new object of the DataTable Class and passes the name of the table to be viewed.
                DataTable dataTable = new DataTable("members");

                // Calls the Fill() method from the SQLiteDataAdapter class and passes the object of DataTable to it.
                dataAdapter.Fill(dataTable);

                // Sets the MembersDatabaseDataGrid.ItemsSource property to the dataTable's default view
                MembersDatabaseDataGrid.ItemsSource = dataTable.DefaultView;

                // Calls the Update() method from the SQLiteDataAdapter class and passes the object of DataTable to it.
                dataAdapter.Update(dataTable);
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

        // Method which handles the 'Clear' button being clicked.
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            // Returns the 3 radiobuttons back to their default state (Unchecked).
            MemberNameRadioButton.IsChecked = DateOfBirthRadioButton.IsChecked = SRUNoRadioButton.IsChecked = false;

            // Returns the textbox 'SearchedFieldTxt' back its default state (blank).
            SearchedFieldTxt.Text = "";

            // Calls the method UpdateDataGrid() to display the default view of the datagrid.
            UpdateDataGrid();
        }

        #endregion

        #region Page Navigation

        // Method which handles the 'Add Records / Accounts' button being clicked
        private void AddRecordButton_Click(object sender, RoutedEventArgs e)
        {
            // Instantiate an object of the AdminAdd page
            AdminAdd adminAddPage = new AdminAdd();

            // Navigates to the AdminAdd page
            NavigationService.Navigate(adminAddPage);
        }

        // Method which handles the 'Update Records' button being clicked
        private void UpdateRecordButton_Click(object sender, RoutedEventArgs e)
        {
            // Instantiate an object of the AdminUpdate page
            AdminUpdate adminUpdatePage = new AdminUpdate();

            // Navigates to the AdminUpdate page
            NavigationService.Navigate(adminUpdatePage);
        }

        // Method which handles the 'Delete Records / Accounts' button being clicked
        private void DeleteRecordButton_Click(object sender, RoutedEventArgs e)
        {
            // Instantiate an object of the AdminDelete page
            AdminDelete adminDeletePage = new AdminDelete();

            // Navigates to the AdminDelete page
            NavigationService.Navigate(adminDeletePage);
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
            navigationValidation.ExitApplicationValidation();
        }

        #endregion
    }
}
