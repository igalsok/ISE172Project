using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project2.PresentationLayer;
using Project2.PersistentLayer;
using Project2.CommunicationLayer;


namespace Project2.BusinessLayer
{
    class ChatRoom
    {

        public User logged;
        public String url = "http://127.0.0.1";
        public List<Message> msgList;
        public List<User> usersList;
        public Gui gui;
        public log4net.ILog log;
        public MessageHandler MessageHandler;
        public UserHandler UserHandler;

        public void Start(Gui gui, log4net.ILog log)
        {
            //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            this.gui = gui;
            this.log = log;
            this.MessageHandler = new MessageHandler();
            this.UserHandler = new UserHandler();
            this.msgList = this.MessageHandler.getAll();
            this.usersList = this.UserHandler.getAll();
            //this.UsersList=
            FirstMenu();
        }
        public void FirstMenu()
        {
            String select = gui.FirstMenu();
            int numSelect;
            try
            {
                numSelect = (Int32.Parse(select));
            }
            catch
            {
                this.log.Info("user inserted a string which is not a number in the first menu");
                this.gui.WrongFirstMenu();
                this.FirstMenu();
            }
            numSelect = (Int32.Parse(select));
            switch (numSelect)
            {
                case 1:
                    this.Register();
                    break;
                case 2:
                    this.Login();
                    break;
                case 3:
                    this.Exit();
                    break;
                default:
                    this.log.Info("the number selected does not exits in the start menu");
                    this.gui.WrongFirstMenu();
                    this.FirstMenu();
                    break;
            }
        }

        public void Register()
        {
            LinkedList<String> information = this.gui.RegisterMenu();
            String g_id = information.First.Value;
            String Username = information.Last.Value;
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
                this.log.Info("attempt to register with the Username:" + Username + ", a user with thie Username already exists");
                this.gui.ExistUsername();
                this.FirstMenu();
            }
            User registered = new User(g_id, Username);
            this.usersList.Add(registered);
            this.UserHandler.saveNew(registered);
            log.Info("User registered successfully. Username: " + Username + "group id: " + g_id);
            this.gui.Registered(Username);
            FirstMenu();
        }

        public void Login()
        {
            LinkedList<String> information = this.gui.Login();
            String g_id = information.First.Value;
            String Username = information.Last.Value;
            bool UsernameExists = false;
            User logging = null;
            foreach (User user in this.usersList)
            {
                if (!UsernameExists)
                    if (user.Username == Username & user.G_id == g_id)
                    {
                        UsernameExists = true;
                        logging = user;
                    }
            }
            if (!UsernameExists)
            {
                log.Info("attempt to login with the Username: " + Username + ", a user with thie Username doesn't exists");
                this.gui.NoSuchUsername();
                this.FirstMenu();
            }
            this.logged = logging;
            log.Info("User loged in successfully. Username: " + Username + "group id: " + g_id);
            this.Logged();
        }

        public void Logged()
        {
            String tmp = this.gui.LoggedMenu(this.logged.Username);
            int selection;
            try
            {
                selection = (Int32.Parse(tmp));
            }
            catch
            {
                this.log.Info("user inserted a string which is not a number in the LoggedMenu");
                Logged();
                this.FirstMenu();
            }
            selection = (Int32.Parse(tmp));

            switch (selection)
            {
                case 1:
                    this.Retrieve();
                    break;
                case 2:
                    this.Display(20);
                    break;
                case 3:
                    this.DisplayAll();
                    break;
                case 4:
                    this.Send();
                    break;
                case 5:
                    this.logout();
                    break;
                default:
                    this.log.Info("the number selected does not exits in the logged menu");
                    this.gui.WrongLoggedMenu();
                    this.Logged();
                    break;
            }
            Logged();
        }

        public void Retrieve()
        {
            List<IMessage> tmpList = Communication.Instance.GetTenMessages(this.url);
            Console.WriteLine(tmpList.Count());
            foreach (IMessage tmp in tmpList)
            {
                Message tmpMsg = new Message(tmp);
                bool exists = false;
                foreach (Message check in this.msgList)
                {
                    if (tmpMsg.Id == check.Id)
                    {
                        exists = true;
                        break;
                    }
                }
                if(!exists)
                {
                   
                    this.MessageHandler.SaveNew(tmpMsg);
                }

            }
        }
        public void logout()
        {
            log.Info("User: " + this.logged.Username + ", group id:" + this.logged.G_id + ", is logging out");
            this.logged = null;
            FirstMenu();
        }

        public void Exit()
        {
            if (this.logged != null)
            {
                this.logout();
            }
            Environment.Exit(0);
        }
        public void Display(int num)
        {
            foreach (Message tmp in this.msgList)
            {

                if (num > 0)
                {
                    gui.DisplayMsg(tmp.ToString());
                    --num;
                }
            }

        }
        public void Send()
        {
           String message =  this.gui.Send();
            if(message.Length > 150)
            {
                gui.MessageLimit();
                this.log.Info(this.logged.Username + "Tried to write a message over 150 chars");
                this.Send();
            }
            if (message.Length ==0)
            {
                gui.NoMessage();
                this.log.Info(this.logged.Username + "Tried to write an empty message");
                this.Send();
            }

            this.logged.Send(message, this.url);
            
            
        }
        public void DisplayAll() // Display ALL message from a specified user function
        {
            LinkedList<String> information = this.gui.DisplayAll();
            String g_id = information.First.Value;
            String Username = information.Last.Value;
            bool exists = false;
            foreach (User user in this.usersList)
            {
                if (!exists)
                {
                    if (user.Username.Equals(Username) & user.G_id.Equals(g_id))
                        {
                        foreach(Message msg in this.msgList)
                        {
                            if(msg.UserName == Username)
                            {
                                Console.WriteLine(msg.ToString());
                            }
                        }

                    }
                        
                }
            }
            if (exists)
            {
                this.log.Info("attempt to retrieve with wrong Username:" + Username);
                this.DisplayAll();
            }
            this.Logged();
            



        }

    }
}