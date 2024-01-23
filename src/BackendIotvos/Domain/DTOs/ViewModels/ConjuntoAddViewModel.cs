using BackendIotvos.Domain.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace BackendIotvos.Domain.DTOs.ViewModels
{
    public class ConjuntoAddViewModel
    {
        [Required(ErrorMessage = "O campo 'Name' e obrigatorio.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo 'Tipo' e obrigatorio.")]
        public TipoConjunto Tipo { get; set; }
        public StatusConjunto Status { get; set; }
    }
}
