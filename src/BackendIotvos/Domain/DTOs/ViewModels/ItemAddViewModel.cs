using BackendIotvos.Domain.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace BackendIotvos.Domain.DTOs.ViewModels
{
    public class ItemAddViewModel
    {
        [Required(ErrorMessage = "O campo 'Name' e obrigatorio.")]
        public string Name { get; set; }
        public string Description { get; set; }
        public StatusItem Status { get; set; }
        public uint Units { get; set; }

        public string Location { get; set; }
        public string rfidTag { get; set; }

        [Required(ErrorMessage = "O campo 'ConjuntoId' e obrigatorio.")]
        public Guid ConjuntoId { get; set; }
    }
}
