using System;
using System.Collections.Generic;

#nullable disable

namespace MiniStructorDB
{
    public partial class User
    {
        public User()
        {
            UserClasses = new HashSet<UserClass>();
        }

        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public bool UserIsAdmin { get; set; }

        public virtual ICollection<UserClass> UserClasses { get; set; }
    }
}
