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
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
       private ChatRoom ch;

        public RegisterWindow(ChatRoom chatRoom)
        {
            this.ch = chatRoom;
            InitializeComponent();
        }

        private void txtbox_usernameReg(object sender, TextChangedEventArgs e)
        {

        }
        private void txtbox_gIDReg(object sender, TextChangedEventArgs e)
        {

        }

        private void btn_register_Click(object sender, RoutedEventArgs e)
        {
            if(ch.Register(txtBox_usernameReg.Text, txtBox_gIDReg.Text))
            {
                MessageBox.Show("Registered Successfully");
                Close();
            }
            else
            {
                MessageBox.Show("this Username: " + txtBox_usernameReg.Text + " and G-Id: " + txtBox_gIDReg.Text + " is already registered");
            }
        }

        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


    }
}
