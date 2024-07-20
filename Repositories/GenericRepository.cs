using Microsoft.EntityFrameworkCore;
using PaparaPatika.Entitities;

namespace PaparaPatika.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : Base
    {
        private readonly PaparaDbContext _context;
        public GenericRepository(PaparaDbContext context)
        {
            _context = context;
        }
        public async Task<TEntity?> Create(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity?> Delete(int Id)
        {
            var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == Id);
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<TEntity?>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> GetById(int Id)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<TEntity?> Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
