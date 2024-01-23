using BackendIotvos.Domain.Entities;
using BackendIotvos.Domain.Enumerations;

namespace BackendIotvos.Domain.DTOs.Responses
{
    public class ConjuntoDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public TipoConjunto Tipo { get; set; }
        public StatusConjunto Status { get; set; }

        public ICollection<Item> Items { get; } = new List<Item>();
    }
}
