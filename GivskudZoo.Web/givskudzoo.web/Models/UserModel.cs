using System.ComponentModel.DataAnnotations;

namespace GivskudZoo.Web.Models
{
    public class UserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}