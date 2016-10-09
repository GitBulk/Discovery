using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatRoom.Models
{
    public class UserManager
    {
        public static List<User> Users { get; set; }

        static UserManager()
        {
            Users = new List<User>();
            Users.Add(new User { UserId = 1, UserName = "Toan" });
            Users.Add(new User { UserId = 2, UserName = "Kaka" });
        }
    }
}