using ProjectMS2.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace ProjectMS2.PersistentLayer
{
    public class MessageHandler
    {

        //fields
        public static int MAX_LIST_SIZE = 200;
        private SqlConnection _connection;
        private SqlConnection Connection
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
            //connection string
            Connection = new SqlConnection("Data Source = ise172.ise.bgu.ac.il,1433\\DB_LAB; Initial Catalog = MS3; user id = publicUser; password = isANerd; Connection Timeout=1; ");
        }

        //methods
        public String lastMessageTime(ObservableCollection<Message> ourList)
        {
            return ourList.Max(m => m.Date).ToString("yyyy-MM-dd HH:mm:ss.fff");
        }

        //filter types: 'time' 'username' 'group_Id'
        public LinkedList<Message> loadMessages(String sortType, bool descending, ObservableCollection<Message> ourList)
        {
            if(sortType.Equals("SendTime") | sortType.Equals("Nickname")| sortType.Equals("Group_Id"))
            {
            LinkedList<Message> tmpList = new LinkedList<Message>();
            SqlCommand timeDes;
            //sql command creating new table, "join", we have to convert the userID to nickname+gId
            if (ourList.Count != 0)
                timeDes = new SqlCommand("SELECT TOP(" + MAX_LIST_SIZE + ") guid, SendTime, Body, Nickname, Group_Id FROM Messages JOIN Users on[Users].Id = [Messages].User_Id WHERE SendTime > '" + lastMessageTime(ourList) + "' ORDER BY '"+ sortType +"' desc", Connection);
            else
                timeDes = new SqlCommand("SELECT TOP(" + MAX_LIST_SIZE + ") guid, SendTime, Body, Nickname, Group_Id FROM Messages JOIN Users on[Users].Id = [Messages].User_Id ORDER BY '" +sortType+"' desc", Connection);
                try
                {
                    Connection.Open();
                    SqlDataReader rs = timeDes.ExecuteReader();
                    if (rs.HasRows)
                    {
                        while (rs.Read())
                        {
                            Message newMsg = new Message(new Guid(Convert.ToString(rs[0])), Convert.ToInt32(rs[4]), Convert.ToString(rs[2]).TrimEnd(), Convert.ToString(rs[3]).TrimEnd(), Convert.ToDateTime(rs[1]));
                            if (descending)
                                tmpList.AddFirst(newMsg); //descending adding the last message to the end of the list. 
                            else
                            {
                                tmpList.AddLast(newMsg);
                            }
                        }

                    }
                    Connection.Close();
                }
                catch (Exception)
                {
                    throw new Exception("not connected to the server");
                }
                
            
            return tmpList;
            }
            else
                throw new Exception("filter type syntax is wrong. should be: SendTime/Nickname/Group_Id");
                   
        }

        public LinkedList<Message> loadMessages(String sortType, bool descending, ObservableCollection<Message> ourList, String gIdFilter, String nickFilter)
        {
            if (sortType.Equals("SendTime") | sortType.Equals("Nickname") | sortType.Equals("Group_Id"))
            {
                LinkedList<Message> tmpList = new LinkedList<Message>();
                SqlCommand timeDes;
                //sql command creating new table, "join", we have to convert the userID to nickname+gId
                if(!nickFilter.Equals(string.Empty))
                { 
                if (ourList.Count != 0)
                    timeDes = new SqlCommand("SELECT TOP(" + MAX_LIST_SIZE + ") guid, SendTime, Body, Nickname, Group_Id FROM Messages JOIN Users on[Users].Id = [Messages].User_Id WHERE Nickname = '" + nickFilter + "' AND Group_id = '"+gIdFilter+ "'AND SendTime > '" + lastMessageTime(ourList) + "' ORDER BY '" + sortType + "' desc", Connection);
                else
                    timeDes = new SqlCommand("SELECT TOP(" + MAX_LIST_SIZE + ") guid, SendTime, Body, Nickname, Group_Id FROM Messages JOIN Users on[Users].Id = [Messages].User_Id WHERE Nickname = '" + nickFilter + "' AND Group_id = '" + gIdFilter + "' ORDER BY '" + sortType + "' desc", Connection);
                }
                else
                {
                    if (ourList.Count != 0)
                        timeDes = new SqlCommand("SELECT TOP(" + MAX_LIST_SIZE + ") guid, SendTime, Body, Nickname, Group_Id FROM Messages JOIN Users on[Users].Id = [Messages].User_Id WHERE Group_id = '" + gIdFilter + "'AND SendTime > '" + lastMessageTime(ourList) + "' ORDER BY '" + sortType + "' desc", Connection);
                    else
                        timeDes = new SqlCommand("SELECT TOP(" + MAX_LIST_SIZE + ") guid, SendTime, Body, Nickname, Group_Id FROM Messages JOIN Users on[Users].Id = [Messages].User_Id WHERE Group_id = '" + gIdFilter + "' ORDER BY '" + sortType + "' desc", Connection);
                }
                try
                {
                    Connection.Open();
                    SqlDataReader rs = timeDes.ExecuteReader();
                    if (rs.HasRows)
                    {
                        while (rs.Read())
                        {
                            Message newMsg = new Message(new Guid(Convert.ToString(rs[0])), Convert.ToInt32(rs[4]), Convert.ToString(rs[2]).TrimEnd(), Convert.ToString(rs[3]).TrimEnd(), Convert.ToDateTime(rs[1]));
                            if (descending)
                                tmpList.AddFirst(newMsg); //descending adding the last message to the end of the list. 
                            else
                            {
                                tmpList.AddLast(newMsg);
                            }
                        }

                    }
                    Connection.Close();
                }
                catch
                {
                    throw new Exception();
                }
                return tmpList;
            }
            else
                throw new Exception("sort type syntax is wrong. should be: SendTime/Nickname/Group_Id");
    }

        private int getUserId(String nickname, int gId)
        {
            SqlCommand getUser = new SqlCommand("select Id from Users where Nickname='"+ nickname + "' AND Group_Id='"+ gId+"';", Connection);
            Connection.Open();
            SqlDataReader rs = getUser.ExecuteReader();
            if (rs.HasRows)
            {
                while (rs.Read())
                {
                    int tmp = Convert.ToInt32(rs[0]);
                    Connection.Close();
                    return tmp;
                }

            }
            Connection.Close();
            return -1;

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

        public void EditMessage(Message msg)
        {
            SqlCommand editCommand = new SqlCommand("UPDATE Messages SET Body = '"+msg.MessageContent+"' WHERE Guid = '" +msg.Id +"';",Connection);
            Connection.Open();
            editCommand.ExecuteNonQuery();
            Connection.Close();

        }
    }
}

           