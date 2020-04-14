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
        public string Street { get; set; }

        [Required]
        public int NumberHouse { get; set; }
    }
}