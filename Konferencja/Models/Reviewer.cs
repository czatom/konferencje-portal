using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konferencja.Models
{
    public class Reviewer
    {
        public Reviewer()
        {
            Photo = "/Content/Images/person.png";
        }
        public int ID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Imię nie może być dłuższe niż 50 znaków.")]
        [Display(Name = "Imię")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        public string Specialisation { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Fotografia")]
        public string Photo { get; set; }

        [Display(Name = "Referencje")]
        [DataType(DataType.MultilineText)]
        [StringLength(500, ErrorMessage = "Referencje nie mogą być któtsze niż 50 i dłuższe niż 500 znaków.", MinimumLength =50)]
        public string References { get; set; }

        [Display(Name = "Imię i nazwisko")]
        public string FullName
        {
            get
            {
                return Name + ", " + Surname;
            }
        }

        [Display(Name = "ID użytkownika")]
        public string ApplicationUserId { get; set; }

        [Display(Name = "Użytkownik")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Display(Name = "Recenzje")]
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
