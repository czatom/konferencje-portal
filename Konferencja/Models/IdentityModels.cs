using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Konferencja.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        [Display(Name = "Imię")]
        public string Name { get; set; }

        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        [Display(Name = "Ulica")]
        public string Address { get; set; }

        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Display(Name = "Kod pocztowy")]
        public string PostalCode { get; set; }

        public virtual ICollection<Publication> Publications { get; set; }

        [Display(Name = "Pełny adres")]
        public string DisplayAddress
        {
            get
            {
                string dspAddress =
                    string.IsNullOrWhiteSpace(this.Address) ? "" : this.Address;
                string dspCity =
                    string.IsNullOrWhiteSpace(this.City) ? "" : this.City;
                string dspPostalCode =
                    string.IsNullOrWhiteSpace(this.PostalCode) ? "" : this.PostalCode;

                return string
                    .Format("{0} {1} {2}", dspAddress, dspCity, dspPostalCode);
            }
        }

        [Display(Name = "Imię i nazwisko")]
        public string FullName
        {
            get
            {
                string dspName =
                    string.IsNullOrWhiteSpace(this.Name) ? "" : this.Name;
                string dspSurname =
                    string.IsNullOrWhiteSpace(this.Surname) ? "" : this.Surname;

                return string
                    .Format("{0} {1} ", dspName, dspSurname);
            }
        }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Konferencja.Models.Conference> Conferences { get; set; }

        public System.Data.Entity.DbSet<Konferencja.Models.Publication> Publications { get; set; }

        public System.Data.Entity.DbSet<Konferencja.Models.Reviewer> Reviewers { get; set; }

        public System.Data.Entity.DbSet<Konferencja.Models.Review> Reviews { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<Conference>()
        //        .HasMany(p => p.Publications)
        //        .WithRequired()
        //        .HasForeignKey(ci => ci.ConferenceID);

        //    modelBuilder.Entity<Publication>()
        //        .HasKey(pi => new { pi.ID, pi.ConferenceID })
        //        .Property(pi => pi.ID)
        //        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        //}
    }
}