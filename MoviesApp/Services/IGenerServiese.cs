using MoviesApp.Model;
using MoviesApp.Movie_Gerenic;

namespace MoviesApp.Services
{
    public interface IGenerServiese :IGeneric<Genre>
    {
        Task<IEnumerable<Genre>> getAll();
        Task<bool> IsVaild(int id);
    }
}