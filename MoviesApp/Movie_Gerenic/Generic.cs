using MoviesApp.Data;

namespace MoviesApp.Movie_Gerenic
{
    public class Generic<T> : IGeneric<T> where T : class
    {
        private readonly AppDbContext context;

        public Generic(AppDbContext _context)
        {
            context = _context;
        }
        public async Task<T> Add(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        public async Task<T> Delete(T entity)
        {
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        public async Task<T> getById(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }
        public async Task<T> Update(T entity)
        {
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }
    }
}
