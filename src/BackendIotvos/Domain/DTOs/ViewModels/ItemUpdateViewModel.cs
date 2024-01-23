using BackendIotvos.Domain.Enumerations;

namespace BackendIotvos.Domain.DTOs.ViewModels
{
    public class ItemUpdateViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public StatusItem Status { get; set; }
        public uint Units { get; set; }
        public string Location { get; set; }
        public string rfidTag { get; set; }
    }
}
