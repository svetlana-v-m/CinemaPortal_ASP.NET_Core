using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaPortal_ASP.NET_Core.Models
{
    public class CinemaDbContext:DbContext
    {
        public DbSet<Cinema> CinemaCollection { get; set; }
        public CinemaDbContext(DbContextOptions<CinemaDbContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }
        public Cinema FindCinemaByID(int id)
        {
            return Set<Cinema>().First(c => c.CinemaID == id);
        }
        public async Task<Cinema> FindCinemaByIDAsync(int id)
        {
            return await Set<Cinema>().FirstAsync(c=>c.CinemaID==id);
        }
        
       
    }
}

