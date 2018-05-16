using ProjectMS2.BusinessLayer;
using ProjectMS2.CommunicationLayer;
using System;
using System.Collections.Generic;
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
        private List<Message> Messages;
        private List<Message> messages
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
        private String FilePath;
        private String filePath
        {
            get
            {
              return this.FilePath;
            }
            set
            {
                this.FilePath = value;
            }
        }


        //constructors
        public MessageHandler()
        {
            messages = new List<Message>();
            filePath = "PersistentLayer/messages.xml";
            if (File.Exists(this.filePath))
            {
                retriveAll();
            }

        }
        //methods
        private List<Message> GetList()
        {
            return this.messages;
        }
        public void SaveNew(Message msg) // save new Message to the list and saves the updated list to the file
        {
            this.Messages.Add(msg);
            TextWriter writer = null;
            try
            {
                var serializer = new XmlSerializer(typeof(List<Message>));
                writer = new StreamWriter(filePath, false);
                serializer.Serialize(writer, messages);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }
        public void SaveNewList(List<Message> msgList) // save new Message to the list and saves the updated list to the file
        {
            this.messages.AddRange(msgList);
            TextWriter writer = null;
            try
            {
                var serializer = new XmlSerializer(typeof(List<IMessage>));
                writer = new StreamWriter(filePath, false);
                serializer.Serialize(writer, messages);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }
        private List<Message> retriveAll() //retrives from the file
        {
            TextReader reader = null;
            try
            {
                if(!File.Exists(this.filePath))
                {
                    throw new System.IO.FileNotFoundException("No file on the specified path!");
                }
                var serializer = new XmlSerializer(typeof(List<Message>));
                reader = new StreamReader(this.filePath);
                this.Messages = (List<Message>)serializer.Deserialize(reader);
                return Messages;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
        public List<Message> getAll() // return the list from the field
        {
            return messages;
        }
        public List<Message> load() //loads again from the file and saves the new list.
        {
            if(File.Exists(this.FilePath))
            {
                return retriveAll();
            }
            else
            {
                return this.messages;
            }
        }
        
    }
}