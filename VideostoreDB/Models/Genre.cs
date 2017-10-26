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
        public int GenreId { get; set; }
        [Required(ErrorMessage = "Enter name")]
        public string Name { get; set; }

    }
}