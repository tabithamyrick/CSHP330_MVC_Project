using System.ComponentModel.DataAnnotations;

namespace MiniStructorMVCApp.Models
{
    public class UserLogin
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "User Email")]
        public string UserEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }


        public string? returnUrl { get; set; }
    }
}
