namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Vidly.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Vidly.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Vidly.Models.ApplicationDbContext context)
        {
            context.Customers.AddOrUpdate(c => c.Name,
            new Customer { Name = "John Smith", IsSubscribedToNewsletter = false, MembershipTypeId = 1, BirthDate = new DateTime(1996, 3, 28).Date },
            new Customer { Name = "Mary Williams", IsSubscribedToNewsletter = true, MembershipTypeId = 2 });

            context.Genres.AddOrUpdate(g => g.Name,
                new Genre { Name = "Comedy" },
                new Genre { Name = "Action" },
                new Genre { Name = "Family" },
                new Genre { Name = "Romance" },
                new Genre { Name = "Sci-Fi" });

            context.Movies.AddOrUpdate(m => m.Name,
                new Movie { Name = "Hangover", GenreId = 1, NumberInStock = 5, ReleasedDate = DateTime.Now, DateAdded = DateTime.Now },
                new Movie { Name = "Die Hard", GenreId = 2, NumberInStock = 3, ReleasedDate = DateTime.Now, DateAdded = DateTime.Now },
                new Movie { Name = "Terminator", GenreId = 2, NumberInStock = 7, ReleasedDate = DateTime.Now, DateAdded = DateTime.Now },
                new Movie { Name = "Toy Story", GenreId = 3, NumberInStock = 12, ReleasedDate = DateTime.Now, DateAdded = DateTime.Now },
                new Movie { Name = "Titanic", GenreId = 4, NumberInStock = 9, ReleasedDate = DateTime.Now, DateAdded = DateTime.Now });
        }
    }
}
