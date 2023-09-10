namespace MoviesApp.Services
{
    public interface IUnitOfWork:IDisposable
    {
        IGenerServiese genreService { get; }
        IMoviesServices moviesService { get; }
        IAccountServiecs accountServiecs { get; }

    }
}