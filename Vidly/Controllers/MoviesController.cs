using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _Context;

        public MoviesController()
        {
            _Context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _Context.Dispose();
        }

        public ViewResult Index()
        {
            var movies = _Context.Movies.Include( m => m.Genre).ToList();

            return View(movies);
        }

        public ActionResult Details( int id)
        {
            var movie = _Context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }

        public ViewResult New()
        {
            var viewModel = new MovieFormViewModel
            {
                Genres = _Context.Genres,
            };

            return View("MovieForm", viewModel);
        }

        public ActionResult Save(MovieFormViewModel viewModel)
        {
            if ( !ModelState.IsValid)
            {
                viewModel.Genres = _Context.Genres;
                return View("MovieForm", viewModel);
            }

            if (viewModel.Id == 0)
            {
                _Context.Movies.Add(new Movie
                {
                    Name = viewModel.Name,
                    ReleasedDate = viewModel.ReleasedDate.Value,
                    GenreId = viewModel.GenreId.Value,
                    NumberInStock = viewModel.NumberInStock.Value
                });
            }
            else
            {
                var movieInDb = _Context.Movies.Single(m => m.Id == viewModel.Id);

                movieInDb.Name = viewModel.Name;
                movieInDb.ReleasedDate = viewModel.ReleasedDate.Value;
                movieInDb.GenreId = viewModel.GenreId.Value;
                movieInDb.NumberInStock = viewModel.NumberInStock.Value;
            }

            _Context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit( int id)
        {
            var movie = _Context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel(movie)
            {
                Genres = _Context.Genres
            };

            return View("MovieForm", viewModel);
        }

    }
}