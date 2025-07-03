using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.BaseRepository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly SoundcloneContext _soundcloneContext;
        private readonly DbSet<T> _dbSet;

        public BaseRepository( SoundcloneContext soundcloneContext)
        {
            _soundcloneContext = soundcloneContext;
            _dbSet = soundcloneContext.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _soundcloneContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async void Remove(T entity)
        {
            _dbSet.Remove(entity);
            await _soundcloneContext.SaveChangesAsync();
        }

        public async void Update(T entity)
        {
            _dbSet.Update(entity);
            await _soundcloneContext.SaveChangesAsync();
        }
    }
}
