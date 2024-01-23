using BackendIotvos.Domain.DTOs.Responses;
using BackendIotvos.Domain.DTOs.ViewModels;
using BackendIotvos.Domain.Enumerations;

namespace BackendIotvos.Services.Interfaces
{
    public interface IConjuntoService
    {
        Task<IEnumerable<ConjuntoDto>> GetAllConjuntosAsync();
        Task<ConjuntoDto?> GetConjuntoByIdAsync(Guid id);
        Task UpdateConjuntoAsync(ConjuntoUpdateViewModel conjunto);
        Task AlterarStatusConjuntoAsync(Guid id, StatusConjunto status);
        Task<Guid> AddConjuntoAsync(ConjuntoAddViewModel conjunto);
        Task DeleteConjuntoAsync(Guid id);
        Task<IEnumerable<ConjuntoDto>> GetConjuntoByNameAsync(string name);
    }
}
