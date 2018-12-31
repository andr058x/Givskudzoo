using System;
using System.Configuration;
using GivskudZoo.Dal;
using System.Linq;
using GivskudZoo.Bll.Utils;

namespace GivskudZoo.Bll
{
    public class UserManager
    {
        private readonly string _encryptKey = ConfigurationManager.AppSettings["EncryptKey"];  

        public bool CheckIfUserExists(string userName, string password)
        {
            var encPassword = SecurityManager.Encrypt(_encryptKey, password);

            using (var ctx = new Entities())
            {
                return ctx.User.Any(x => x.Username == userName && x.Password == encPassword);
            }
        }

        public bool AddUser(string userName, string password)
        {
            var encPassword = SecurityManager.Encrypt(_encryptKey, password);

            using (var ctx = new Entities())
            {
                ctx.User.Add(new User
                {
                    Username = userName,
                    Password = encPassword,
                    CreationDate = DateTime.Now
                });

                return ctx.SaveChanges() > 0;
            }
        }
    }
}
