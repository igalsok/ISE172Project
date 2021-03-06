﻿
using ProjectMS2.PersistentLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ProjectMS2.BusinessLayer
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
        private String _toString;

        public String toString
        {
            get { return this._toString; }
            set { this._toString = value;}
            
        }
        public Message(int gId, String body, String nickname)
        {
            this.Id = Guid.NewGuid();
            this.GroupID = gId;
            this.MessageContent = body;
            this.UserName = nickname;
            this.Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            this.toString = "Nickname: " + UserName + " | GroupId: " + GroupID + "\nSend Time: " + Date + "\nMessage: " + MessageContent + "\n";
        }
        public Message(Guid id,int gId, String body, String nickname,DateTime time)
        {
            this.Id = id;
            this.GroupID = gId;
            this.MessageContent = body;
            this.UserName = nickname;
            this.Date = time;
            this.toString = "Nickname: " + UserName + " | GroupId: " + GroupID + "\nSend Time: " + Date + "\nMessage: " + MessageContent + "\n";
        }
        public void changeContent(String content)
        {
            this.MessageContent = content;
        }
        public override String ToString()
        {
            return toString;
        }
      

    }
}
