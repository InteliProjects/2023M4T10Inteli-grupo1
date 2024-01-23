using BackendIotvos.Data.Context;
using BackendIotvos.Domain.Entities;
using BackendIotvos.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackendIotvos.Repository
{
    public class ConjuntoRepository : BaseRepository, IConjuntoRepository
    {
        private readonly IoTvosContext _context;

        public ConjuntoRepository(IoTvosContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Conjunto>> GetConjuntosByNameAsync(string name)
        {
            name = name.ToLower();

            List<Conjunto> matchingConjuntos = await _context.Conjuntos
                .Where(conjunto => conjunto.Name.ToLower().Contains(name))
                .ToListAsync();

            return matchingConjuntos;
        }
    }
}
