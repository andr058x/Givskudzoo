using System;
using Newtonsoft.Json;

namespace GivskudZoo.Model
{
    public class ImageDto   //In the folder GivskudZoo we have the models of the web application
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string MimeType { get; set; }
        [JsonIgnore]
        public byte[] Blob { get; set; }
        public int Size { get; set; }

        public string ImageUrl => $"data:image/png;base64,{Convert.ToBase64String(Blob)}";
    }
}