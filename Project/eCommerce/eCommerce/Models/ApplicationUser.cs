﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name ="Full Name")]
        public string FullName { get; set; }
        public string UserRoles { get; set; }
    }
}
