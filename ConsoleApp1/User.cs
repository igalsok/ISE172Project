using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp1
{
    public class User
    {
        private String username;
        public String Username{
            get
            {
                return username;
            }
            set
            {
                this.username = value;
            }
        }
        private String _password;
        public String Password
        {
            get
            {
                return _password;
            }
            set
            {
                this._password = value;
            }
        }
        private int g_id;
        public int G_id{
            get
            {
                return g_id;
            }
            set
            {
               this.g_id = value;
                
            }
        }

        public User()
        {

        }
        public User(int g_id, String Username,String pass)
        {
            this.G_id = g_id;
            this.Username = Username;
            this.Password = pass;
        }
    }
}