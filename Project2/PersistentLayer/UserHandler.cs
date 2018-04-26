using Project2.BusinessLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Project2.PersistentLayer
{
    class UserHandler
    {
        //fields
        private List<User> userList;
        private List<User> users
        {
            get
            {
                return userList;

            }
            set
            {
                this.userList = value;
            }


        }
        private String FilePath
        {
            get;
            set;
        }

        //constructors
        public UserHandler()
        {
            users = new List<User>();
            FilePath = "PersistentLayer/users.xml";
            if (File.Exists(this.FilePath))
            {
                retriveAll();
            }
        }
        //methods
        private List<User> GetList()
        {
            return this.users;
        }
        public void saveNew(User user)  //save new User to the list and saves the updated list to the file
        {
            users.Add(user);
            TextWriter writer = null;
            try
            {
                var serializer = new XmlSerializer(typeof(List<User>));
                writer = new StreamWriter(FilePath, false);
                serializer.Serialize(writer, users);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }
        public void saveNewList(List<User> userList)  //save new User to the list and saves the updated list to the file
        {
            this.users.AddRange(userList);
            TextWriter writer = null;
            try
            {
                var serializer = new XmlSerializer(typeof(List<String>));
                writer = new StreamWriter(FilePath, false);
                serializer.Serialize(writer, users);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }
        private List<User> retriveAll() //retrives from the file
        {
            TextReader reader = null;
            try
            {
                if (!File.Exists(this.FilePath))
                {
                    throw new System.IO.FileNotFoundException("No file on the specified path!");
                }
                var serializer = new XmlSerializer(typeof(List<User>));
                reader = new StreamReader(this.FilePath);
                this.users = (List<User>)serializer.Deserialize(reader);
                return users;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

        }
        public List<User> getAll() //returns the list from the field
        {
            return users;
        }
        public List<User> load() // loads again from the file and saves the new list.
        {
            if (File.Exists(this.FilePath))
            {
                return retriveAll();
            }
            else
            {
                return this.users;
            }
        }
    }
}
