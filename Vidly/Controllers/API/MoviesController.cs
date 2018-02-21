using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.DTOs;
using Vidly.Models;
using System.Data.Entity;

namespace Vidly.Controllers.API
{
    public class MoviesController : ApiController
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

        public IHttpActionResult GetMovies()
        {
            return Ok(_Context.Movies
                .Include( m => m.Genre)
                .ToList()
                .Select(Mapper.Map<Movie, MovieDTO>));
        }

        public IHttpActionResult GetMovie(int id)
        {
            var movie = _Context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null) NotFound();

            return Ok(Mapper.Map<Movie, MovieDTO>(movie));
        }

        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDTO movieDTO)
        {
            if (!ModelState.IsValid) BadRequest();

            var movie = Mapper.Map<MovieDTO, Movie>(movieDTO);
            _Context.Movies.Add(movie);
            _Context.SaveChanges();

            movieDTO.Id = movie.Id;

            return Created(new Uri(Request.RequestUri + "/" + movieDTO.Id), movieDTO);
        }

        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, MovieDTO movieDTO)
        {
            var movieInDb = _Context.Movies.SingleOrDefault(m => m.Id == id);

            if (movieInDb == null) NotFound();

            if (!ModelState.IsValid) BadRequest();

            Mapper.Map(movieDTO, movieInDb);
            _Context.SaveChanges();

            return Ok(movieDTO);
        }

        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var movieInDb = _Context.Movies.SingleOrDefault(m => m.Id == id);

            if (movieInDb == null) NotFound();

            _Context.Movies.Remove(movieInDb);
            _Context.SaveChanges();

            return Ok();

        }
    }

}
