﻿using System;
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

        private void txtbox_usernameLog(object sender, TextChangedEventArgs e)
        {

        }
        private void txtbox_gIDLog(object sender, TextChangedEventArgs e)
        {

        }

        private void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            if (ch.Login(txtBox_usernameLog.Text, txtBox_gIDReg.Text))
            {
                
                this.isLogged = true;
                Close();
            }
            else
            {
                MessageBox.Show("this Username: " + txtBox_usernameLog.Text + " is not registered");
            }
        }

        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


    }
}
