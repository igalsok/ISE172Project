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
using ProjectMS2.BusinessLayer;

namespace ProjectMS2.PresentationLayer
{
    /// <summary>
    /// Interaction logic for ChatroomWindow.xaml
    /// </summary>
    public partial class ChatroomWindow : Window, INotifyPropertyChanged
    {
        
        #region Fields/Properties
        private ChatRoom ch;  
        private int _chked;
        private ObservableCollection<Message> FilterList;
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
        private bool isConnected;
        private bool isReversed;



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
            this._chked = 1;
            this.ch = ch;
            this.MsgLst = FilterList =  ch.msgList;
            DataContext = this;
            timer(); 
            txtBox_sendMsg.Text = String.Empty;
            ((INotifyCollectionChanged)lst_Display.Items).CollectionChanged += ListView_CollectionChanged; // autoscroll
            isConnected = false;
            btnVisible();
            chk_des.IsChecked = true;
        }
        #endregion
        #region MainFunctions
        private void click_btn_logout(object sender, RoutedEventArgs e)
        {
            ch.logout();
            MainWindow window2 = new MainWindow(ch);
            window2.Show();
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
            chk_gId.IsChecked = false;
            chk_uName.IsChecked = false;
            chk_time.IsEnabled = false;
            chk_gId.IsEnabled = true;
            chk_uName.IsEnabled = true;
            this._chked = 1;
            if(ch!=null)
            { 
                if(!isReversed)
                { 
                MsgLst = ch.msgList;
                }
                else
                {
                    ObservableCollection<Message> tmp = new ObservableCollection<Message>(ch.msgList);
                    this.MsgLst = new ObservableCollection<Message>(tmp.Reverse<Message>());
                }
            }

        }
        private void chk_uName_Checked(object sender, RoutedEventArgs e)
        {
            chk_gId.IsChecked = false;
            chk_time.IsChecked = false;
            chk_time.IsEnabled = true;
            chk_uName.IsEnabled = false;
            chk_gId.IsEnabled = true;
            this._chked = 2;
            if(!isReversed)
            this.MsgLst = new ObservableCollection<Message>(from i in ch.msgList orderby i.GroupID orderby i.UserName select i);
            else
            {
                ObservableCollection<Message> tmp = new ObservableCollection<Message>(from i in ch.msgList orderby i.GroupID orderby i.UserName select i);
                this.MsgLst = new ObservableCollection<Message>(tmp.Reverse<Message>());
            }
        }
        private void chk_gId_Checked(object sender, RoutedEventArgs e)
        {
            chk_time.IsChecked = false;
            chk_uName.IsChecked = false;
            chk_time.IsEnabled = true;
            chk_uName.IsEnabled = true;
            chk_gId.IsEnabled = false;
            
            this._chked = 3;
            if(!isReversed)
            { 
            this.MsgLst = new ObservableCollection<Message>(from i in ch.msgList orderby i.UserName orderby i.GroupID select i);
            }
            else
            {
                ObservableCollection<Message> tmp = new ObservableCollection<Message>(from i in ch.msgList orderby i.UserName orderby i.GroupID select i);
                FilterList = new ObservableCollection<Message>(tmp.Reverse<Message>());
            }
          
        }
        private void filterbtn_Click(object sender, RoutedEventArgs e)
        {
            txtBox_IdFilter.Text = String.Empty;
            txtBox_uNameFilter.Text = String.Empty;
        }
        private void txtBox_IdFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            Dispatcher.Invoke(new Action(() => {
                if (txtBox_IdFilter.Text == "")
                {
                    txtBox_uNameFilter.Visibility = Visibility.Hidden;
                    lbl_uName.Visibility = Visibility.Hidden;
                }
                else
                {
                    txtBox_uNameFilter.Visibility = Visibility.Visible;
                    lbl_uName.Visibility = Visibility.Visible;
                }
                filter();
            }));
        }
        private void txtBox_uNameFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            filter();
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
            chk_des.IsChecked = false;
            isReversed = true;
            MsgLst = new ObservableCollection<Message>(MsgLst.Reverse());

        }
        private void chk_des_Checked(object sender, RoutedEventArgs e)
        {
            chk_as.IsChecked = false;
            if (isReversed)
                MsgLst = new ObservableCollection<Message>(MsgLst.Reverse());
            isReversed = false;
            


        }
        #endregion
        #region AutoScroll
        private void ListView_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                // scroll the new item into view   
                lst_Display.ScrollIntoView(e.NewItems[0]);
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
            isConnected = ch.Retrieve();
            btnVisible();
            sortDisplay();
            filter();
        }
        #endregion
        #region ProperyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        #endregion
        #region Sort&Filter
        private void sortDisplay()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {


                switch (this._chked)
                {
                    case 1:
                        if (!isReversed)
                        {
                            if (txtBox_IdFilter.Text == "")
                            {

                                MsgLst = ch.msgList;
                            }
                            else
                                FilterList = ch.msgList;
                        }
                        else
                        {
                            if (txtBox_IdFilter.Text == "")
                            {
                                this.MsgLst = new ObservableCollection<Message>(ch.msgList.Reverse<Message>());
                            }
                            else
                                FilterList = new ObservableCollection<Message>(ch.msgList.Reverse<Message>());
                        }
                        break;
                    case 2:
                        if (!isReversed)
                        {
                            if (txtBox_IdFilter.Text == "")
                            {

                                FilterList = this.MsgLst = new ObservableCollection<Message>(from i in ch.msgList orderby i.GroupID orderby i.UserName select i);
                            }
                            else
                            {
                                FilterList = new ObservableCollection<Message>(from i in ch.msgList orderby i.GroupID orderby i.UserName select i);
                            }
                        }
                        else
                        {
                            if (txtBox_IdFilter.Text == "")
                            {
                                ObservableCollection<Message> tmp = new ObservableCollection<Message>(from i in ch.msgList orderby i.GroupID orderby i.UserName select i);
                                FilterList = this.MsgLst = new ObservableCollection<Message>(tmp.Reverse<Message>());
                            }
                            else
                            {
                              ObservableCollection<Message>  tmp = new ObservableCollection<Message>(from i in ch.msgList orderby i.GroupID orderby i.UserName select i);
                                FilterList = new ObservableCollection<Message>(tmp.Reverse<Message>());
                            }
                        }
                                break;
                    case 3:
                        if (!isReversed)
                        {
                            if (txtBox_IdFilter.Text == "")
                                this.MsgLst = new ObservableCollection<Message>(from i in ch.msgList orderby i.UserName orderby i.GroupID select i);
                            else
                                FilterList = new ObservableCollection<Message>(from i in ch.msgList orderby i.UserName orderby i.GroupID select i);
                        }
                        else
                        {
                            if (txtBox_IdFilter.Text == "")
                            { 
                                ObservableCollection<Message> tmp = new ObservableCollection<Message>(from i in ch.msgList orderby i.UserName orderby i.GroupID select i);
                                FilterList = this.MsgLst = new ObservableCollection<Message>(tmp.Reverse<Message>());
                            }
                            else
                            {
                                ObservableCollection<Message> tmp = new ObservableCollection<Message>(from i in ch.msgList orderby i.UserName orderby i.GroupID select i);
                                FilterList = new ObservableCollection<Message>(tmp.Reverse<Message>());
                            }
                        }
                            break;

                }
            });
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
        #endregion

        
    }
}
