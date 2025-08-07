using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JopApplication.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace JopApplication.Infrastructure.DBContexts
{
    public class UserIdentityDbContext:IdentityDbContext<ApplicationUser>
    {
        public UserIdentityDbContext(DbContextOptions<UserIdentityDbContext> options):base(options)
        {
            
        }
    }
}
