
using System.Threading.Tasks;

namespace HomeHarvest.Client.Services
{
    public interface IManager<TEntity> where TEntity : class
    {
        Task<bool> Delete(int id);
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(int id);
        Task<TEntity> Insert(TEntity entity);
        Task<TEntity> Update(TEntity entityToUpdate);
    }
}