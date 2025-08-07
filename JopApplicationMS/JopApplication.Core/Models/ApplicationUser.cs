using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JopApplication.Core.Models
{
    public class ApplicationUser :IdentityUser
    {
        public string? Fname { get; set; }
        public string? Lname { get; set; }

    }
}
