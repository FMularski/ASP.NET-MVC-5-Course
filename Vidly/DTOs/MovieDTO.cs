using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.DTOs
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GenreId { get; set; }
        public GenreDTO Genre { get; set; }
        public DateTime ReleasedDate { get; set; }
        public DateTime DateAdded { get; set; }
        public int NumberInStock { get; set; }
    }
}