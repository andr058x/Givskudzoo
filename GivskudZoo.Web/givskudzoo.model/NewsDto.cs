using System;
using System.IO;

namespace GivskudZoo.Model
{
    public class NewsDto   //In the folder GivskudZoo we have the models of the web application
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public int? ImageId { get; set; }
        public bool DeleteImage { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdateDate { get; set; }

        public ImageDto Image { get; set; }
    }
}