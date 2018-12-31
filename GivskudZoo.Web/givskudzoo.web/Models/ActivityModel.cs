using System;
using System.Collections.Generic;
using System.ComponentModel;
using GivskudZoo.Model;

namespace GivskudZoo.Web.Models
{
    public abstract class ActivityBaseModel
    {
        public DateTime Date { get; set; }
    }
    public class ActivityDateModel : ActivityBaseModel
    {
    }

    public class ActivityModel : ActivityBaseModel
    {
        public IEnumerable<ActivityDto> List { get; set; }
    }

    public class ActivityModelInput
    {
        [DisplayName("Title")]
        public string Title { get; set; }

        [DisplayName("Location")]
        public string Location { get; set; }

        [DisplayName("Upload File")]
        public string Description { get; set; }

        [DisplayName("Date")]
        public DateTime Date { get; set; }

        [DisplayName("Begin hour")]
        public int BeginHour { get; set; }

        [DisplayName("End hour")]
        public int EndHour { get; set; }

        [DisplayName("Begin minutes")]
        public int BeginMin { get; set; }

        [DisplayName("End minutes")]
        public int EndMin { get; set; }
    }

    public class ActivityModelOutput : ActivityModelInput
    {
        public int Id { get; set; }
    }

    public class ActivityDetailsModel : ActivityModelInput
    {
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}