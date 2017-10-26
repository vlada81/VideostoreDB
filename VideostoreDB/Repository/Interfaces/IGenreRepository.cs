using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideostoreDB.Models;

namespace VideostoreDB.Repository.Interfaces
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetAll();
        Genre GetById(int id);
        bool Create(Genre genre);
        void Update(Genre genre);
        void Delete(int id);
    }
}
