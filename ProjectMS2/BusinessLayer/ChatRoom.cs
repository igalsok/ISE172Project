using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMS2.PresentationLayer;
using ProjectMS2.PersistentLayer;
using ProjectMS2.CommunicationLayer;
using System.Timers;
using System.Collections.ObjectModel;
using System.Windows;
using System.ComponentModel;

namespace ProjectMS2.BusinessLayer
{
    public class ChatRoom : INotifyPropertyChanged
    {
        #region Fields/Properties
        private User logged;
        private String url = "http://192.168.1.114";
        public ObservableCollection<Message> list;
        private ObservableCollection<Message> _msgList;
        public ObservableCollection<Message> msgList
        {
            get { return _msgList; }
            set
            {
                this._msgList = value;
                NotifyPropertyChanged("msgList");

            }
        }
        public ObservableCollection<Message> filterList;
        private List<User> usersList;
        private log4net.ILog log;
        private MessageHandler MessageHandler;
        private UserHandler UserHandler;
       public int sortBtn;
        public bool isReversed;

        #endregion
        #region constructors
        public ChatRoom(log4net.ILog tmp)
        {
            this.log = tmp;
            this.MessageHandler = new MessageHandler();
            this.UserHandler = new UserHandler();
            this.msgList = new ObservableCollection<Message>(this.MessageHandler.getAll());
            this.filterList = new ObservableCollection<Message>(this.MessageHandler.getAll());
            this.usersList = this.UserHandler.getAll();
            sortBtn = 1;
            isReversed = false;
            this.list = new ObservableCollection<Message>(this.MessageHandler.getAll());
        }
        #endregion
        #region firstMenu
        public Boolean Register(String Username, String Gid)
        {
            bool exists = false;
            foreach (User user in this.usersList)
            {
                if (!exists)
                {
                    if (user.Username.Equals(Username))
                        exists = true;
                }
            }
            if (exists)
            {
                this.log.Warn("attempt to register with the Username:" + Username + ", a user with thie Username already exists");
                return false;
            }
            else
            {
                User registered = new User(Gid, Username);
                this.UserHandler.saveNew(registered);
                log.Info("User registered successfully. Username: " + Username + "group id: " + Gid);
                return true;
            }


        }

        public Boolean Login(String Username, String g_id)
        {

            bool UsernameExists = false;
            bool groupIdExists = false;
            User logging = null;
            foreach (User user in this.usersList)
            {
                if (!UsernameExists)
                    if (user.Username == Username)
                    {
                        UsernameExists = true;
                        if (user.G_id == g_id)
                        {
                            groupIdExists = true;
                            logging = user;
                        }
                    }
            }
            if (!UsernameExists)
            {
                log.Warn("attempt to login with the Username: " + Username + ", a user with thie Username doesn't exists");
                return false;

            }
            else if (!groupIdExists)
            {
                log.Warn("attempt to login with the Username: " + Username + "  ID: " + g_id + " - this ID doesnt match the username");
                return false;
            }
            else
            {
                this.logged = logging;
                log.Info("User loged in successfully. Username: " + Username + "group id: " + g_id);
                return true;
            }
        }
        public void Exit()
        {
            log.Logger.Repository.Shutdown();

        }
        #endregion
        #region chatroomMenu
        public int Retrieve()
        {
            try
            {
                List<IMessage> tmpList = Communication.Instance.GetTenMessages(this.url);
                bool isNew = false;
                foreach (IMessage tmp in tmpList)
                {

                    Message tmpMsg = new Message(tmp);
                    bool exists = false;
                    foreach (Message check in this.list)
                    {
                        if (tmpMsg.Id.Equals(check.Id))
                        {
                            exists = true;
                            break;
                        }
                    }
                    if (!exists)
                    {
                        this.MessageHandler.SaveNew(tmpMsg);
                        App.Current.Dispatcher.Invoke((Action)delegate
                        {
                            this.list.Add(tmpMsg);
                            isNew = true;

                        });
                    }

                }
                if (isNew) { isNew = false; return 2; }
                
                else
                return 1;
            }
            catch (System.AggregateException)
            {
                return 3;
            }
        }


        public void logout()
        {
            log.Info("User: " + this.logged.Username + ", group id:" + this.logged.G_id + ", is logging out");
            this.logged = null;
        }


        public String Display(int num)
        {
            String str = "";
            foreach (Message tmp in this.msgList)
            {

                if (num > 0)
                {
                    str = str + "\n" + tmp;
                    --num;
                }
                else if (num == 0)
                {
                    str = str + "\n" + tmp;
                }
            }
            return str;
        }
        public int Send(String msg)
        {

            if (msg.Length > 150)
            {

                this.log.Info(this.logged.Username + "Tried to write a message over 150 chars");
                return 1;

            }
            else if (msg.Length == 0)
            {
                this.log.Info(this.logged.Username + "Tried to write an empty message");
                return 2;
            }
            else
            {
                this.logged.Send(msg, this.url);
                return 3;

            }






        }
        public void DisplayAll(String Username, String g_id) // Display ALL message from a specified user function
        {


            bool exists = false;
            foreach (Message msg in this.msgList)
            {
                if (msg.UserName.Equals(Username))
                {

                    exists = true;
                }

            }
            if (!exists)
            {

                this.log.Warn("attempt to retrieve messages with wrong userName and GroupID combination:" + Username + " " + g_id);
            }
        }




        #endregion
        #region sort&filter

        public void filter(bool idEmpty, bool uNameEmpty, String IdFilter, String uNameFilter)
        {
            ObservableCollection<Message> tmpList = new ObservableCollection<Message>();
            if (!idEmpty)
            {
                foreach (Message msg in list)
                {
                    if (msg.GroupID.Equals(IdFilter))
                    {
                        tmpList.Add(msg);
                    }
                }

                if (!uNameEmpty)
                {
                    foreach (Message msg in tmpList.ToList<Message>())
                    {
                        if (!msg.UserName.Equals(uNameFilter))
                        {
                            tmpList.Remove(msg);
                        }
                    }

                }

                sort(tmpList.ToList<Message>());
            }
            else
            {
                sort(this.list.ToList<Message>());
            }
        }
      
        public void sort(List<Message> list)
        {
            ObservableCollection<Message> tmpList = new ObservableCollection<Message>();
            switch (sortBtn)
            {
                case 1:
                    if (!isReversed)
                    {
                            tmpList = new ObservableCollection<Message>(list);
                      }
                   
                    else
                    {
                        tmpList = new ObservableCollection<Message>(list.Reverse<Message>());
                    }
                    break;
                case 2:
                    if (!isReversed)
                    {

                       tmpList = new ObservableCollection<Message>(from i in list orderby i.GroupID orderby i.UserName select i);
                    }
                    else
                    {
                        tmpList = new ObservableCollection<Message>((from i in list orderby i.GroupID orderby i.UserName select i).Reverse<Message>());
                    }
                  
                    break;
                case 3:
                    if (!isReversed)
                    {
                        tmpList = new ObservableCollection<Message>(from i in list orderby i.UserName orderby i.GroupID select i);
                    }
                    else
                        tmpList = new ObservableCollection<Message>((from i in list orderby i.UserName orderby i.GroupID select i).Reverse<Message>());
  
                    break;

            }

           this.msgList= tmpList;

        }
        public void reverse(bool reversed)
        {
            if (reversed)
            {
                msgList = new ObservableCollection<Message>(msgList.Reverse<Message>());
                    this.isReversed = true;
            }
            else
            {
                if(isReversed)
                msgList = new ObservableCollection<Message>(msgList.Reverse<Message>());
                this.isReversed = false;
            }
     

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

    }

}

