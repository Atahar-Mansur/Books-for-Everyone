using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksForEveryone.Data
{
    public class ApplicationUser : IdentityUser
    {
        //User Primery Info
        [Required]
        public string Name { get; set; }
        [Required, MaxLength(11)]
        public string MobileNumber { get; set; }

        //Present address
        [Required]
        public string Address { get; set; }
        [Required]
        public int ZipCode { get; set; }
        [Required]
        public string AreaThana { get; set; }
        [Required]
        public string District { get; set; }

        //Initial Books Info
        [Required]
        public string Book1Name { get; set; }
        [Required]
        public string Book1WriName { get; set; }

        public string Book2Name { get; set; }
        public string Book2WriName { get; set; }

    }
}
