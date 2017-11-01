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
        IGenreRepository genreRepo = new GenreRepo();

        // GET: Movie
        public ActionResult Index()
        {
            var movieList = movieRepo.GetAll();

            return View(movieList);
        }

        public ActionResult Details(int id)
        {
            var movie = movieRepo.GetById(id);

            return View(movie);
        }

        public ActionResult Create()
        {
            var genres = genreRepo.GetAll();

            var movieGenreVM = new MovieGenreViewModel
            {
                Genres = genres
            };

            return View(movieGenreVM);
        }

        [HttpPost]
        public ActionResult Create(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                movieRepo.Create(movie);
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");

        }

        public ActionResult Edit(int id)
        {
            var movie = movieRepo.GetById(id);
            var genres = genreRepo.GetAll();

            var movieGenreVM = new MovieGenreViewModel()
            {
                Movie = movie,
                Genres = genres
            };

            return View(movieGenreVM);
        }

        [HttpPost]
        public ActionResult Edit(Movie movie)
        {
            try
            {
                movieRepo.Update(movie);

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(int id)
        {
            var movie = movieRepo.GetById(id);
            return View(movie);
        }

        [HttpPost]
        public ActionResult Delete(Movie movie)
        {
            try
            {
                movieRepo.Delete(movie.MovieId);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
            
        }
    }
}