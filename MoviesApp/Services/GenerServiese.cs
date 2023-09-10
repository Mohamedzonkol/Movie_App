using Microsoft.EntityFrameworkCore;
using MoviesApp.Data;
using MoviesApp.Model;
using MoviesApp.Movie_Gerenic;

namespace MoviesApp.Services
{
    public class GenerServiese : Generic<Genre>, IGenerServiese
    {
        private readonly DbSet<Genre> Genre;

        public GenerServiese(AppDbContext context) : base(context)
        {
            Genre = context.Set<Genre>();
        }
        public async Task<IEnumerable<Genre>> getAll()
        {
            return await Genre.OrderBy(n => n.Name).ToListAsync();
        }
        public async Task<bool> IsVaild(int id)
        {
            return await Genre.AnyAsync(n => n.Id == id);
        }


    }
}
