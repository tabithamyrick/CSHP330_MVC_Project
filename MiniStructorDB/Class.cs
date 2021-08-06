using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable

namespace MiniStructorDB
{
    public partial class Class
    {
        public Class()
        {
            UserClasses = new HashSet<UserClass>();
        }

        public int ClassId { get; set; }
        [DisplayName("Name")]
        public string ClassName { get; set; }
        [DisplayName("Description")]
        public string ClassDescription { get; set; }
        [DisplayName("Price")]
        public decimal ClassPrice { get; set; }

        public virtual ICollection<UserClass> UserClasses { get; set; }
    }
}
