using Project2.CommunicationLayer;
using Project2.PersistentLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Project2.BusinessLayer
{
    public class Message : IMessage
    {
        private Guid id;
        public Guid Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }
        private String userName;
        public String UserName
        {
            get
            {
                return userName;
            }
            set
            {
                this.userName = value;
            }

        }
        private DateTime date;
        public DateTime Date
        {
            get
            {
                return this.date;
            }
            set
            {
                this.date = value;
            }
        }
        private String body;
        public String MessageContent
        {
            get
            {
                return this.body;
            }
            set
            {
                this.body = value;
               }
        }
        private String groupID;
        public String GroupID
        {
            get
            {
                return this.groupID;
            }
            set
            {
                this.groupID = value;
            }


        }
        private String toString;
        public String tostring
        {
            get
            {
                return this.toString;
            }
            set
            {
                this.toString = value;
            }
        }
   
        public Message()
        { }
        public Message(IMessage msg)
        {
            this.Id = msg.Id;
            this.GroupID = msg.GroupID;
            this.MessageContent = msg.MessageContent;
            this.UserName = msg.UserName;
            this.Date = msg.Date;
            this.tostring = msg.ToString();
            MessageHandler messageHandler = new MessageHandler();
            messageHandler.SaveNew(this);
        }
        public String ToString()
        {
            return this.tostring;
        }

    }
}
