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
        public static String TIME = "SendTime";
        public static String NICKNAME = "Nickname";
        public static String GROUP_ID = "Group_Id";
        private ChatRoom ch;
        private String _sortType;
        public String sortType
        {
            get { return this._sortType; }
            set { this._sortType = value; }
        }
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
            btn_send.IsEnabled = false;
            txtBox_sendMsg.MaxLength = 100;
            chk_des.IsChecked = true;
            chk_time.IsChecked = true;
            this.sortType = TIME;
            chk_autoScroll.IsChecked = true;
            ((INotifyCollectionChanged)lst_Display.Items).CollectionChanged += lst_Display_CollectionChanged;
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
        private void lst_Display_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (chk_autoScroll.IsChecked == true && lst_Display.Items.Count > 0)
            {
                if (chk_des.IsChecked == true)
                    lst_Display.ScrollIntoView(lst_Display.Items[lst_Display.Items.Count - 1]);
                else if (chk_as.IsChecked == true)
                    lst_Display.ScrollIntoView(lst_Display.Items[0]);
            }

        }

        private void Button_Send_Click(object sender, RoutedEventArgs e)
        {
            if (btn_send.IsEnabled)
                ch.Send(txtBox_sendMsg.Text);
            txtBox_sendMsg.Text = string.Empty;

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
                    ch.Retrieve(sortType, txtBox_IdFilter.Text, txtBox_uNameFilter.Text);


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
        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null && item.IsSelected)
            {
                if (ch.canEdit((Message)item.DataContext))
                {
                    EditWindow window2 = new EditWindow((Message)item.DataContext, ch, this);
                    window2.ShowDialog();
                }

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
                         try { ch.Retrieve(sortType, txtBox_IdFilter.Text, txtBox_uNameFilter.Text); }
                         catch (Exception ex)
                         {
                             RetrieveTimer.Enabled = false;
                             Hide();
                             System.Windows.MessageBox.Show(ex.Message);
                             ch.logout();
                             MainWindow window2 = new MainWindow(ch);
                             window2.Show();
                             Close();
                         }

                     }));
        }
        #endregion

        private void chk_myFilter_Checked(object sender, RoutedEventArgs e)
        {

            txtBox_IdFilter.Text = Convert.ToString(this.ch.logged.G_id);
            txtBox_uNameFilter.Text = this.ch.logged.Username;


        }
        private void chk_myFilter_UnChecked(object sender, RoutedEventArgs e)
        {
            txtBox_IdFilter.Text = string.Empty;
            txtBox_uNameFilter.Text = string.Empty;
        }

        private void btn_info_click(object sender, RoutedEventArgs e)
        {
            String info = "Hello Everybody! \nthis is our Chatroom \n1.Messages can only contain 100 latters \n2.You can choose from 3 sort types (time,nickname,GroupId) \n3.You can edit your own messages by pressing on wanted message \n    have fun! \n                                                                 version: 2.0.1";
            System.Windows.MessageBox.Show(info);
        }

        private void txtbox_send_Changed(object sender, TextChangedEventArgs e)
        {
            if (txtBox_sendMsg.Text.Equals(string.Empty))
            {
                btn_send.IsEnabled = false;
            }
            else
            {
                btn_send.IsEnabled = true;
            }
        }
    }
}

