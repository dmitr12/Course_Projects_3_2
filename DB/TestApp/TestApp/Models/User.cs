using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestApp.Models
{
    public class User
    {
        public int IdUser { get; set; }
        [Required]
        [Display(Name ="Логин")]
        public string Login { get; set; }
        [Required]
        [Display(Name ="Почта")]
        public string Mail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Пароль")]
        public string Password { get; set; }

        public RoleOfUser RoleOfUser { get; set; }
        public int? RoleOfUserId { get; set; }
    }
}