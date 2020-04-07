using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestApp.Models
{
    public class Film
    {
        public int IdFilm { get; set; }
        public string NameFilm { get; set; }
        public string Country { get; set; }
        public DateTime DateIssue { get; set; }
        public string Genre { get; set; }
        public int DurationMinutesFilm { get; set; }
        public byte[] Poster { get; set; }
    }
}