using MovieInfo.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieInfo.EntityFramework.Repositories
{
    public interface IMovieRepository
    {

        Task<Movie> AddAsync(Movie movie);
        Task Delete(int id);
        Task<IEnumerable<Movie>> GetAll();
        Task<Movie> GetMovieById(int id);
    }
}