using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
        private System.Timers.Timer _RetrieveTimer;
        public System.Timers.Timer RetrieveTimer
        {
            get
            {
                return this._RetrieveTimer;
            }
            set
            {
                this._RetrieveTimer = value;
            }
        }

        public ChatroomWindow(ChatRoom ch)
        {
            InitializeComponent();
            this.ch = ch;
            timer();
            txtBox_sendMsg.Text =  String.Empty;
        }
        private void timer()
        {
            // Create a timer with a two second interval.
            RetrieveTimer = new System.Timers.Timer(2000);
            // Hook up the Elapsed event for the timer. 
            RetrieveTimer.Elapsed += OnTimedEvent;

            RetrieveTimer.AutoReset = true;
            RetrieveTimer.Enabled = true;

        }
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            ch.Retrieve();
            this.Dispatcher.Invoke(() =>
            {
                txtBox_displayMsg.Text = ch.Display(0);
            });
           
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


        private void Button_Click(object sender, RoutedEventArgs e)
        {
           int caseSwitch = ch.Send(txtBox_sendMsg.Text);
            switch (caseSwitch)
            {
                case 1:
                    MessageBox.Show("Message didn't sent! \n Message cannot be over 150 chars!");
                    txtBox_sendMsg.Text = "";
                    break;
                case 2:
                    MessageBox.Show("Message didn't sent! \n cannot send an empty message!");
                    txtBox_sendMsg.Text = "";
                    break;
                case 3:    
                    txtBox_sendMsg.Text = "";
                    break;
                default:
                    break;
            }
        }

        private void txtBox_displayMsg_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtBox_displayMsg.ScrollToEnd();
        }

    }
}
