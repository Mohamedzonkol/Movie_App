using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoviesApp.Model;

namespace MoviesApp.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

       public DbSet<Movie> Movies { get; set; }
       public DbSet<Genre> Genres { get; set; }

    }
}
