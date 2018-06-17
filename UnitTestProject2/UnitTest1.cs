using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectMS2.BusinessLayer;
using ProjectMS2.PresentationLayer;

namespace UnitTestProject2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void test_login()
        {
            ChatRoom ch = new ChatRoom();
            ch.Login("TestUser", 101010, "1234");
        }
        [TestMethod]
        public void test_wrongPassRegister()
        {
            ChatRoom ch = new ChatRoom();
            try { ch.Register("TestUser123", 123, "12345678901234567890"); }
            catch { };
        }
 
        [TestMethod]
        public void test_wrongLogin()
        {
            ChatRoom ch = new ChatRoom();
            try { ch.Login("TestUser123", 101010, "1234"); }
            catch { };
            
        } 
        [TestMethod]
        public void test_send()
        {
            ChatRoom ch = new ChatRoom();
            ch.Login("TestUser", 101010, "1234");
            ch.Send("Test Message");
        }
        [TestMethod]
        public void test_sendEmpty()
        {
            ChatRoom ch = new ChatRoom();
            ch.Login("TestUser", 101010, "1234");
            try { ch.Send(""); }
            catch { }
        }
        [TestMethod]
        public void test_sendMax()
        {
            ChatRoom ch = new ChatRoom();
            ch.Login("TestUser", 101010, "1234");
            try { ch.Send("123456789012345678901234567890123456789021235320544567777777666784566564564563456657867856786345876345787847826357867845678456274562356456765463565463546353456"); }
            catch { }
        }
        [TestMethod]
        public void test_logout()
        {
            ChatRoom ch = new ChatRoom();
            ch.Login("TestUser", 101010, "1234");
            ch.logout();
            if (ch.logged != null)
                throw new Exception();
        }
    }
}
