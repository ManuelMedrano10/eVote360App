using eVote360App.Core.Domain.Common;

namespace eVote360App.Core.Domain.Interfaces
{
    public interface IGenericRepository<Entity> where Entity : BasicEntity<int>
    {
        Task<Entity> AddAsync(Entity entity);
        Task<Entity> UpdateAsync(int id, Entity entity);
        Task DeleteAsync(int id);
        Task<List<Entity>> GetAllAsync();
        IQueryable<Entity> GetAllQuery();
        Task<Entity?> GetByIdAsync(int id);
        Task ChangeStatusAsync(int id);
    }
}
