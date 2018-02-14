// Commented

using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SQLite; // Needed for SQLite functionality


namespace SimplyRugby
{
    public class Database
    {
        #region Variable

        // Sets the path of the stored database as a string called 'simplyRugbyDBConnectionString'.
        private const string simplyRugbyDBConnectionString = @"Data Source=Database/SimplyRugbyDatabase.db;Version=3;";

        #endregion

        #region CreateDatabase Method
        public void CreateDatabase()
        {
            // Initialises a connection to the Database based on the path which is stored as the string 'SimplyRugbyDBConnectionString'.
            SQLiteConnection sqliteConnection = new SQLiteConnection(simplyRugbyDBConnectionString);

            try
            {
                // Calls the Open() method from the SQLiteConnection class to open a connection with the database.
                sqliteConnection.Open();

                // A string set up to store a database command for creating the table called 'members'
                string commandMembersTable = "CREATE TABLE members (MembershipNo INTEGER PRIMARY KEY, MemberName VARCHAR(20), EmailAddress VARCHAR(30), PhoneNo VARCHAR(11), DateOfBirth VARCHAR(10), SRUNo VARCHAR(9), MembershipType VARCHAR(20), StandardSkillLevel INT, SpinSkillLevel INT, PopSkillLevel INT, FrontSkillLevel INT, RearSkillLevel INT, SideSkillLevel INT, ScrabbleSkillLevel INT, DropSkillLevel INT, PuntSkillLevel INT, GrubberSkillLevel INT, GoalSkillLevel INT, PassingComments VARCHAR(100), TacklingComments VARCHAR(100), KickingComments VARCHAR(100), LastUpdatedDate VARCHAR(100));";

                // A string set up to store a database command for creating the table called 'login'
                string commandLoginTable = "CREATE TABLE login (LoginID INTEGER PRIMARY KEY, Username VARCHAR(100), Password VARCHAR(100), AccountType VARCHAR(6));";


                // Instantiates a new object of the SQLiteCommand class, using the string 'commandMembersTable' and the database connection
                SQLiteCommand sqliteMembersTableCommand = new SQLiteCommand(commandMembersTable, sqliteConnection);

                // Instantiates a new object of the SQLiteCommand class, using the string 'commandLoginTable' and the database connection
                SQLiteCommand sqliteLoginTableCommand = new SQLiteCommand(commandLoginTable, sqliteConnection);

                // Calls the ExecuteNonQuery() from the SQLiteCommand class to execute the command 'sqliteCommand'
                sqliteMembersTableCommand.ExecuteNonQuery();

                // Calls the ExecuteNonQuery() from the SQLiteCommand class to execute the command 'sqliteCommand'
                sqliteLoginTableCommand.ExecuteNonQuery();

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

        #region AddRecord Methods

        // Method which takes in 4 values and adds a new record to the login table
        public void AddRecord(SQLiteConnection sqliteConnection, string username, string password, string accountType)
        {
            try
            {
                // Calls the Open() method from the SQLiteConnection class to open a connection with the database.
                sqliteConnection.Open();

                // A string set up to store a database command.
                string command = "INSERT INTO login (Username, Password, AccountType) VALUES('" + username + "', '" + password + "', '" + accountType + "')";

                // Instantiates a new object of the SQLiteCommand class, using the string 'command' and the database connection
                SQLiteCommand sqliteCommand = new SQLiteCommand(command, sqliteConnection);

                // Calls the ExecuteNonQuery() from the SQLiteCommand class to execute the command 'sqliteCommand'
                sqliteCommand.ExecuteNonQuery();

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

        // Method which takes in 7 values and adds a new record to the members table
        public void AddRecord(SQLiteConnection sqliteConnection, string memberName, string emailAddress, string phoneNo, string dateOfBirth, string sruNo, string membershipType)
        {
            try
            {
                // Sets up a constant string, used for inputting default values
                const string nA = "N/A";

                // Calls the Open() method from the SQLiteConnection class to open a connection with the database.
                sqliteConnection.Open();

                // A string set up to store a database command.
                string command = "INSERT INTO members (MemberName, EmailAddress, PhoneNo, DateOfBirth, SRUNo, MembershipType, StandardSkillLevel, SpinSkillLevel, PopSkillLevel, FrontSkillLevel, RearSkillLevel, SideSkillLevel, ScrabbleSkillLevel, DropSkillLevel, PuntSkillLevel, GrubberSkillLevel, GoalSkillLevel, PassingComments, TacklingComments, KickingComments, LastUpdatedDate) VALUES('" + memberName + "', '" + emailAddress + "', '" + phoneNo + "', '" + dateOfBirth + "', '" + sruNo + "', '" + membershipType + "', '" + 0 + "','" + 0 + "', '" + 0 + "', '" + 0 + "', '" + 0 + "', '" + 0 + "', '" + 0 + "', '" + 0 + "', '" + 0 + "', '" + 0 + "', '" + 0 + "', '" + nA + "', '" + nA + "' , '" + nA + "',  '" + nA + "')";

                // Instantiates a new object of the SQLiteCommand class, using the string 'command' and the database connection
                SQLiteCommand sqliteCommand = new SQLiteCommand(command, sqliteConnection);

                // Calls the ExecuteNonQuery() from the SQLiteCommand class to execute the command 'sqliteCommand'
                sqliteCommand.ExecuteNonQuery();

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

        #region UpdateRecord Methods

        // Method which takes in 4 values and updates an existing record in the login table
        public void UpdateRecord(SQLiteConnection sqliteConnection, string username, string password, int loginID)
        {
            try
            {
                // Calls the Open() method from the SQLiteConnection class to open a connection with the database.
                sqliteConnection.Open();

                // A string set up to store a database command.
                string command = "UPDATE login SET Username='" + username + "', Password='" + password + "' WHERE LoginID='" + loginID + "' ";

                // Instantiates a new object of the SQLiteCommand class, using the string 'command' and the database connection
                SQLiteCommand sqliteCommand = new SQLiteCommand(command, sqliteConnection);

                // Calls the ExecuteNonQuery() from the SQLiteCommand class to execute the command 'sqliteCommand'
                sqliteCommand.ExecuteNonQuery();

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

        // Method which takes in 7 values and updates an existing record in the members table
        public void UpdateRecord(SQLiteConnection sqliteConnection, string membershipNo, string memberName, string emailAddress, string phoneNo, string dateOfBirth, string sruNo, string membershipType)
        {
            try
            {
                // Calls the Open() method from the SQLiteConnection class to open a connection with the database.
                sqliteConnection.Open();

                // A string set up to store a database command.
                string command = "UPDATE members SET MembershipNo='" + membershipNo + "', MemberName='" + memberName + "', EmailAddress='" + emailAddress + "', PhoneNo='" + phoneNo + "', DateOfBirth='" + dateOfBirth + "', SRUNo='" + sruNo + "', MembershipType='" + membershipType + "' WHERE MembershipNo='" + membershipNo + "' ";

                // Instantiates a new object of the SQLiteCommand class, using the string 'command' and the database connection
                SQLiteCommand sqliteCommand = new SQLiteCommand(command, sqliteConnection);

                // Calls the ExecuteNonQuery() from the SQLiteCommand class to execute the command 'sqliteCommand'
                sqliteCommand.ExecuteNonQuery();

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

        // Method which takes in 17 values and updates an existing record in the members table
        public void UpdateRecord(SQLiteConnection sqliteConnection, string StandardSkill, string SpinSkill, string PopSkill, string FrontSkill, string RearSkill, string SideSkill, string ScrabbleSkill, string DropSkill, string PuntSkill, string GrubberSkill, string GoalSkill, string PassingComments, string TacklingComments, string KickingComments, string currentDateAndTime, string MembershipNo)
        {
            try
            {
                // Calls the Open() method from the SQLiteConnection class to open a connection with the database.
                sqliteConnection.Open();

                // A string set up to store a database command.
                string command = "UPDATE members SET StandardSkillLevel='" + StandardSkill + "', SpinSkillLevel='" + SpinSkill + "', PopSkillLevel='" + PopSkill + "', FrontSkillLevel='" + FrontSkill + "', RearSkillLevel='" + RearSkill + "', SideSkillLevel='" + SideSkill + "', ScrabbleSkillLevel='" + ScrabbleSkill + "', DropSkillLevel='" + DropSkill + "', PuntSkillLevel='" + PuntSkill + "', GrubberSkillLevel='" + GrubberSkill + "', GoalSkillLevel='" + GoalSkill + "', PassingComments='" + PassingComments + "', TacklingComments='" + TacklingComments + "', KickingComments='" + KickingComments + "', LastUpdatedDate='" + currentDateAndTime + "' WHERE MembershipNo='" + MembershipNo + "' ";

                // Instantiates a new object of the SQLiteCommand class, using the string 'command' and the database connection
                SQLiteCommand sqliteCommand = new SQLiteCommand(command, sqliteConnection);

                // Calls the ExecuteNonQuery() from the SQLiteCommand class to execute the command 'sqliteCommand'
                sqliteCommand.ExecuteNonQuery();

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

        #region DeleteRecord Method

        // Method which handles deleting a record from the database
        public void DeleteRecord(SQLiteConnection sqliteConnection, string condition, string type)
        {
            try
            {
                // The program will only enter this IF statement if the value for the string 'type' is equal to "record"
                if (type == "record")
                {
                    // Calls the Open() method from the SQLiteConnection class to open a connection with the database.
                    sqliteConnection.Open();

                    // A string set up to store a database command.
                    string command = "DELETE FROM members WHERE MembershipNo = '" + condition + "' ";

                    // Instantiates a new object of the SQLiteCommand class, using the string 'command' and the database connection
                    SQLiteCommand sqliteCommand = new SQLiteCommand(command, sqliteConnection);

                    // Calls the ExecuteNonQuery() from the SQLiteCommand class to execute the command 'sqliteCommand'
                    sqliteCommand.ExecuteNonQuery();

                    // Calls the Close() method from the SQLiteConnection class to close the connection with the database
                    sqliteConnection.Close();
                }

                // The program will only enter this ELSE IF statement if the value for the string 'type' is equal to "account"
                else if (type == "account")
                {
                    // Calls the Open() method from the SQLiteConnection class to open a connection with the database.
                    sqliteConnection.Open();

                    // A string set up to store a database command.
                    string command = "DELETE FROM login WHERE Username = '" + condition + "' ";

                    // Instantiates a new object of the SQLiteCommand class, using the string 'command' and the database connection
                    SQLiteCommand sqliteCommand = new SQLiteCommand(command, sqliteConnection);

                    // Calls the ExecuteNonQuery() from the SQLiteCommand class to execute the command 'sqliteCommand'
                    sqliteCommand.ExecuteNonQuery();

                    // Calls the Close() method from the SQLiteConnection class to close the connection with the database
                    sqliteConnection.Close();
                }

                // The program will only enter this ELSE statement if the value for the string 'type' is not equal to either "record" or "account"
                else
                {
                    MessageBox.Show("There has been an issue with deleting that database record.\nPlease try again.");
                    return;
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

        #region Display Data Methods

        // Method takes in a database connection and a query returns a SQLiteDataAdapter object
        public SQLiteDataAdapter UpdateDatabaseView(SQLiteConnection sqliteConnection, string query)
        {
            // Calls the Open() method from the SQLiteConnection class to open a connection with the database.
            sqliteConnection.Open();

            // Instantiates a new object of the SQLiteCommand class, using the string 'query' and the database connection
            SQLiteCommand createCommand = new SQLiteCommand(query, sqliteConnection);

            // Calls the ExecuteNonQuery() from the SQLiteCommand class to execute the command 'createCommand'
            createCommand.ExecuteNonQuery();

            // Instantiates a new object of the SQLiteDataAdapter class, passing the SQLiteCommand object 'createCommand'
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(createCommand);

            // Returns the SQLiteDataAdapter object
            return dataAdapter;
        }

        // Method takes in a database connection and a query returns a SQLiteDataReader object
        public SQLiteDataReader SetUpDataReader(SQLiteConnection sqliteConnection, string query)
        {
            // Calls the Open() method from the SQLiteConnection class to open a connection with the database.
            sqliteConnection.Open();

            // Instantiates a new object of the SQLiteCommand Class, using the string 'query' and the database connection
            SQLiteCommand sqliteCommand = new SQLiteCommand(query, sqliteConnection);

            // Calls the method ExecuteReader() from the SQLiteCommand class, to create the SQLiteDataReader object  
            SQLiteDataReader dataReader = sqliteCommand.ExecuteReader();

            // Returns the SQLiteDataReader object
            return dataReader;
        }

        #endregion
    }
}