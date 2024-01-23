using BackendIotvos.Domain.Enumerations;

namespace BackendIotvos.Domain.Entities
{
    public class Conjunto : BaseEntity
    {
        public string Name { get; set; }
        public TipoConjunto Tipo { get; set; }
        public StatusConjunto Status { get; set; }

        public ICollection<Item> Items { get; } = new List<Item>();
    }
}
