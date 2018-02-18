using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class MovieFormViewModel
    {
        public MovieFormViewModel()
        {
            Id = 0;
        }

        public MovieFormViewModel(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            GenreId = movie.GenreId;
            ReleasedDate = movie.ReleasedDate;
            NumberInStock = movie.NumberInStock;
        }

        public int? Id { get; set; }

        [Required(ErrorMessage = "Title of the movie is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Genre of the movie is required.")]
        public int? GenreId { get; set; }

        [Required(ErrorMessage = "Released date of the movie is required.")]
        public DateTime? ReleasedDate { get; set; }

        [Required(ErrorMessage = "Number in stock of the movie is required.")]
        [Range(1, 20)]
        public int? NumberInStock { get; set; }

        public IEnumerable<Genre> Genres { get; set; }
        public string HeaderString
        {
            get
            {
                if ( Id != 0)
                    return "Edit Movie";

                return "New Movie";
            }
        }
    }
}