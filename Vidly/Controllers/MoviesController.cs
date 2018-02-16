using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        public ActionResult Index()
        {
            var movies = new List<Movie>
            {
                new Movie { Id = 1, Name = "Shrek"},
                new Movie { Id = 2, Name = "Wall-e"}
            };

            return View(movies);
        }

        public ViewResult Random()
        {
            var movie = new Movie { Name = "Shrek!" };

            var customers = new List<Customer>
            {
                new Customer { Name = "Customer 1"},
                new Customer { Name = "Customer 2"},
                new Customer { Name = "Customer 3"},
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };


            return View(viewModel);
        }

        public ContentResult Edit( int id)
        {
            return Content("id = " + id);
        }

        public ContentResult Params( int? pageIndex, string sortBy)
        {
            if (!pageIndex.HasValue)
                pageIndex = 1;
            if (String.IsNullOrEmpty(sortBy))
                sortBy = "Name";

            return Content(String.Format("pageIndex = {0}, sortBy = {1}", pageIndex, sortBy));
        }

        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1, 12)}")]
        public ContentResult Released( int year, int month)
        {
            return Content(String.Format("year = {0} month = {1}", year, month));
        }
    }
}