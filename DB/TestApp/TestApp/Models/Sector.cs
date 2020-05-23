using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestApp.Models
{
    public class Sector
    {
        public int IdSector { get; set; }
        public int? HallId { get; set; }
        public Hall HallOfSector { get; set; }
        [Required]
        public string NameSector { get; set; }
        [Required]
        public int StartRow { get; set; }
        [Required]
        public int EndRow { get; set; }
        [Required]
        public int CountSeatsRow { get; set; }
        [Required]
        public int CostSeat { get; set; }
        public List<Seat> Seats { get; set; }

        public Sector()
        {
            Seats = new List<Seat>();
        }
    }
}