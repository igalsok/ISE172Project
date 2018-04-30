using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMS2.PresentationLayer;
using ProjectMS2.PersistentLayer;
using ProjectMS2.CommunicationLayer;


namespace ProjectMS2.BusinessLayer
{
   public class ChatRoom
    {

        public User logged;
        public String url = "http://127.0.0.1";
        public List<Message> msgList;
        public List<User> usersList;
        public Gui gui;
        public log4net.ILog log;
        public MessageHandler MessageHandler;
        public UserHandler UserHandler;

        public ChatRoom(log4net.ILog tmp)
        {
            this.log = tmp;
            this.MessageHandler = new MessageHandler();
            this.UserHandler = new UserHandler();
            this.msgList = this.MessageHandler.getAll();
            this.usersList = this.UserHandler.getAll();
        }
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

        public Boolean Register(String Username,String Gid)
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

        public void Login()
        {
            LinkedList<String> information = this.gui.Login();
            String g_id = information.First.Value;
            String Username = information.Last.Value;
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
                this.gui.NoSuchUsername();
                this.FirstMenu();
            }
            if (!groupIdExists)
            {
                log.Warn("attempt to login with the Username: " + Username + "  ID: " + g_id + " - this ID doesnt match the username");
                this.gui.noSuchId();
                this.FirstMenu();
            }
            else
            {
                this.logged = logging;
                log.Info("User loged in successfully. Username: " + Username + "group id: " + g_id);
                this.Logged();
            }
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
            log.Logger.Repository.Shutdown();
            
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

            }
            else if (message.Length ==0)
            {
                gui.NoMessage();
                this.log.Info(this.logged.Username + "Tried to write an empty message");
            }
            else
            {
                this.logged.Send(message, this.url);

            }
            this.Logged();





        }
        public void DisplayAll() // Display ALL message from a specified user function
        {
            LinkedList<String> information = this.gui.DisplayAll();
            String g_id = information.First.Value;
            String Username = information.Last.Value;
            bool exists = false;
            foreach (Message msg in this.msgList)
            {
                if(msg.UserName.Equals(Username))
                {
                    this.gui.DisplayMsg(msg.tostring);
                    exists = true ;
                }
               
            }
            if (!exists)
            {
                this.gui.noUser();
                this.log.Info("attempt to retrieve messages with wrong userName and GroupID combination:" + Username + " " + g_id);
            }
            this.Logged();
            



        }

    }
}