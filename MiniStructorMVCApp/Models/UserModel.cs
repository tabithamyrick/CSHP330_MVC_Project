using MiniStructorDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniStructorMVCApp.Models
{
    public class UserModel : User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
