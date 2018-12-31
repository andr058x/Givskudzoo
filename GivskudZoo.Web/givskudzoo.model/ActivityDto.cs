using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GivskudZoo.Model
{
    public class ActivityDto  //In the folder GivskudZoo we have the models of the web application
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string BeginTime => BeginDate.ToString("HH:mm");
        public string EndTime => EndDate.ToString("HH:mm");
    }
}
