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

    class Review
    {
        public int ID { get; set; }

        [Display(Name = "Grade")]
        [DisplayFormat(NullDisplayText = "No grade")]
        public Grade? Grade { get; set; }


        [Required]
        public int ReviewerID { get; set; }

        public virtual Reviewer Reviewer { get; set; }
    }
}
