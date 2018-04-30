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
using ProjectMS2.BusinessLayer;

namespace ProjectMS2.PresentationLayer
{
    /// <summary>
    /// Interaction logic for ChatroomWindow.xaml
    /// </summary>
    public partial class ChatroomWindow : Window
    {
        private ChatRoom ch;
       
        
        public ChatroomWindow(ChatRoom ch)
        {
            InitializeComponent();
            this.ch = ch;
            ch.Retrieve();
            refresh();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void click_btn_logout(object sender, RoutedEventArgs e)
        {
            ch.logout();
            MainWindow window2 = new MainWindow(ch);
            window2.Show();
            Close();

        }
        private void refresh()
        {

            txt_msg.Text = ch.Display(20);
        }

        private void btn_refresh_Click(object sender, RoutedEventArgs e)
        {
            refresh();
        }
    }
}
