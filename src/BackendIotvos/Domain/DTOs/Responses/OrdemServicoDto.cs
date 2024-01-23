using BackendIotvos.Domain.Entities;
using BackendIotvos.Domain.Enumerations;

namespace BackendIotvos.Domain.DTOs.Responses
{
    public class OrdemServicoDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public StatusOrdemServico Status { get; set; }
        public ICollection<Item> Items { get; } = new List<Item>();
    }
}
