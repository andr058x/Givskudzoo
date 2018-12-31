using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GivskudZoo.Bll.Test
{
    [TestClass]
    public class UserTest
    {
        private UserManager _manager;

        public UserTest()
        {
            _manager = new UserManager();
        }

        [TestMethod]
        public void CreateUser()
        {
            _manager.AddUser("andrea", "andrea");
        }
    }
}
