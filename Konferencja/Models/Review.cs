using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Display(Name = "Ocena")]
        [DisplayFormat(NullDisplayText = "Brak oceny")]

        public Grade? Grade { get; set; }

        [Required]
        [Display(Name = "Opis")]
        [StringLength(1000, ErrorMessage = "Opis nie może być dłuższy niż 1000 znaków.")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid? Token { get; set; }

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


        public bool HasGrade
        {
            get { return Grade.HasValue; }
                
        }
    }
}
