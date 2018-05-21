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

namespace ProjectMS2.Testers
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        Testers tester = new Testers();
        public TestWindow()
        {
            InitializeComponent();
        }

        private void btn_Empty_Click(object sender, RoutedEventArgs e)
        {
            if (tester.testEmpty())
            {
                MessageBox.Show("test Successfull");
            }
            else
            {
                MessageBox.Show("test not Successfull");
            }
        }

        private void btn_Over_Click(object sender, RoutedEventArgs e)
        {
            if (tester.testOver())
            {
                MessageBox.Show("test Successfull");
            }
            else
            {
                MessageBox.Show("test not Successfull");
            }
        }

        private void btn_NRL_Click(object sender, RoutedEventArgs e)
        {
            if (tester.testNotRegisteredLogin())
            {
                MessageBox.Show("test Successfull");
            }
            else
            {
                MessageBox.Show("test not Successfull");
            }
        }

        private void btn_NotLoggedOut_Click(object sender, RoutedEventArgs e)
        {
            if (tester.testLogout())
            {
                MessageBox.Show("test Successfull");
            }
            else
            {
                MessageBox.Show("test not Successfull");
            }
        }

        private void btn_SavedUser_Click(object sender, RoutedEventArgs e)
        {
            if (tester.testSavedUser())
            {
                MessageBox.Show("test Successfull");
            }
            else
            {
                MessageBox.Show("test not Successfull");
            }
        }

        private void btn_LoginTwice_Click(object sender, RoutedEventArgs e)
        {
            if (tester.testLoginTwice())
            {
                MessageBox.Show("test Successfull");
            }
            else
            {
                MessageBox.Show("test not Successfull");
            }
        }

        private void btn_RegisterTwice_Click(object sender, RoutedEventArgs e)
        {
            if (tester.testLoginTwice())
            {
                MessageBox.Show("test Successfull");
            }
            else
            {
                MessageBox.Show("test not Successfull");
            }
        }
    }
}
