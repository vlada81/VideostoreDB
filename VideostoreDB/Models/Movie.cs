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

        [Required(ErrorMessage = "Please enter movie name")]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter movie price")]
        [RegularExpression(@"^\d*$")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please enter release year, eg. 2017")]
        [RegularExpression(@"\d{4}", ErrorMessage = "Please enter release year, eg. 2017")]
        [Display(Name = "Realese Year")]
        public int ReleaseDate { get; set; }

        [Required(ErrorMessage = "Please select movie genre")]
        public Genre Genre { get; set; }

    }
}