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

namespace DatabaseManagementSystem
{
    public partial class MainWindow : Window
    {
        MySql.Data.MySqlClient.MySqlConnection conn;

        public MainWindow()
        {
            InitializeComponent();
            ConnectToDatabase();
            SetStation("No station selected");
        }

        public void ConnectToDatabase()
        {
            string myConnectionString;
            
            myConnectionString = "server=127.0.0.1;uid=root;" +
                "pwd=;database=test2;";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();
                DatabaseManagementSystem.NotificationWindow nw = new NotificationWindow("Connection successful!");
                nw.Show();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                DatabaseManagementSystem.NotificationWindow nw = new NotificationWindow("Connection failed!");
                nw.Show();
            }
        }

        private void SetStation(string v)
        {
            SelectedStation.Text = "Station | " + v;
        }

        private void SetConnection

        private void EnableMenu()
        {
            MenuItem_Insert.IsEnabled = true;
            MenuItem_View.IsEnabled = true;
        }

        private void MenuItem_Insert_Click(object sender, RoutedEventArgs e)
        {
            DatabaseManagementSystem.DatabaseEntry de = new DatabaseEntry();
            de.Show();
        }

        private void MenuItem_Laser_Click(object sender, RoutedEventArgs e)
        {
            EnableMenu();
            SetStation("Laser");
        }

        private void MenuItem_3DPrinter_Click(object sender, RoutedEventArgs e)
        {
            EnableMenu();
            SetStation("3DPrinter");
        }

        private void MenuItem_Retry_Click(object sender, RoutedEventArgs e)
        {
            ConnectToDatabase();
        }
    }
}
