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

namespace WPFPageSwitch
{
    /// <summary>
    /// Interaction logic for DatabaseEntry.xaml
    /// </summary>
    public partial class DatabaseEntry : UserControl, ISwitchable
    {
        WPFPageSwitch.MainWindow mw;

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
            string connectionString = "Data Source=127.0.0.1;" +
                                      "Initial Catalog=watehole;" +
                                      "User id=root;" +
                                      "Password=;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO data (pn, station, qty, isLastStation) VALUES (@pn, @station, @qty, @ils)");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
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
                if (connection.State != ConnectionState.Open) { connection.Open(); }
                cmd.ExecuteNonQuery();
            }
        }

        private void button_OK_Click(object sender, RoutedEventArgs e)
        {
            newEntry();
            Switcher.Switch(mw);
        }

        private void button_Back_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(mw);
        }
    }
}
