using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideostoreDB.Models;
using VideostoreDB.ViewModel;

namespace VideostoreDB.Repository.Interfaces
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetAll();
        Movie GetById(int id);
        bool Create(Movie movie);
        void Update(Movie movie);
        void Delete(int id);
    }
}
