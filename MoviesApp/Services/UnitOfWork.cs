using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MoviesApp.Data;

namespace MoviesApp.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;
        private readonly UserManager<AppUser> userManager;
        private readonly IConfiguration configuration;

        public UnitOfWork(AppDbContext _context,UserManager<AppUser> _userManager,IConfiguration _configuration)
        {
            context = _context;
            userManager = _userManager;
            configuration = _configuration;
            genreService = new GenerServiese(context);
            moviesService  =new MoviesServices(context);
            accountServiecs = new AccountServiecs(userManager, configuration);
        }
        public IGenerServiese genreService { get; private set; }

        public IMoviesServices moviesService { get; private set; }
        public IAccountServiecs accountServiecs { get; private set; }

      
        public void Dispose()
        {
            context.Dispose();
        }
    }
}
