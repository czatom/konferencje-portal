﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konferencja.Models
{
    public class Author
    {
        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string Name { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Second name cannot be longer than 100 characters.")]
        public string Surname { get; set; }

        [Key]
        [ForeignKey("ApplicationUser")]
        public int ApplicationUserID { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}