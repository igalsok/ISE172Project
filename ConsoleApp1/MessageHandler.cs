
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace ConsoleApp1
{
    public class MessageHandler
    {

        //fields
        private static int MAX_LIST_SIZE = 5;
        private LinkedList<Message> Messages;
        private LinkedList<Message> messages
        {
            get
            {
                return this.Messages;

            }
            set
            {
                this.Messages = value;
            }
        }
        private SqlConnection _connection;
        public SqlConnection Connection
        {
            get
            {
                return this._connection;
            }
            set
            {
                this._connection = value;
            }
        }

        //constructors
        public MessageHandler()
        {
            Connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS01;Initial Catalog=MS3;user id=publicUser;password = isANerd;Trusted_Connection=yes;");
            messages = new LinkedList<Message>();
        }

        //methods
        public String lastMessageTime(ObservableCollection<Message> ourList )
        {
                return ourList.Last<Message>().Date.ToString("yyyy-MM-dd HH:mm:ss.fff");
            
        }
        public LinkedList<Message> loadMessages(String filterType, bool descending, ObservableCollection<Message> ourList)
        {
                   return loadTimeDes( ourList);
        }
        public LinkedList<Message> loadTimeDes(ObservableCollection<Message> ourList)
        {
            LinkedList<Message> tmpList = new LinkedList<Message>();
            SqlCommand timeDes;
            //sql command creating new table, "join", we have to convert the userID to nickname+gId
            if(ourList.Count!=0)
            timeDes = new SqlCommand("SELECT TOP(" +MAX_LIST_SIZE + ") guid, SendTime, Body, Nickname, Group_Id FROM Messages JOIN Users on[Users].Id = [Messages].User_Id WHERE SendTime > '"+lastMessageTime(ourList)+ "' ORDER BY SendTime desc", Connection);
            else
            timeDes = new SqlCommand("SELECT TOP(" + MAX_LIST_SIZE + ") guid, SendTime, Body, Nickname, Group_Id FROM Messages JOIN Users on[Users].Id = [Messages].User_Id ORDER BY SendTime desc", Connection);
            Connection.Open();
            SqlDataReader rs = timeDes.ExecuteReader();
            if (rs.HasRows) 
            {
                while (rs.Read())
                {
                    Message newMsg = new Message(new Guid(Convert.ToString(rs[0])), Convert.ToInt32(rs[4]), Convert.ToString(rs[2]), Convert.ToString(rs[3]), Convert.ToDateTime(rs[1]));
                    tmpList.AddFirst(newMsg);
                }

            }
            Connection.Close();
            return tmpList;
        }
        public int getUserId(String nickname, int gId)
        {

           SqlCommand getUser = new SqlCommand("select Id from Users where Nickname='"+ nickname + "' AND Group_Id='"+ gId+"';", Connection);
            try
            {
                Connection.Open();
                SqlDataReader rs = getUser.ExecuteReader();
                if (rs.HasRows)
                {
                    while (rs.Read())
                    {

                        return Convert.ToInt32(rs[0]);
                    }

                }
               
                return -1;
            }
            finally
            {
                Connection.Close();
            }

        }
        public void sendMessage(Message msg)
        { 
            SqlCommand sendMessage = new SqlCommand("INSERT INTO Messages (Guid, User_Id, SendTime,Body) VALUES (@Guid, @User_Id,@SendTime, @Body)", Connection);
            // create parameters
            sendMessage.Parameters.Add("@Guid", System.Data.SqlDbType.Text);
            sendMessage.Parameters.Add("@User_Id", System.Data.SqlDbType.Int);
            sendMessage.Parameters.Add("@SendTime", System.Data.SqlDbType.DateTime);
            sendMessage.Parameters.Add("@Body", System.Data.SqlDbType.Text);
            // set values to parameters 
            sendMessage.Parameters["@Guid"].Value = msg.Id.ToString();
            sendMessage.Parameters["@User_Id"].Value = getUserId(msg.UserName,msg.GroupID);
            sendMessage.Parameters["@SendTime"].Value = msg.Date;
            sendMessage.Parameters["@Body"].Value = msg.MessageContent;
            Connection.Open();
            sendMessage.ExecuteNonQuery();
                Connection.Close();
            
        }
    }
}

           