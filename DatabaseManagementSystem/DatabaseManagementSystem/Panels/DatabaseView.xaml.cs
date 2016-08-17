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
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Data;

namespace WPFPageSwitch
{
    /// <summary>
    /// Interaction logic for DatabaseView.xaml
    /// </summary>
    public partial class DatabaseView : UserControl, ISwitchable
    {
        WPFPageSwitch.MainWindow mw;
       

        public DatabaseView( WPFPageSwitch.MainWindow mw )
        {
            InitializeComponent();
            this.mw = mw;
            View("SELECT * FROM (SELECT id, pn, station, SUM(qty) as qty, isLastStation, timestamp FROM data AS A GROUP BY station, pn) AS te WHERE station = '" + mw.Station + "'");
        }

        private void View( String sql )
        {
            string myConnectionString = "server=127.0.0.1;uid=admin;" +
                "pwd=;database=waterhole;";
            try
            {
                MySqlConnection connection = new MySqlConnection(myConnectionString);

                MySqlCommand cmdSel = new MySqlCommand(sql, connection);
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmdSel);
                da.Fill(dt);
                dataGrid.DataContext = dt;
                dataGrid.IsReadOnly = true;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void button_Back_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(mw);
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            View("SELECT * FROM data");
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            View("SELECT * FROM (SELECT id, pn, station, SUM(qty) as qty, isLastStation, timestamp FROM data AS A GROUP BY station, pn) AS te WHERE station = '" + mw.Station + "'");
        }
    }
}
