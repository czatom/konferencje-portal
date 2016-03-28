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
        [Display(Name = "Data")]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Temat nie może mieć więcej niż 200 znaków.")]
        [Display(Name = "Temat")]
        public string Theme { get; set; }

        [UIHint("PublicationTemplate")]
        [Display(Name = "Publikacje")]
        public virtual ICollection<Publication> Publications { get; set; }
    }
}
