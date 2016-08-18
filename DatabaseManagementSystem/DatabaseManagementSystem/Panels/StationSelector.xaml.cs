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

namespace WPFPageSwitch
{
    /// <summary>
    /// Interaction logic for StationSelector.xaml
    /// </summary>
    public partial class StationSelector : UserControl, ISwitchable
    {
        WPFPageSwitch.MainWindow mw;

        public StationSelector(WPFPageSwitch.MainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void button_Laser_Click(object sender, RoutedEventArgs e)
        {
            mw.SetStation("Laser");
            Switcher.Switch(mw);
        }

        private void button_3DPrinter_Click(object sender, RoutedEventArgs e)
        {
            mw.SetStation("3DPrinter");
            Switcher.Switch(mw);
        }

        private void button_Back_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(mw);
        }

        private void button_Administrator_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Admin(mw));
        }
    }
}
