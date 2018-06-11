using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using ProjectMS2.BusinessLayer;

namespace ProjectMS2.PresentationLayer
{
    /// <summary>
    /// Interaction logic for ChatroomWindow.xaml
    /// </summary>
    public partial class ChatroomWindow : Window
    {

        #region Fields/Properties
        //static filter types:
        private static String TIME = "SendTime";
        private static String NICKNAME = "Nickname";
        private static String GROUP_ID = "Group_Id";
        private ChatRoom ch;
        private bool isConnected;
        private String sortType;


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
        #endregion
        #region Constructor
        public ChatroomWindow(ChatRoom ch)
        {

            InitializeComponent();
            this.ch = ch;
            DataContext = ch;
            timer();
            txtBox_sendMsg.Text = String.Empty;
            isConnected = false;
            btnVisible();
            chk_des.IsChecked = true;
            chk_time.IsChecked = true;
            this.sortType = TIME;
         
        }
        #endregion
        #region MainFunctions
        private void click_btn_logout(object sender, RoutedEventArgs e)
        {
            ch.logout();
            MainWindow window2 = new MainWindow(ch);
            window2.Show();
            RetrieveTimer.Enabled = false;
            Close();

        }
        private void btnVisible()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (isConnected)
                {
                    btn_send.IsEnabled = true;
                    lbl_Con.Visibility = Visibility.Visible;
                    lbl_nCon.Visibility = Visibility.Hidden;
                }
                else
                {
                    btn_send.IsEnabled = false;
                    lbl_Con.Visibility = Visibility.Hidden;
                    lbl_nCon.Visibility = Visibility.Visible;
                }
            });
        }
        private void Button_Send_Click(object sender, RoutedEventArgs e)
        {
            if (isConnected)
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

        }
        private void chk_time_Checked(object sender, RoutedEventArgs e)
        {
            sortType = TIME;
            ch.sortTypeChanged(TIME);
        }

        private void chk_nickname_Checked(object sender, RoutedEventArgs e)
        {
            sortType = NICKNAME;
            ch.sortTypeChanged(NICKNAME);
        }

        private void chk_gId_Checked(object sender, RoutedEventArgs e)
        {
            sortType = GROUP_ID;
            ch.sortTypeChanged(GROUP_ID);
        }
        private void filterbtn_Click(object sender, RoutedEventArgs e)
        {
            txtBox_IdFilter.Text = String.Empty;
            txtBox_uNameFilter.Text = String.Empty;
        }
        private void txtBox_IdFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                if (txtBox_IdFilter.Text == "")
                {
                    txtBox_uNameFilter.Visibility = Visibility.Hidden;
                    lbl_uName.Visibility = Visibility.Hidden;
                    txtBox_uNameFilter.Text = "";
                    ch.emptyDisplayList();
                    ch.Retrieve(sortType, txtBox_IdFilter.Text, txtBox_uNameFilter.Text);
                }
                else
                {
                    txtBox_uNameFilter.Visibility = Visibility.Visible;
                    lbl_uName.Visibility = Visibility.Visible;
                    ch.emptyDisplayList();
                    ch.Retrieve(sortType,txtBox_IdFilter.Text,txtBox_uNameFilter.Text);
                   

                }
               
            }));
        }
        private void txtBox_uNameFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            ch.emptyDisplayList();
            ch.Retrieve(sortType, txtBox_IdFilter.Text, txtBox_uNameFilter.Text);
        }
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                btn_send.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }
        private void chk_as_Checked(object sender, RoutedEventArgs e)
        {
            if (ch.descending)
            {
                chk_des.IsChecked = false;
                ch.reverse();
            }
            
        }
        private void chk_des_Checked(object sender, RoutedEventArgs e)
        {
            if (!ch.descending)
            {
                chk_as.IsChecked = false;
                ch.reverse();
            }
        }
        #endregion
        #region Timer
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
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                     new Action(() =>
                     {
                         ch.Retrieve(sortType, txtBox_IdFilter.Text, txtBox_uNameFilter.Text);
                     }));
        }
        #endregion

    }
}

