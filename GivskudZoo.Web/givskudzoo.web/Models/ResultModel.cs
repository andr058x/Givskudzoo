namespace GivskudZoo.Web.Models
{
    public class ResultModel
    {
        public bool Success => string.IsNullOrEmpty(Error);

        public string Error { get; set; }
    }
}