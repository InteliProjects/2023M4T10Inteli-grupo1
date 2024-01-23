using BackendIotvos.Domain.Enumerations;
using Microsoft.Extensions.Hosting;

namespace BackendIotvos.Domain.Entities
{
    public class OrdemServico : BaseEntity
    {
        public string Name { get; set; }
        public StatusOrdemServico Status {  get; set; }
        public ICollection<Item> Items { get; } = new List<Item>();
    }
}
