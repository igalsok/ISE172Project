using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMS2.PresentationLayer;
using ProjectMS2.PersistentLayer;
using System.Timers;
using System.Collections.ObjectModel;
using System.Windows;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Threading;
using System.Text.RegularExpressions;
using System.Collections.Specialized;

namespace ProjectMS2.BusinessLayer
{
    public class ChatRoom : INotifyPropertyChanged
    {
        #region Fields/Properties
        private User _logged;
        public User logged
        {
            get { return this._logged; }
            set { this._logged = value; }
        }

        private ObservableCollection<Message> _DisplayList;
        public ObservableCollection<Message> DisplayList
        {
            get { return _DisplayList; }
            set
            {
                this._DisplayList = value;
                NotifyPropertyChanged("DisplayList");
            }
        }
        private log4net.ILog log;
        private MessageHandler _messageHandler;
        public MessageHandler messageHandler
        {
            get { return this._messageHandler; }
            set { this._messageHandler = value; }
        }
        private UserHandler _userHandler;
        public UserHandler userHandler
        {
            get { return this._userHandler; }
            set { this._userHandler = value; }
        }
        private bool _descending;
        public bool descending
        {
            get { return this._descending; }
            set { this._descending = value; }
        }

        #endregion
        #region constructors
        public ChatRoom(log4net.ILog tmp)
        {
            this.log = tmp;
            this.messageHandler = new MessageHandler();
            this.userHandler = new UserHandler();
            this.descending = true;
            this.DisplayList = new ObservableCollection<Message>();
            try
            {
                Retrieve("SendTime", string.Empty, string.Empty);
            }
            catch
            {

            }
        }
        public ChatRoom()
        {
            this.messageHandler = new MessageHandler();
            this.userHandler = new UserHandler();
            this.descending = true;
            this.DisplayList = new ObservableCollection<Message>();
            try
            {
                Retrieve("SendTime", string.Empty, string.Empty);
            }
            catch
            {

            }
        }
        #endregion
        #region firstMenu
        public void Register(String Username, int Gid, String Password)
        {
            //checking pass length
            if (Password.Length < 4 || Password.Length > 16)
                throw new InvalidOperationException("Password length should be in range of 4-16");
            //checking if only latters and numbers
            bool valid = Password.All(c => char.IsLetterOrDigit(c));
            if (!valid)
                throw new InvalidOperationException("A valid password should contain only letters and numbers");
            User tmpUser = new User(Gid, Username, Hash.GetHashString(Password + "1337")); //creating tmp user
            if (userHandler.isUserExists(tmpUser)) // checking if this user is already exists
            {
                if (log != null)
                    this.log.Warn("attempt to register with the Username:" + Username + ", G-ID: " + Gid + ".a user with this Username and G-ID already exists");
                throw new InvalidOperationException("Username and GroupId already taken");
            }
            else
            {
                userHandler.addUser(tmpUser); // adding user to database
                if (log != null)
                    log.Info("User registered successfully. Username: " + Username + "group id: " + Gid);
            }


        }

        public void Login(String Username, int g_id, String Password)
        {
            User tmpUser = new User(g_id, Username, Hash.GetHashString(Password + "1337"));
            if (userHandler.loginValidation(tmpUser))
            {
                this.logged = tmpUser;
                if (log != null)
                    log.Info("User logged in successfully. Username: " + Username + "group id: " + g_id);
            }
            else
            {
                if (log != null)
                    log.Warn("attempt to login with the Username: " + Username + " and G-ID: " + g_id + ", a user with this Username and ID doesn't exist");
                throw new InvalidOperationException("Nickname or Password are not valid");
            }
        }
        public void Exit()
        {
            log.Logger.Repository.Shutdown();
        }
        #endregion
        #region chatroomMenu
        public void Retrieve(String sortType, String gIdFilter, String nickFilter)
        {

            LinkedList<Message> tmpList = null;
            //trying to load new messages from the server. 2 methods from messages handler. 1- if no filter, 2- with filter
            try
            {
                if (gIdFilter.Equals(String.Empty))
                    tmpList = new LinkedList<Message>(messageHandler.loadMessages(sortType, descending, this._DisplayList));
                else
                    tmpList = new LinkedList<Message>(messageHandler.loadMessages(sortType, descending, this._DisplayList, gIdFilter, nickFilter));
            }
            catch
            {
                if (log != null)
                    log.Error("The client has lost it's connection to the server");
                throw new Exception("Connection to the the server has been lost");
            }
            int overloadCount = tmpList.Count(); //checking if our list is full and if so, removing the first messages.
            if (DisplayList.Count == MessageHandler.MAX_LIST_SIZE)
            {
                while (overloadCount > 0)
                {
                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                     new Action(() =>
                     {
                         this.DisplayList.RemoveAt(DisplayList.IndexOf(DisplayList.OrderBy(m => m.Date).FirstOrDefault<Message>()));
                     }));
                    overloadCount--;
                }
            }
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
             {
                 if (descending) //adding messages by descending
                 {
                     foreach (Message msg in tmpList.ToList<Message>())
                     {
                         this.DisplayList.Add(msg);
                     }
                 }
                 else
                 {
                     foreach (Message msg in tmpList.ToList<Message>()) //adding messages by ascending
                     {
                         DisplayList.Insert(0, msg);
                     }
                 }
                 if (tmpList.Count > 0)
                     sortTypeChanged(sortType); //we want the list keep updating and not losing it's sorting.
            }));


        }
        public void emptyDisplayList()
        {
            this.DisplayList = new ObservableCollection<Message>();
        }
        public void logout()
        {
            if (log != null)
                log.Info("User: " + this.logged.Username + ", group id:" + this.logged.G_id + ", is logging out");
            this.logged = null;
        }
        public void Send(String Msg)
        {
            if (Msg.Length > 100)
            {
                if (log != null)
                    log.Warn("User: " + this.logged.Username + ", group id:" + this.logged.G_id + ",try to send over 100 length message");
                throw new Exception("message over 150 chars");
            }
            else if (Msg.Length == 0)
            {
                if (log != null)
                    log.Warn("User: " + this.logged.Username + ", group id:" + this.logged.G_id + ", tried to send an empty message");
                throw new Exception("empty message");
            }
            else
            {
                Message msg = new Message(logged.G_id, Msg, logged.Username);
                this.messageHandler.sendMessage(msg);
                if (log != null)
                    log.Info("User: " + this.logged.Username + ", group id:" + this.logged.G_id + ", sent a message");
            }
        }
        public void reverse()
        {

            descending = !descending;
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
           new Action(() =>
           {
               this.DisplayList = new ObservableCollection<Message>(DisplayList.Reverse<Message>());

           }));
        }
        public void sortTypeChanged(String sortType)
        {
            if (descending)
            {
                if (sortType.Equals("SendTime"))
                    DisplayList = new ObservableCollection<Message>(from i in DisplayList orderby i.Date select i);
                if (sortType.Equals("Nickname"))
                    DisplayList = new ObservableCollection<Message>(from i in DisplayList orderby i.Date orderby i.UserName select i);
                if (sortType.Equals("Group_Id"))
                    DisplayList = new ObservableCollection<Message>(from i in DisplayList orderby i.Date orderby i.UserName orderby i.GroupID select i);
            }
            else
            {
                if (sortType.Equals("SendTime"))
                    DisplayList = new ObservableCollection<Message>((from i in DisplayList orderby i.Date select i).Reverse<Message>());
                if (sortType.Equals("Nickname"))
                    DisplayList = new ObservableCollection<Message>((from i in DisplayList orderby i.Date orderby i.GroupID orderby i.UserName select i).Reverse<Message>());
                if (sortType.Equals("Group_Id"))
                    DisplayList = new ObservableCollection<Message>((from i in DisplayList orderby i.Date orderby i.UserName orderby i.GroupID select i).Reverse<Message>());
            }
        }
        public bool canEdit(Message editMsg)
        {
            //checking if the speciefic user are trying to edit it's own message.
            return editMsg.GroupID.Equals(logged.G_id) && editMsg.UserName.Equals(logged.Username);
        }
        public void editMessage(Message msg, String sortType, String GIdFilter, String NickFilter)
        {
            //editing the message and updating it in the display list.
            messageHandler.EditMessage(msg);
            Message message = new Message(msg.Id, msg.GroupID, msg.MessageContent, msg.UserName, msg.Date);
            DisplayList[DisplayList.IndexOf(msg)] = message;
            if (log != null)
                log.Warn("User: " + this.logged.Username + ", group id:" + this.logged.G_id + "edited it's message - Message Id: " + msg.Id);
        }
        public bool isConnected()
        {
            try
            {
                userHandler.isConnected();
                return true;
            }
            catch
            {
                if (log != null)
                    log.Error("The client has lost it's connection to the server");
                return false;
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

