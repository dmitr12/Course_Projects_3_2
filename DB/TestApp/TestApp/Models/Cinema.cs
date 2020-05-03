using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestApp.Models
{
    public class Cinema
    {
        public int IdCinema { get; set; }

        [Required]
        [Display(Name ="Название кинотеатра")]
        public string NameCinema { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; }
        public List<Hall> Halls { get; set; }

        public Cinema()
        {
            Halls = new List<Hall>();
        }
    }
}