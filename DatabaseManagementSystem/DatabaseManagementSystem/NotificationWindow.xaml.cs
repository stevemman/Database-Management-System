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

namespace DatabaseManagementSystem
{
    /// <summary>
    /// Interaction logic for NotificationWindow.xaml
    /// </summary>
    public partial class NotificationWindow : Window
    {
        public NotificationWindow( String notificationMessage )
        {
            InitializeComponent();
            setText(notificationMessage);
        }

        private void setText(String msg)
        {
            theMessage.Text = msg;
        }

        private void button_DismissNotification_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
