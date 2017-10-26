using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideostoreDB.Models;
using VideostoreDB.ViewModel;

namespace VideostoreDB.Repository.Interfaces
{
    interface IMovieGenreVMRepository
    {
        IEnumerable<MovieGenreViewModel> GetAll();
        MovieGenreViewModel GetById(int id);
        bool Create(MovieGenreViewModel movieGenreVM);
        void Update(MovieGenreViewModel movieGenreVM);
        void Delete(int id);
    }
}
