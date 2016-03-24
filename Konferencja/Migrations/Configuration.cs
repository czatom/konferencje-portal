namespace Konferencja.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Konferencja.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Konferencja.Models.ApplicationDbContext";
        }

        protected override void Seed(Konferencja.Models.ApplicationDbContext context)
        {
              context.Conferences.AddOrUpdate(
                p => p.Theme,
                new Conference { Theme = "Bardzo powa¿na konferencja 1", Date = new DateTime(2016, 1, 1) },
                new Conference { Theme = "Bardzo powa¿na konferencja 2", Date = new DateTime(2016, 5, 25) },
                new Conference { Theme = "O wy¿szoœci ziemniaka nad dziurkowaniem w Fortranie", Date = new DateTime(2016, 6, 26) }
              );

            context.Roles.AddOrUpdate(
              p => p.Name,
              new Role { Name = "Bardzo powa¿na konferencja 1", Date = new DateTime(2016, 1, 1) },
              new Conference { Theme = "Bardzo powa¿na konferencja 2", Date = new DateTime(2016, 5, 25) },
              new Conference { Theme = "O wy¿szoœci ziemniaka nad dziurkowaniem w Fortranie", Date = new DateTime(2016, 6, 26) }
            );
        }
    }
}
