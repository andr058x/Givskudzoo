using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GivskudZoo.Dal.Test
{
    [TestClass]
    public class NewsTest
    {
        [TestMethod]
        public void GetNews()  
        {
            using (var ctx = new Entities()) {
                var ns = ctx.News;

                foreach(var n in ns)
                {
                    Console.WriteLine(n.Description);
                }
            }
        }

        [TestMethod]
        public void AddNews()
        {
            using (var ctx = new Entities())
            {
                var ns = new News() { Description = "prova2" };

                ctx.News.Add(ns);

                ctx.SaveChanges();
            }
        }
    }
}
