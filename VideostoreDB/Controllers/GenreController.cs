using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideostoreDB.Models;
using VideostoreDB.Repository;
using VideostoreDB.Repository.Interfaces;

namespace VideostoreDB.Controllers
{
    public class GenreController : Controller
    {
        private IGenreRepository genreRepo = new GenreRepo();

        // GET: Genre
        public ActionResult Index()
        {
            var genreList = genreRepo.GetAll();

            return View(genreList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return View(genre);
            }

            if (genreRepo.Create(genre))
            {
                return RedirectToAction("Index");
            }

            return View("Error");
        }

        public ActionResult Edit(int id)
        {
            var genre = genreRepo.GetById(id);

            return View(genre);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection form)
        {
            var genre = new Genre();

            try
            {
                UpdateModel(genre);

                genreRepo.Update(genre);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(genre);
            }
        }

        public ActionResult Delete(int id)
        {
            var genre = genreRepo.GetById(id);
            return View(genre);
        }

        [HttpPost]
        public ActionResult Delete(Genre genre)
        {
            try
            {
                genreRepo.Delete(genre.GenreId);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return RedirectToAction("Index");
            }
        }

        
        
    }
}