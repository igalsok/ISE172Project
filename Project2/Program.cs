using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project2.BusinessLayer;

namespace Project2

{
    class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
    (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            PresentationLayer.Gui gui = new PresentationLayer.Gui();
            ChatRoom chatroom = new ChatRoom();
            chatroom.Start(gui, log);
            
        }
    }
}
