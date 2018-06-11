
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp1
{
    public class UserHandler
    {
        //fields
        private SqlConnection _connection;
        private SqlConnection Connection
        {
            get
            {
                return _connection;

            }
            set
            {
                this._connection = value;
            }


        }
        //constructors
        public UserHandler()
        {
            Connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS01;Initial Catalog=MS3;user id=publicUser;password = isANerd;Trusted_Connection=yes;");
        }
        //methods

        public void addUser(User user)
        {
            if(!isUserExists(user))
            {
                SqlCommand addUser = new SqlCommand("INSERT INTO Users (Group_Id, Nickname, Password) VALUES (@Group_Id, @Nickname, @Password)", Connection);
                // create parameters
                addUser.Parameters.Add("@Group_Id", System.Data.SqlDbType.Int);
                addUser.Parameters.Add("@Nickname", System.Data.SqlDbType.Text);
                addUser.Parameters.Add("@Password", System.Data.SqlDbType.Text);
                // set values to parameters 
                addUser.Parameters["@Group_Id"].Value = user.G_id;
                addUser.Parameters["@Nickname"].Value = user.Username;
                addUser.Parameters["@Password"].Value = user.Password;
                Connection.Open();
                addUser.ExecuteNonQuery();
                Connection.Close();
            }
        }
        public bool isUserExists(User user)
        {
            SqlCommand existCmd = new SqlCommand("SELECT COUNT(*) from Users where Nickname='"+user.Username+ "' AND Group_Id='"+ user.G_id+"';", Connection);
            Connection.Open();
            int userCount = (int)existCmd.ExecuteScalar();
            Connection.Close();
            return userCount != 0;
          

        }
       
    }
}
