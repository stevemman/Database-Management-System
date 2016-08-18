using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : UserControl, ISwitchable
    {
        WPFPageSwitch.MainWindow mw;

        public Admin(WPFPageSwitch.MainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void button_Back_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new StationSelector(mw));
        }

        public void newEntry()
        {
            MySqlConnection conn;
            string myConnectionString = "server=127.0.0.1;uid=admin;pwd=;database=waterhole;";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;

                MySqlCommand cmd = new MySqlCommand("INSERT INTO pn (pn, lastStation) VALUES (@pn, @lastStation)");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@pn", Pn.Text);
                cmd.Parameters.AddWithValue("@lastStation", LastStation.Text);
                if (conn.State != ConnectionState.Open) { conn.Open(); }
                cmd.ExecuteNonQuery();
            }
            catch(MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool checkIfEmpty()
        {
            if (string.IsNullOrWhiteSpace(Pn.Text) || string.IsNullOrWhiteSpace(LastStation.Text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void button_OK_Click(object sender, RoutedEventArgs e)
        {
            if (!checkIfEmpty())
            {
                newEntry();
            }
            Switcher.Switch(new Admin(mw));
        }
    }

}
