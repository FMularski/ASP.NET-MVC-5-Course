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
            context.Customers.AddOrUpdate(c => c.Id,
                new Customer { Name = "John Smith", IsSubscribedToNewsletter = false, MembershipTypeId = 1 },
                new Customer { Name = "Mary Williams", IsSubscribedToNewsletter = true, MembershipTypeId = 2 });
        }
    }
}
