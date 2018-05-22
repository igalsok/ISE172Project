using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectMS2.Testers;
namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void NotRegisteredLogin()
        {
            Testers tester = new Testers();
            tester.testNotRegisteredLogin();
        }
    }
}
