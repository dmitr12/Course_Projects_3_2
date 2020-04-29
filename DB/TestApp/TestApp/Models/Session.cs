using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestApp.Models
{
    public class Session
    {
        public int IdSession { get; set; }
        [Required]
        [Display(Name ="Начало проведения сеанса")]
        [DataType(DataType.DateTime)]
        public DateTime StartSession { get; set; }
        [Required]
        [Display(Name ="Фильм")]
        public int? FilmId {get;set;}
        public Film Film { get; set; }
        [Required]
        [Display(Name ="Зал")]
        public int? HallId { get; set; }
        public Hall Hall { get; set; }
    }
}