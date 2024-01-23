using BackendIotvos.Data.Context;
using BackendIotvos.Domain.Entities;
using BackendIotvos.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackendIotvos.Repository
{
    public class OrdemServicoRepository : BaseRepository, IOrdemServicoRepository
    {
        private readonly IoTvosContext _context;

        public OrdemServicoRepository(IoTvosContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrdemServico>> GetOrdensServicoByNameAsync(string name)
        {
            name = name.ToLower();

            List<OrdemServico> matchingOrdensServico = await _context.OrdensServico
                .Where(ordemServico => ordemServico.Name.ToLower().Contains(name))
                .ToListAsync();

            return matchingOrdensServico;
        }
    }
}
