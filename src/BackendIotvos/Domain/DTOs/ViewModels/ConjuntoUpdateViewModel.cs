using BackendIotvos.Domain.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace BackendIotvos.Domain.DTOs.ViewModels
{
    public class ConjuntoUpdateViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public TipoConjunto Tipo { get; set; }
        public StatusConjunto Status { get; set; }
    }
}
