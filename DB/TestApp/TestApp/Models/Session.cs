using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestApp.Models
{
    public class Session
    {
        public int IdSession { get; set; }
        public DateTime StartSession { get; set; }
        public int? FilmId {get;set;}
        public Film Film { get; set; }
        public int? HallId { get; set; }
        public Hall Hall { get; set; }
    }
}