using BackendIotvos.Domain.Entities;

namespace BackendIotvos.Repository.Interfaces
{
    public interface IConjuntoRepository : IBaseRepository
    {
        Task<IEnumerable<Conjunto>> GetConjuntosByNameAsync(string name);
    }
}
