using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Collective.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(15)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(15)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [MaxLength(200)]
        [Display(Name = "About Me")]
        public string Bio { get; set; }
        public string UserImgPath { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
