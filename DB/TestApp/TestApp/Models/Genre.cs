using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestApp.Models
{
    public class Genre
    {
        public int IdGenre { get; set; }
        [Display(Description ="Жанр")]
        public string NameGenre { get; set; }
        [Display(Description ="Описание")]
        public string DescriptionGenre { get; set; }

        public ICollection<Film> Films { get; set; }
        public Genre()
        {
            Films = new List<Film>();
        }
    }
}