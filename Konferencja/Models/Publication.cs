using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konferencja.Models
{
    public class Publication
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Author")]
        public Author Author { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "Title cannot be longer than 200 characters and shorter then 10.")]
        public string Title { get; set; }

        [StringLength(500, ErrorMessage = "Title cannot be longer than 500 characters.")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Related file")]
        public string File { get; set; }

        public virtual ICollection<Reviewer> Reviewers { get; set; }
    }
}
