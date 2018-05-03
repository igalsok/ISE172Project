using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public partial class ChatroomWindow : Window, INotifyPropertyChanged
    {

        private ChatRoom ch;
        private int _chked;
        private ObservableCollection<Message> FilterList;
        public ObservableCollection<Message> UserMsgList;
        public ObservableCollection<Message>GroupMsgList;
        private ObservableCollection<Message> _msgLst;
        public ObservableCollection<Message> MsgLst
        {
            get
            {
                return this._msgLst;
            }
            set
            {

                this._msgLst = value;
                NotifyPropertyChanged("MsgLst");
            }
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
        public ChatroomWindow(ChatRoom ch)
        {

            InitializeComponent();
            this._chked = 1;
            this.ch = ch;
            this.MsgLst = ch.msgList;
            FilterList = new ObservableCollection<Message>();
            DataContext = this;
            timer();
            txtBox_sendMsg.Text = String.Empty;
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
            sortDisplay();
            filter();
        }


        private void click_btn_logout(object sender, RoutedEventArgs e)
        {
            ch.logout();
            MainWindow window2 = new MainWindow(ch);
            window2.Show();
            Close();

        }


        private void Button_Send_Click(object sender, RoutedEventArgs e)
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


        private void chk_time_Checked(object sender, RoutedEventArgs e)
        {
            chk_gId.IsChecked = false;
            chk_uName.IsChecked = false;
            this._chked = 1;
            
        }

        private void chk_uName_Checked(object sender, RoutedEventArgs e)
        {
            chk_gId.IsChecked = false;
            chk_time.IsChecked = false;
            this._chked = 2;
           

        }

        private void chk_gId_Checked(object sender, RoutedEventArgs e)
        {
            chk_time.IsChecked = false;
            chk_uName.IsChecked = false;
            this._chked = 3;
            

        }
        private void sortDisplay()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {


                switch (this._chked)
                {
                    case 1:
                        if (txtBox_IdFilter.Text == "")
                            MsgLst = ch.msgList;
                        else
                            FilterList = ch.msgList;
                        break;
                    case 2:
                        if (txtBox_IdFilter.Text == "")
                            this.MsgLst = new ObservableCollection<Message>(from i in ch.msgList orderby i.GroupID orderby i.UserName  select i);
                        else
                            FilterList = new ObservableCollection<Message>(from i in ch.msgList orderby i.GroupID orderby i.UserName select i);
                        break;
                    case 3:
                        if (txtBox_IdFilter.Text == "")
                            this.MsgLst = new ObservableCollection<Message>(from i in ch.msgList  orderby i.UserName orderby i.GroupID select i);
                        else
                            FilterList = new ObservableCollection<Message>(from i in ch.msgList orderby i.UserName orderby i.GroupID select i);
                        break;

                }
            });
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        private void filter()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {

                if (txtBox_IdFilter.Text != "")
                {
                    ObservableCollection<Message> tmpList = new ObservableCollection<Message>();
                    foreach (Message msg in FilterList)
                    {
                        if (msg.GroupID.Equals(txtBox_IdFilter.Text))
                        {
                            tmpList.Add(msg);
                        }
                    }

                    if (txtBox_uNameFilter.Text != "")
                    {
                        foreach (Message msg in tmpList.ToList<Message>())
                        {
                            if (!msg.UserName.Equals(txtBox_uNameFilter.Text))
                            {
                                tmpList.Remove(msg);
                            }
                        }

                    }
                    this.MsgLst = tmpList;
                }
            });
        }

        private void filterbtn_Click(object sender, RoutedEventArgs e)
        {
            txtBox_IdFilter.Text = String.Empty;
            txtBox_uNameFilter.Text = String.Empty;
        }

        private void txtBox_IdFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            Dispatcher.Invoke(new Action(() => {
                if(txtBox_IdFilter.Text == "")
                {
                    txtBox_uNameFilter.Visibility = Visibility.Hidden;
                    lbl_uName.Visibility = Visibility.Hidden;
                }
                else
                {
                    txtBox_uNameFilter.Visibility = Visibility.Visible;
                    lbl_uName.Visibility = Visibility.Visible;
                }
            }));
        }
    }
}
