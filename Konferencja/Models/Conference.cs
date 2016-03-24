using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konferencja.Models
{
    public class Conference
    {
        public int ID { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of the conference")]
        public DateTime Date { get; set; }
        [Required]
        [StringLength(200, ErrorMessage = "Title cannot be longer than 200 characters.")]
        [Display(Name = "Theme of the conference")]
        public string Theme { get; set; }

        public virtual ICollection<Publication> Publications { get; set; }
    }
}
