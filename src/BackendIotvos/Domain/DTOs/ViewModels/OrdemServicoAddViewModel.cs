using BackendIotvos.Domain.Entities;
using BackendIotvos.Domain.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace BackendIotvos.Domain.DTOs.ViewModels
{
    public class OrdemServicoAddViewModel
    {
        [Required(ErrorMessage = "O campo 'Name' e obrigatorio.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo 'Tipo' e obrigatorio.")]
        public StatusOrdemServico Status { get; set; }
        public ICollection<Item> Items { get; } = new List<Item>();
    }
}
