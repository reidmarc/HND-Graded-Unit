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



namespace SimplyRugby
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Object

        // Instantiates a new object of the Database Class.
        Database simplyRugbyDatabases = new Database();

        #endregion

        #region Constructor

        public MainWindow()
        {
            InitializeComponent();

            //Only used once on intial start up to create the databases            
            //simplyRugbyDatabases.CreateDatabase();

            // Sets the frame 'MainFrame' to load the 'LogIn' page when then application is run.
            MainFrame.Content = new SimplyRugby.LogIn();

            MessageBox.Show("Any red buttons are only included as QOL features for the marker.");
        }

        #endregion
    }
}