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
        [Required(ErrorMessage ="Заполните поле")]
        public string NameFilm { get; set; }
        [Required]
        public string DescriptionFilm { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public int YearIssue { get; set; }

        [Range(60,240,ErrorMessage ="Продолжительность фильма должна быть 60-240 минут")]
        [Required]
        public int DurationMinutesFilm { get; set; }
        public byte[] Poster { get; set; }
        
        public ICollection<Genre> Genres { get; set; }
        public Film()
        {
            Genres = new List<Genre>();
        }
    }
}