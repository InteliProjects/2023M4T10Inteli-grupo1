using BackendIotvos.Domain.Enumerations;
using System.Reflection.Metadata;

namespace BackendIotvos.Domain.Entities
{
    public class Item : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public StatusItem Status { get; set; }
        public uint Units { get; set; }
        public string Location { get; set; }
        public string rfidTag { get; set; }
        public Guid ConjuntoId { get; set; }
        public Conjunto Conjunto { get; set; }

        // Navegação opcional para permitir que item exista sem OS
        public Guid? OrdemServicoId { get; set; }
        public OrdemServico? OrdemServico { get; set; }
    }
}
