using ProjectMS2.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProjectMS2.PersistentLayer
{
    public class UserHandler
    {
        //fields
        private SqlConnection _connection;
        private SqlConnection connection
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
            connection = new SqlConnection("Data Source = localhost\\SQLEXPRESS01; Initial Catalog = MS3; user id = publicUser; password = isANerd; Trusted_Connection = yes;Connection Timeout=1; ");
        }
        //methods

        public void addUser(User user)
        {
            if (!isUserExists(user))
            {
                SqlCommand addUser = new SqlCommand("INSERT INTO Users (Group_Id, Nickname, Password) VALUES (@Group_Id, @Nickname, @Password)", connection);
                // create parameters
                addUser.Parameters.Add("@Group_Id", System.Data.SqlDbType.Int);
                addUser.Parameters.Add("@Nickname", System.Data.SqlDbType.Text);
                addUser.Parameters.Add("@Password", System.Data.SqlDbType.Text);
                // set values to parameters 
                addUser.Parameters["@Group_Id"].Value = user.G_id;
                addUser.Parameters["@Nickname"].Value = user.Username;
                addUser.Parameters["@Password"].Value = user.Password;
                connection.Open();
                addUser.ExecuteNonQuery();
                connection.Close();
            }
        }
        public bool isUserExists(User user)
        {
            SqlCommand existCmd = new SqlCommand("SELECT COUNT(*) from Users where Nickname='" + user.Username + "' AND Group_Id='" + user.G_id + "';", connection);
            connection.Open();
            int userCount = (int)existCmd.ExecuteScalar();
            connection.Close();
            return userCount != 0;
        }
        public bool loginValidation(User user)
        {
            SqlCommand existCmd = new SqlCommand("SELECT COUNT(*) from Users where Nickname='" + user.Username + "' AND Group_Id='" + user.G_id + "' AND Password ='" + user.Password + "';", connection);
            connection.Open();
            int userCount = (int)existCmd.ExecuteScalar();
            connection.Close();
            return userCount == 1;
        }
        public void isConnected()
        {
            try
            {
                connection.Open();
                connection.Close();
            }
            catch
            {
                connection.Close();
                throw new Exception("not connected");
            }

        }
    }
}
