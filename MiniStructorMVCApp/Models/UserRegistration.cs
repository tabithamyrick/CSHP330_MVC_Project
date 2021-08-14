using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniStructorMVCApp.Models
{
    public class UserRegestration
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "User Email")]
        public string UserEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string UserPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("UserPassword")]
        [NotMapped]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        public string? returnUrl { get; set; }
    }
}
