using Microsoft.EntityFrameworkCore;
using MovieInfo.Domain.Extenstions;
using MovieInfo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieInfo.EntityFramework.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext _context;


        public MovieRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Movie> GetMovieById(int id)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id && !m.IsDeleted);
            if (movie == null) throw new NullReferenceException(" Movie not found ");
            return movie;
        }

        public async Task<Movie> AddAsync(Movie movie)
        {
            await _context.AddAsync(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public async Task Delete(int id)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
            movie.SoftDelete();
            movie.DeletionTime = DateTime.Now;
            await _context.SaveChangesAsync();
        }


        public int Sum(params int[] parammeters)
        {
            int result = 0;
            if (parammeters == null) return 0;
            foreach (var item in parammeters)
            {
                result += item;
            }
            return result;
        }

        public void Calc()
        {
            var res = Sum(1, 2, 3, 5, 100);
        }



        public async Task<IEnumerable<Movie>> GetAll()
        {
            return await _context.Movies.Where(m => !m.IsDeleted).ToListAsync();
        }
    }
}
