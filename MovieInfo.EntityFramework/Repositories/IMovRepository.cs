using MovieInfo.Domain.Models;
using MovieInfo.EntityFramework.Repositories.Generic;
using System.Threading.Tasks;

namespace MovieInfo.EntityFramework.Repositories
{
    public interface IMovRepository : IRepositiry<Movie>
    {
        Task<Movie> GetByName(string name);
    }
}