using BackendIotvos.Domain.DTOs.Responses;
using BackendIotvos.Domain.DTOs.ViewModels;
using BackendIotvos.Domain.Entities;
using BackendIotvos.Domain.Enumerations;

namespace BackendIotvos.Services.Interfaces
{
    public interface IItemService
    {
        Task<IEnumerable<ItemDto>> GetAllItemsAsync();
        Task<ItemDto?> GetItemByIdAsync(Guid id);
        Task UpdateItemAsync(ItemUpdateViewModel item);
        Task AlterarStatusItemAsync(Guid id, StatusItem status);
        Task<Guid> AddItemAsync(ItemAddViewModel item);
        Task DeleteItemAsync(Guid id);
        Task<IEnumerable<ItemDto>> GetItemByNameAsync(string name);
    }
}
