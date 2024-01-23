using BackendIotvos.Domain.DTOs.Responses;
using BackendIotvos.Domain.DTOs.ViewModels;
using BackendIotvos.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendIotvos.Services.Interfaces
{
    public interface IOrdemServicoService
    {
        Task<IEnumerable<OrdemServicoDto>> GetAllOrdemServicosAsync();
        Task<OrdemServicoDto?> GetOrdemServicoByIdAsync(Guid id);
        Task<Guid> AddOrdemServicoAsync(OrdemServicoAddViewModel ordemServico);
        Task UpdateOrdemServicoAsync(OrdemServicoUpdateViewModel ordemServico);
        Task AlterarStatusOrdemServicoAsync(Guid id, StatusOrdemServico status);
        Task DeleteOrdemServicoAsync(Guid id);
        Task<IEnumerable<OrdemServicoDto>> GetOrdemServicoByNameAsync(string name);
    }
}
