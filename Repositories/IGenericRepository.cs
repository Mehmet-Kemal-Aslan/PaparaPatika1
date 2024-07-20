using PaparaPatika.Entitities;

namespace PaparaPatika.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : Base
    {
        Task<List<TEntity?>> GetAll();
        Task<TEntity?> GetById(int Id);
        Task<TEntity?> Create(TEntity entity);
        Task<TEntity?> Update(TEntity entity);
        Task<TEntity?> Delete(int Id);
    }
}
