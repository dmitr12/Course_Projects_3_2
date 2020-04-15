using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestApp.Models
{
    public class Hall
    {
        public int IdHall { get; set; }
        [Required]
        public string NameHall { get; set; }
        public int? CinemaId { get; set; }
        public Cinema Cinema { get; set; }
    }
}