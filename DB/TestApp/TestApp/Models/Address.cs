using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestApp.Models
{
    public class Address
    {
        public int IdAddress { get; set; }

        [Required]
        [Display(Name ="Улица")]
        public string Street { get; set; }

        [Required]
        [Display(Name ="Номер дома")]
        [Range(1,int.MaxValue)]
        public int NumberHouse { get; set; }
    }
}