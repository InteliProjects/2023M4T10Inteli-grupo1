using BackendIotvos.Domain.Entities;
using BackendIotvos.Domain.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace BackendIotvos.Domain.DTOs.ViewModels
{
    public class OrdemServicoUpdateViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public StatusOrdemServico Status { get; set; }
        public ICollection<Item> Items { get; } = new List<Item>();
    }
}
