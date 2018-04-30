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
using ProjectMS2.PresentationLayer;
using ProjectMS2.BusinessLayer;

namespace ProjectMS2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private ChatRoom ch;
        public MainWindow(ChatRoom tmp)
        {
            ch = tmp;
            InitializeComponent();
        }

        private void click_btn_Register(object sender, RoutedEventArgs e)
        {
            RegisterWindow window = new RegisterWindow(this.ch);
            window.Show();
        }
        private void click_btn_Login(object sender, RoutedEventArgs e)
        {
            
            LoginWindow window = new LoginWindow();
            window.Show();
        }
        private void click_btn_Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
