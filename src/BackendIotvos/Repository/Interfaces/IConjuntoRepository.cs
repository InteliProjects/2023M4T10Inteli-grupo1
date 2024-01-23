using BackendIotvos.Domain.Entities;

namespace BackendIotvos.Repository.Interfaces
{
    public interface IOrdemServicoRepository : IBaseRepository
    {
        Task<IEnumerable<OrdemServico>> GetOrdensServicoByNameAsync(string name);
    }
}
