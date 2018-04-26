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
        static void Main(string[] args)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            PresentationLayer.Gui gui = new PresentationLayer.Gui();
            ChatRoom chatroom = new ChatRoom();
            chatroom.Start(gui, log);
            log.Logger.Repository.Shutdown();
        }
    }
}
