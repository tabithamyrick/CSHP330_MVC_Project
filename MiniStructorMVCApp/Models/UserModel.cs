using MiniStructorDB;

namespace MiniStructorMVCApp.Models
{
    public class UserModel : User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
