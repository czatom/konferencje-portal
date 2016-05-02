using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konferencja.Models
{
    public class ManagePublicationsViewModel
    {
        [Display(Name = "Publikacje bez wyznaczonych recenzji")]
        public ICollection<Publication> PublicationsWithoutReviews { get; set; }

        [Display(Name = "Publikacje bez ocenionych recenzji")]
        public ICollection<Publication> PublicationsWithoutAssessment { get; set; }

        [Display(Name = "Publikacje oczekujące na akceptację")]
        public ICollection<Publication> PendingPublications{ get; set; }

        [Display(Name = "Odrzucone publikacje")]
        public ICollection<Publication> RejectedPublications { get; set; }

        [Display(Name = "Zaakceptowane publikacje")]
        public ICollection<Publication> AcceptedPublications { get; set; }
    }
}
