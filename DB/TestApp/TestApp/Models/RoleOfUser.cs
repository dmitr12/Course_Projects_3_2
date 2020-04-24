using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestApp.Models
{
    public class RoleOfUser
    {
        public int IdRole { get; set; }
        public string NameRole { get; set; }
        public string NameConnection { get; set; }
        public ICollection<User> Users { get; set; }
        public RoleOfUser()
        {
            Users = new List<User>();
        }
    }
}