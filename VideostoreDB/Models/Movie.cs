using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideostoreDB.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Genre Genre { get; set; }
        public Boolean Deleted { get; set; }

    }
}