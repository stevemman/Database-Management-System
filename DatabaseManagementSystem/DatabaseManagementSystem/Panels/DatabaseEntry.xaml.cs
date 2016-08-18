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
        private string myConnectionString = "server=127.0.0.1;uid=admin;pwd=;database=waterhole;";

        public DatabaseEntry(WPFPageSwitch.MainWindow mw)
        {
            InitializeComponent();
            InitComboBox();
            this.mw = mw;
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void newEntry()
        {
            String lastStation;

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;

                MySqlCommand cmd = new MySqlCommand("INSERT INTO data (pn, station, qty, isLastStation) VALUES (@pn, @station, @qty, @isLastStation)");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@pn", Pn.Text);
                cmd.Parameters.AddWithValue("@station", mw.Station);
                if (radioButton_Dispatch.IsChecked == true)
                {
                    cmd.Parameters.AddWithValue("@qty", Int32.Parse(Qty.Text) * -1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@qty", Int32.Parse(Qty.Text));
                }

                String sql = "SELECT lastStation FROM pn WHERE pn='" + Pn.Text + "'";
                MySqlCommand cmdSel = new MySqlCommand(sql, conn);
                DataSet ds = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter(cmdSel);
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count != 0)
                {
                    lastStation = ds.Tables[0].Rows[0]["lastStation"].ToString();

                    if (lastStation.Equals(mw.Station))
                    {
                        cmd.Parameters.AddWithValue("@isLastStation", true);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@isLastStation", false);
                    }
                }
                else
                {
                    return;
                }

                if (conn.State != ConnectionState.Open) { conn.Open(); }
                cmd.ExecuteNonQuery();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InitComboBox()
        {
            string query = "SELECT pn, lastStation FROM pn";
            MySqlConnection conn = new MySqlConnection(myConnectionString);
            MySqlDataAdapter da = new MySqlDataAdapter(query, conn);

            conn.Open();
            DataSet ds = new DataSet();
            da.Fill(ds, "Part No");

            

            foreach ( DataRow dr in ds.Tables[0].Rows )
            {
                comboBox1.Items.Add(dr["pn"]);
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

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                Pn.Text = string.Empty;
            }
            else {
                Pn.Text = comboBox1.SelectedItem.ToString();
            }
        }

        private void radioButton_Insert_Checked(object sender, RoutedEventArgs e)
        {
            if (radioButton_Dispatch.IsChecked == true)
            {
                radioButton_Dispatch.IsChecked = false;
            }
        }

        private void radioButton_Dispatch_Checked(object sender, RoutedEventArgs e)
        {
            if (radioButton_Insert.IsChecked == true)
            {
                radioButton_Insert.IsChecked = false;
            }
        }
    }
}
