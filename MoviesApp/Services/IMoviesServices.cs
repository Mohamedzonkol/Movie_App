using MoviesApp.Model;
using MoviesApp.Movie_Gerenic;

namespace MoviesApp.Services
{
    public interface IMoviesServices: IGeneric<Movie>
    {
        Task<IEnumerable<Movie>> getAll(int GenreId=0);
        Task<Movie> Search(string movieName);
    }
}