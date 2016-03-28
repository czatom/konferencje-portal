using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konferencja.Models
{
    public enum Grade
    {
        A, B, C, D, F
    }

    public class Review
    {
        public int ID { get; set; }

        [Display(Name = "Ocena")]
        [DisplayFormat(NullDisplayText = "Brak oceny")]
        public Grade? Grade { get; set; }

        [Required]
        [Display(Name = "ID Recenzenta")]
        public int ReviewerID { get; set; }

        [Required]
        [Display(Name = "ID publikacji")]
        public int PublicationID { get; set; }

        [Display(Name = "Publikacja")]
        public virtual Publication Publication { get; set; }

        [Display(Name = "Recenzent")]
        public virtual Reviewer Reviewer { get; set; }

        public bool HasGrade()
        {
            return Grade.HasValue;
        }
    }
}
