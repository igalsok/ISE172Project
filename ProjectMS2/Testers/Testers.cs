using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMS2.BusinessLayer;
using ProjectMS2.PersistentLayer;

namespace ProjectMS2.Testers
{
    class Testers
    {
        private ChatRoom ch;
        private UserHandler uh;
        

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
  (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public Testers()
        {
            ch = new ChatRoom(log);
            uh = new UserHandler();
        }

        public Boolean testEmpty()
        {
            ch.Register("tester", "123");
            ch.Login("tester", "123");
            int i = ch.Send(String.Empty);
            ch.logout();
            List<User> list = ch.UserHandler.getAll();
            foreach (User usr in list)
            {
                if (usr.Username.Equals("tester") && usr.G_id.Equals("123"))
                {
                    list.Remove(usr);
                    ch.UserHandler.saveNewList(list);
                    break;
                }
            }
            if (i == 2)
            {
                return true;
            } 
           return false;
        }

        public Boolean testOver()
        {
            ch.Register("tester", "123");
            ch.Login("tester", "123");
            String OverString = "";
            for(int j = 0; j < 151; j++)
            {
                OverString += 'a';
            }
            int i = ch.Send(OverString);
            ch.logout();
            List<User> list = ch.UserHandler.getAll();
            foreach (User usr in list)
            {
                if (usr.Username.Equals("tester") && usr.G_id.Equals("123"))
                {
                    list.Remove(usr);
                    ch.UserHandler.saveNewList(list);
                    break;
                }
            }
            if (i == 1)
            {
                return true;
            }
            return false;
        }

        public Boolean testNotRegisteredLogin()
        {
            List<User> list = ch.UserHandler.getAll();
            foreach (User usr in list)
            {
                if (usr.Username.Equals("tester") && usr.G_id.Equals("123"))
                {
                    list.Remove(usr);
                    ch.UserHandler.saveNewList(list);
                    break;
                }
            }
            if (!ch.Login("tester", "123"))
                return true;
            return false;
        }

        public Boolean testLogout()
        {
            ch.Register("tester", "123");
            ch.Login("tester", "123");
            ch.logout();
            List<User> list = ch.UserHandler.getAll();
            foreach (User usr in list)
            {
                if (usr.Username.Equals("tester") && usr.G_id.Equals("123"))
                {
                    list.Remove(usr);
                    ch.UserHandler.saveNewList(list);
                    return false;
                }
            }
            return true;
        }

        public Boolean testSavedUser()
        {
            String UN = "tester";
            String GID = "123";
            ch.Register(UN,GID);
            User user = new User(GID, UN);
            uh.saveNew(user);
            List<User> CheckList = uh.RetriveAll();
            List<User> list = ch.UserHandler.getAll();
            if (CheckList.Contains(user))
            {
                ch.logout();
                list.Remove(user);
                ch.UserHandler.saveNewList(list);
                return true;
            }
            return false;
        }

        public Boolean testLoginTwice()
        {
            String UN = "tester";
            String GID = "123";
            ch.Register(UN, GID);
            ch.Login(UN, GID);
            if (!ch.Login(UN, GID))
                return true;
            return false;
        }


    }
}

