namespace MoviesApp.Movie_Gerenic
{
    public interface IGeneric<T> where T : class
    {
        Task<T> Add(T entity);
        Task<T> Delete(T entity);
        Task<T> getById(int id);
        Task<T> Update(T entity);
    }
}