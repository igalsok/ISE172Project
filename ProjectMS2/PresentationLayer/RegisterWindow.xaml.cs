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

        private void btn_register_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ch.Register(txtBox_usernameReg.Text, Convert.ToInt32(txtBox_gIDReg.Text), pwd_box.Password.ToString());
                MessageBox.Show("Registered Successfully");
                Close();
            }
            catch (Exception ex)
            {
                if (ex.GetType().IsAssignableFrom(typeof(System.FormatException)))
                {
                    System.Windows.MessageBox.Show("Please fill in all the fields!");
                }
                else
                System.Windows.MessageBox.Show(ex.Message);
            } 
        }

        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


    }
}
