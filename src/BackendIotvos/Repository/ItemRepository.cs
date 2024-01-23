using BackendIotvos.Data.Context;
using BackendIotvos.Domain.Entities;
using BackendIotvos.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackendIotvos.Repository
{
    public class ItemRepository : BaseRepository, IItemRepository
    {
        private readonly IoTvosContext _context;

        public ItemRepository(IoTvosContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Item>> GetItemsByNameAsync(string name)
        {
            name = name.ToLower();

            List<Item> matchingItems = await _context.Items
                .Where(item => item.Name.ToLower().Contains(name))
                .ToListAsync();

            return matchingItems;
        }
    }
}
