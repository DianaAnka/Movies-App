using Microsoft.EntityFrameworkCore;
using MovieInfo.Domain.Models;
using MovieInfo.EntityFramework.Repositories.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MovieInfo.EntityFramework.Repositories
{
    public class MovRepository : Repositiry<Movie>, IMovRepository
    {
        public MovRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<Movie> GetByName(string name)
        {
            return await Entities.Where(m => m.Name == name).FirstOrDefaultAsync() ??
                throw new ArgumentNullException("Item not found");
        }

        
    }
}
