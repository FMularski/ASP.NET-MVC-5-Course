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
                HeaderString = "New Movie"
            };

            return View("MovieForm", viewModel);
        }

        public RedirectToRouteResult Save(MovieFormViewModel viewModel)
        {
            if (viewModel.Movie.Id == 0)
                _Context.Movies.Add(viewModel.Movie);
            else
            {
                var movieInDb = _Context.Movies.Single(m => m.Id == viewModel.Movie.Id);

                movieInDb.Name = viewModel.Movie.Name;
                movieInDb.ReleasedDate = viewModel.Movie.ReleasedDate;
                movieInDb.DateAdded= viewModel.Movie.DateAdded;
                movieInDb.GenreId = viewModel.Movie.GenreId;
                movieInDb.NumberInStock = viewModel.Movie.NumberInStock;
            }

            _Context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit( int id)
        {
            var movie = _Context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel
            {
                Movie = movie,
                Genres = _Context.Genres,
                HeaderString = "Edit Movie"
            };

            return View("MovieForm", viewModel);
        }

    }
}