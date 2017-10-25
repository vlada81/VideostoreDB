using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideostoreDB.Models;

namespace VideostoreDB.ViewModel
{
    public class MovieGenreViewModel
    {
        public Movie Movie { get; set; }
        public List<Genre> Genres { get; set; }
        public int SelectedGenreId { get; set; }
    }
}