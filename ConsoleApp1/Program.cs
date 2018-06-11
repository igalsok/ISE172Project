using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
      
        static void Main(string[] args)
        {
            MessageHandler handler = new MessageHandler();
            ObservableCollection<Message> col = new ObservableCollection<Message>();
            Message msg = new Message(Guid.NewGuid(),123,"fourth", "igal",new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second));
            // handler.sendMessage(msg);
            handler.loadMessages("time", true, col).ToList<Message>().ForEach(col.Add);
            foreach(Message msg2 in col)
            {
                Console.WriteLine(msg2); 
            }
            Console.WriteLine(col.Max(m => m.Date));
            String s;
            s = Console.ReadLine();
            Message msg3 = new Message(Guid.NewGuid(),123,s,"igal", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second));
            handler.sendMessage(msg3);
            handler.loadMessages("time", true, col).ToList<Message>().ForEach(col.Add);
            if(col.Count>5)
            {
                col.RemoveAt(0);
            }
            foreach (Message msg2 in col)
            {
                Console.WriteLine(msg2);
            }
            Console.ReadLine();

        }
    }
}
