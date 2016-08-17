using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace WPFPageSwitch
{
    /// <summary>
    /// Interaction logic for DatabaseEntry.xaml
    /// </summary>
    public partial class DatabaseEntry : UserControl, ISwitchable
    {
        WPFPageSwitch.MainWindow mw;
        private MySqlConnection conn;

        public DatabaseEntry(WPFPageSwitch.MainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void newEntry()
        {
            string myConnectionString = "server=127.0.0.1;uid=admin;" +
                "pwd=;database=waterhole;";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
               
                MySqlCommand cmd = new MySqlCommand("INSERT INTO data (pn, station, qty, isLastStation) VALUES (@pn, @station, @qty, @ils)");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@pn", Pn.Text);
                cmd.Parameters.AddWithValue("@station", mw.Station);
                cmd.Parameters.AddWithValue("@qty", Int32.Parse(Qty.Text));
                if (Ils.IsChecked ?? false)
                {
                    cmd.Parameters.AddWithValue("@ils", true);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ils", false);
                }
                if (conn.State != ConnectionState.Open) { conn.Open(); }
                cmd.ExecuteNonQuery();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool checkIfEmpty()
        {
            if (string.IsNullOrWhiteSpace(Pn.Text) || string.IsNullOrWhiteSpace(Qty.Text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool checkIfCorrectType()
        {
            if (!checkIfEmpty())
            {
                try
                {
                    Int32.Parse(Qty.Text);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void button_OK_Click(object sender, RoutedEventArgs e)
        {
            if (!checkIfEmpty() && checkIfCorrectType())
            {
                newEntry();
                Switcher.Switch(mw);
            }
        }

        private void button_Back_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(mw);
        }
    }
}
