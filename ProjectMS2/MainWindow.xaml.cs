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
using System.Timers;
using System.Windows.Threading;

namespace ProjectMS2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ChatRoom ch;
        private System.Timers.Timer RetrieveTimer;
        public MainWindow(ChatRoom tmp)
        {
            InitializeComponent();
            timer();
            ch = tmp;
            isConnected();
        }

        private void click_btn_Register(object sender, RoutedEventArgs e)
        {
            RegisterWindow window = new RegisterWindow(this.ch);
            window.Show();
        }
        private void click_btn_Login(object sender, RoutedEventArgs e)
        {

            LoginWindow window = new LoginWindow(this.ch);
            window.ShowDialog();
            if (window.isLogged)
            {
                ChatroomWindow window2 = new ChatroomWindow(this.ch);
                window2.Show();
                RetrieveTimer.Enabled = false;
                Close();
            }


        }
        private void click_btn_Exit(object sender, RoutedEventArgs e)
        {
            ch.Exit();
            Close();
        }
        #region timer
        private void timer()
        {
            // Create a timer with a two second interval.
            RetrieveTimer = new System.Timers.Timer(1000);
            // Hook up the Elapsed event for the timer. 
            RetrieveTimer.Elapsed += OnTimedEvent;

            RetrieveTimer.AutoReset = true;
            RetrieveTimer.Enabled = true;

        }
        private void isConnected() // checking if connected to the server. and showing the right icon + label 
        {
            if (ch.isConnected())
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                 new Action(() =>
                  {
                      if(radio_connected.IsVisible == false)
                          ch.Retrieve(ChatroomWindow.TIME, string.Empty, string.Empty);

                      btn_login.IsEnabled = true;
                      btn_register.IsEnabled = true;
                      radio_connected.Visibility = Visibility.Visible;
                      radio_notConnected.Visibility = Visibility.Hidden;
                  }));


            }
            else
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                new Action(() =>
                {
                    btn_login.IsEnabled = false;
                    btn_register.IsEnabled = false;
                    radio_connected.Visibility = Visibility.Hidden;
                    radio_notConnected.Visibility = Visibility.Visible;
                }));
            }

        }
        private void OnTimedEvent(Object source, ElapsedEventArgs e) //every second checking if the client is connected to the server
        {
            isConnected();
        }
        #endregion
    }
}
