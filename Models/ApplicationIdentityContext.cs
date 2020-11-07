using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaPortal_ASP.NET_Core.Models
{
    public class ApplicationIdentityContext:IdentityDbContext<User>
    {
        public ApplicationIdentityContext(DbContextOptions<ApplicationIdentityContext> options) : base(options)  
        {
            Database.EnsureCreated();
        }
    }
}
