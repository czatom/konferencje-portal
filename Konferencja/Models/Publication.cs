using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konferencja.Models
{
    public class Publication
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "ID użytkownika")]
        public string ApplicationUserId { get; set; }

        [Required]
        [Display(Name = "ID konferencji")]
        public int ConferenceID { get; set; }

        [Display(Name = "Akceptacja")]
        public bool Accepted { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "Tytuł nie może być krótszy niż 10 i dłuższy niż 200.")]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [StringLength(500, ErrorMessage = "Opis nie może być dłuższy niż 500 znaków.")]
        [Display(Name = "Opis")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Plik")]
        public string File { get; set; }

        [Display(Name = "Autor")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Display(Name = "Konferencja")]
        public virtual Conference Conference { get; set; }

        [Display(Name = "Recenzje")]
        public virtual ICollection<Review> Reviews { get; set; }

        public bool HasReviews
        {
            get
            {
                return Reviews!=null && Reviews.Count > 0;
            }
        }

        public bool HasIncompleteReviews
        {
            get
            {
                return Reviews != null && Reviews.Where(r=>!r.HasGrade).Count() > 0;
            }
        }
    }
}
