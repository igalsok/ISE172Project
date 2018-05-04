using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMS2.CommunicationLayer;


namespace ProjectMS2.BusinessLayer
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
        private String g_id;
        public String G_id{
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
        public User(String g_id, String Username)
        {
            this.G_id = g_id;
            this.Username = Username;
           
        }
        public void Send(String msg, String url)
        {
            CommunicationLayer.Communication.Instance.Send(url, this.G_id, this.Username, msg);
        }

    }
}