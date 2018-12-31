using System;
using System.ComponentModel;
using System.Web;

namespace GivskudZoo.Web.Models
{
    public class NewsModelInput
    {
        [DisplayName("Title")]
        public string Title { get; set; }

        [DisplayName("Teaser description")]
        public string ShortDescription { get; set; }

        [DisplayName("Upload File")]
        public string Description { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }
    }

    public class NewsModelOutput : NewsModelInput
    {
        public int Id { get; set; }

        public string FileName { get; set; }
    }

    public class NewsDetailsModel : NewsModelInput
    {
        public DateTime CreationDate { get; set; }

        public string Author { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
}