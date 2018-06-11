
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ConsoleApp1
{
    public class Message 
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
        public  String UserName
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
        private int groupID;
        public int GroupID
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

   
        public Message()
        { }
        public Message(Guid id, int gId, String body, String nickname, DateTime time)
        {
            this.Id = id;
            this.GroupID = gId;
            this.MessageContent = body;
            this.UserName = nickname;
            this.Date = time;
        }
        public override String ToString()
        {
            return "Nickname: "+UserName +" | GroupId: " + GroupID + "\n Send Time: " + Date + "\n     Message: " + MessageContent + "\n";
        }

    }
}
