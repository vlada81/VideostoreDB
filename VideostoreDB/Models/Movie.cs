using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace VideostoreDB.Models
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }
        [Required(ErrorMessage = "Enter name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter price")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Enter release year")]
        public int ReleaseDate { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }

    }
}