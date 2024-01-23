using BackendIotvos.Data.Context;
using BackendIotvos.Domain.Entities;
using BackendIotvos.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackendIotvos.Repository
{
    public class BaseRepository : IBaseRepository
    {
        private readonly IoTvosContext _context;

        public BaseRepository(IoTvosContext context)
        {
            _context = context;
        }

        public async Task<T?> GetByIdAsync<T>(Guid id) where T : BaseEntity
        {
            return await _context.Set<T>().FirstOrDefaultAsync(t => t.Id == id);
        }
        public async Task<IEnumerable<T>> GetAllAsync<T>() where T : BaseEntity
        {
            return await _context.Set<T>().ToListAsync();
        }
        public void Add<T>(T entity) where T : BaseEntity
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
            _context.Add(entity);
        }
        public void Update<T>(T entity) where T : BaseEntity
        {
            _context.Entry(entity).State = EntityState.Modified;
            entity.UpdatedAt = DateTime.UtcNow;
        }

        public void Delete<T>(T entity) where T : BaseEntity
        {
            _context.Remove(entity);
        }
    }
}
