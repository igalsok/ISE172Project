using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.PresentationLayer
{
    class Gui
    {
        //constructor
        public Gui()
        {
            
        }

        public String FirstMenu()
        {
            Console.WriteLine("To Register press 1");
            Console.WriteLine("To Login press 2");
            Console.WriteLine("To exit press 3");
            return Console.ReadLine();
        }
        public void WrongFirstMenu()
        {
            Console.WriteLine("Not a valid Number");
        }
        public LinkedList<String> Login()
        {
            LinkedList<String> X = new LinkedList<String>();
            Console.WriteLine("Enter Username : ");
            X.AddFirst(Console.ReadLine());
            Console.WriteLine("Enter Group ID : ");
            X.AddFirst(Console.ReadLine());
            return X;
        }
        public LinkedList<String> RegisterMenu()
        {
            LinkedList<String> X = new LinkedList<String>();
            Console.WriteLine("Enter Username : ");
            X.AddFirst(Console.ReadLine());
            Console.WriteLine("Enter Group ID : ");
            X.AddFirst(Console.ReadLine());
            return X;
        }
        public String LoggedMenu(String Username)
        {
            Console.WriteLine("Welcome " + Username);
            Console.WriteLine("To retrieve Messages press 1");
            Console.WriteLine("To display 20 last Messages press 2");
            Console.WriteLine("To display all Messages by specified user press 3");
            Console.WriteLine("To send Messages press 4");
            Console.WriteLine("To logout press 5");
            return Console.ReadLine();
        }
        public void WrongLoggedMenu()
        {
            Console.WriteLine("not a valid option");
        }
        public void NoSuchUsername()
        {
            Console.WriteLine("Such Username or groupID does not exist");
        }
        public void ExistUsername()
        {
            Console.WriteLine("A user with this username already exists");
        }
        public void Registered(String Username)
        {
            Console.WriteLine(Username+", You registered successfully");
        }
        public String Send()
        {
           Console.WriteLine("Please write a message (150 char MAX):");
           return Console.ReadLine();

        }
        public LinkedList<String> DisplayAll()
        {
            LinkedList<String> X = new LinkedList<String>();
            Console.WriteLine("To display all the messages of certain User please:");
            Console.WriteLine("Enter Username : ");
            X.AddFirst(Console.ReadLine());
            Console.WriteLine("Enter Group ID : ");
            X.AddFirst(Console.ReadLine());
            return X;

        }
        public void DisplayMsg(String msg)
            {
            Console.WriteLine(msg);
        }
        public void MessageLimit()
        {
            Console.WriteLine("Message with over than 150 chars can't be sent");
        }

        public void NoMessage()
        {
            Console.WriteLine("Message is empty, please write a message");
        }
    }
}
