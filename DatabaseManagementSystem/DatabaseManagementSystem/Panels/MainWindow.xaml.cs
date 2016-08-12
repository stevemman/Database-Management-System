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

namespace WPFPageSwitch
{
    public partial class MainWindow : UserControl, ISwitchable
    {
        public MySql.Data.MySqlClient.MySqlConnection conn;
        public String Station;

        public MainWindow()
        {
            InitializeComponent();
            ConnectToDatabase();
            SetStation(); //default
        }

        public void ConnectToDatabase()
        {
            string myConnectionString;
            
            myConnectionString = "server=127.0.0.1;uid=root;" +
                "pwd=;database=waterHole;";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();
 
                SetConnection("Successful");
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);

                SetConnection("Failed");
            }
        }

        public void SetStation() //default
        {
            SelectedStation.Text = "Station | No station selected";
        }

        public void SetStation(string v)
        {
            Station = v;
            SelectedStation.Text = "Station | " + v;
            EnableButtons();
        }

        private void SetConnection(string v)
        {
            ConnectionStatus.Text = "Connection | " + v;
        }

        private void EnableButtons()
        {
            Insert_Button.IsEnabled = true;
            View_Button.IsEnabled = true;
        }

         public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void Insert_Button_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new DatabaseEntry(this));
        }

        private void View_Button_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new DatabaseView(this));
        }

        private void Station_Button_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new StationSelector(this));
        }
    }
}
