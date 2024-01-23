using BackendIotvos.Domain.Entities;

namespace BackendIotvos.Repository.Interfaces
{
    public interface IItemRepository : IBaseRepository
    {
        Task<IEnumerable<Item>> GetItemsByNameAsync(string name);
    }
}
