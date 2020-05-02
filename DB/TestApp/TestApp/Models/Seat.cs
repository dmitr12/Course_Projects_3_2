using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestApp.Models
{
    public class Seat
    {
        public int IdSeat { get; set; }
        [Display(Name ="Место")]
        public int NumberSeat { get; set; }
        public Sector Sector { get; set; }
        public int SectorId { get; set; }
    }
}