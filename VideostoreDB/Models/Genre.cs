using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace VideostoreDB.Models
{
    public class Genre
    {
        [Key]
        [Required(ErrorMessage = "Please select movie genre")]
        public int GenreId { get; set; }
        [Required(ErrorMessage = "Please enter name")]
        public string GenreName { get; set; }

    }
}