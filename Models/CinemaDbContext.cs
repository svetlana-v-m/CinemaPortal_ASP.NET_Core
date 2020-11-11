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
        public Cinema FindCinemaByTitle(string title)
        {
            return Set<Cinema>().First(c => c.Name.ToUpper().Trim().Equals(title.ToUpper().Trim()));
        }

        public async Task<Cinema> FindCinemaByTitleAsync(string title)
        {
            return await Set<Cinema>().FirstAsync(c => c.Name.ToUpper().Trim().Equals(title.ToUpper().Trim()));
        }

        public void Add(Cinema cinema)
        {
            Set<Cinema>().Add(cinema);
            this.SaveChangesAsync();
        }

        public void Change(Cinema cinema)
        {
            Set<Cinema>().Update(cinema);
            this.SaveChangesAsync();
        }

        public void Delete(Cinema cinema)
        {
            Set<Cinema>().Remove(cinema);
        }
    }
}

