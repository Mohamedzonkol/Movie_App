using Microsoft.EntityFrameworkCore;
using MoviesApp.Data;
using MoviesApp.DTO;
using MoviesApp.Model;
using MoviesApp.Movie_Gerenic;

namespace MoviesApp.Services
{
    public class MoviesServices : Generic<Movie>, IMoviesServices
    {
        private readonly DbSet<Movie> movie;

        public MoviesServices(AppDbContext _context) : base(_context)
        {
            movie = _context.Set<Movie>();
        }

        public async Task<IEnumerable<Movie>> getAll(int GenreId=0)
        {
            return await movie.Where(g => g.GenreId == GenreId|| GenreId==0)
                    .OrderByDescending(g => g.Rate)
                    .Include(g => g.Genre)
                    .ToListAsync();

        }
        public async Task<Movie> Search(string movieName)
        {
            return await movie.Include(g => g.Genre).SingleOrDefaultAsync(x => x.Title == movieName);
            
        }

    }
}
