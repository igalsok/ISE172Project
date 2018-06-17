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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private bool _isLogged;
        public bool isLogged
        {
            get { return this._isLogged; }
            set { this._isLogged = value; }
        }
     
        public LoginWindow()
        {
            InitializeComponent();
        }

        private ChatRoom ch;

        public LoginWindow(ChatRoom chatRoom)
        {
            this.ch = chatRoom;
            InitializeComponent();
        }


        //checking if the login is successful, if it is, closing the window and opening the chatroom window.
        private void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            try {
                ch.Login(txtBox_usernameLog.Text, Convert.ToInt32(txtBox_gIDlog.Text), pwd_box.Password.ToString());
                this.isLogged = true;
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
