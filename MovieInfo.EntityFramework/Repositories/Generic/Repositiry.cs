using Microsoft.EntityFrameworkCore;
using MovieInfo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MovieInfo.EntityFramework.Repositories.Generic
{
    public class Repositiry<T> : IRepositiry<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        protected readonly DbSet<T> Entities;
        public Repositiry(ApplicationDbContext context)
        {
            _context = context;
            Entities = _context.Set<T>();
        }

        public int? MyInt { get; set; }
        public float? MyProperty { get; set; }


        public async Task<T> GetAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            var entity = await Entities.IncludeMultiple(includes)
                .FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted) ?? //Check if item is not null 
                throw new ArgumentNullException("Item not found");// if item is not found throw Exception
            return entity;



        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Entities.Where(e => !e.IsDeleted).ToListAsync();
        }


        public async Task<T> AddAsync(T entity)
        {
            entity.SetCreationTime();
            await Entities.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Update(T entity)
        {
            var entityToUpdate = await Entities.FirstOrDefaultAsync(e => e.Id == entity.Id && !e.IsDeleted) ??
                throw new ArgumentNullException("Item not found");

            Entities.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(int id)
        {
            var entity = await Entities.FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted) ??
                throw new ArgumentNullException("Item not found");
            entity.Delete();
            await _context.SaveChangesAsync();
        }
    }
}
