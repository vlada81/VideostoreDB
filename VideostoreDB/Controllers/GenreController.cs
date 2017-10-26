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
            if (genreRepo.Create(genre))
            {
                return RedirectToAction("Index");
            }

            return View("Error");
        }

        public ActionResult Delete(int id)
        {
            genreRepo.Delete(id);
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection form)
        {
            int genreId = int.Parse(Request.Form["Name"]);
            genreRepo.Delete(genreId);
            
            return RedirectToAction("Index");
        }
    }
}