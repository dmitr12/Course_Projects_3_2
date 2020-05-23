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
        [Required]
        [Display(Name ="Название фильма")]
        public string NameFilm { get; set; }
        [Required]
        [Display(Name ="Описание фильма")]
        public string DescriptionFilm { get; set; }
        [Required]
        [Display(Name ="Страна")]
        public string Country { get; set; }
        [Required]
        [Display(Name ="Год выпуска")]
        public int YearIssue { get; set; }
        [Required]
        [Range(60,240,ErrorMessage ="Продолжительность фильма должна быть 60-240 минут")]
        [Display(Name ="Продолжительность фильма")]
        public int DurationMinutesFilm { get; set; }
        [Display(Name ="Постер")]
        public byte[] Poster { get; set; }
        [Display(Name="Трейлер")]
        public byte[] Trailer { get; set; }
        
        public List<Session> Sessions { get; set; }
        public Film()
        {
            Sessions = new List<Session>();
        }
    }
}