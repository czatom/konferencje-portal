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
        [Display(Name = "Publikacje bez recenzji")]
        public ICollection<Publication> PublicationsWithoutReviews { get; set; }

        [Display(Name = "Publikacje oczekujące na akceptację")]
        public ICollection<Publication> PendingPublications{ get; set; }

    }
}
