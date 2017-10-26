using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideostoreDB.Models;
using VideostoreDB.ViewModel;
using VideostoreDB.Repository;
using VideostoreDB.Repository.Interfaces;

namespace VideostoreDB.Controllers
{
    public class MovieController : Controller
    {
        IMovieRepository movieRepo = new MovieRepo();
        IMovieGenreVMRepository movieGenreVMRepo = new MovieGenreVMRepo();

        // GET: Movie
        public ActionResult Index()
        {
            var movieList = movieRepo.GetAll();

            return View(movieList);
        }

        public ActionResult Details(int id)
        {
            var movieGenreVM = movieGenreVMRepo.GetById(id);

            return View(movieGenreVM);
        }

        public ActionResult Edit(int id)
        {
            var movieGenreVM = movieGenreVMRepo.GetById(id);

            return View(movieGenreVM);
        }
    }
}