using AutoMapper;
using BackendIotvos.Domain.DTOs.Responses;
using BackendIotvos.Domain.DTOs.ViewModels;
using BackendIotvos.Domain.Entities;
using BackendIotvos.Domain.Enumerations;
using BackendIotvos.Repository.Interfaces;
using BackendIotvos.Services.Interfaces;

namespace BackendIotvos.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly IConjuntoService _conjuntoService;

        public ItemService(IItemRepository itemRepository, IMapper mapper, IUnitOfWork uow, IConjuntoService conjuntoService)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
            _uow = uow;
            _conjuntoService = conjuntoService;
        }

        public async Task<IEnumerable<ItemDto>> GetAllItemsAsync()
        {
            IEnumerable<Item> items = await _itemRepository.GetAllAsync<Item>();
            IEnumerable<ItemDto> itemsResponse = _mapper.Map<IEnumerable<ItemDto>>(items);
            return itemsResponse;
        }

        public async Task<ItemDto?> GetItemByIdAsync(Guid id)
        {
            Item? item = await _itemRepository.GetByIdAsync<Item>(id);
            ItemDto itemResponse = _mapper.Map<ItemDto>(item);
            return itemResponse;
        }
        public async Task AlterarStatusItemAsync(Guid id, StatusItem status)
        {
            Item item = await _itemRepository.GetByIdAsync<Item>(id) ?? throw new Exception("Item not found");
            item.Status = status;
            _itemRepository.Update(item);
            await _uow.Commit();
        }
        public async Task UpdateItemAsync(ItemUpdateViewModel item)
        {
            Item itemEntity = _mapper.Map<Item>(item);
            _itemRepository.Update(itemEntity);
            await _uow.Commit();
        }

        public async Task<Guid> AddItemAsync(ItemAddViewModel item)
        {
            _ = await _conjuntoService.GetConjuntoByIdAsync(item.ConjuntoId) ?? throw new ArgumentException($"Conjunto com o ID {item.ConjuntoId} não encontrado");

            Item itemEntity = _mapper.Map<Item>(item);
            _itemRepository.Add(itemEntity);
            await _uow.Commit();

            return itemEntity.Id;
        }


        public async Task<IEnumerable<ItemDto>> GetItemByNameAsync(string name)
        {
            IEnumerable<Item> items = await _itemRepository.GetItemsByNameAsync(name);
            IEnumerable<ItemDto> itemsResponse = _mapper.Map<IEnumerable<ItemDto>>(items);
            return itemsResponse;
        }

        public async Task DeleteItemAsync(Guid id)
        {
            Item? item = await _itemRepository.GetByIdAsync<Item>(id);

            if (item != null)
            {
                _itemRepository.Delete(item);
                await _uow.Commit();
            }
        }
    }
}
