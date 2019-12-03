using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkWeb.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public UserProfile Profile { get; set; }
    }
}
