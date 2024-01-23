using AutoMapper;
using BackendIotvos.Domain.DTOs.Responses;
using BackendIotvos.Domain.DTOs.ViewModels;
using BackendIotvos.Domain.Entities;
using BackendIotvos.Domain.Enumerations;
using BackendIotvos.Repository;
using BackendIotvos.Repository.Interfaces;
using BackendIotvos.Services.Interfaces;

namespace BackendIotvos.Services
{
    public class ConjuntoService : IConjuntoService
    {
        private readonly IConjuntoRepository _conjuntoRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public ConjuntoService(IConjuntoRepository conjuntoRepository, IMapper mapper, IUnitOfWork uow)
        {
            _conjuntoRepository = conjuntoRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<IEnumerable<ConjuntoDto>> GetAllConjuntosAsync()
        {
            IEnumerable<Conjunto> conjuntos = await _conjuntoRepository.GetAllAsync<Conjunto>();
            IEnumerable<ConjuntoDto> conjuntosResponse = _mapper.Map<IEnumerable<ConjuntoDto>>(conjuntos);
            return conjuntosResponse;
        }

        public async Task<ConjuntoDto?> GetConjuntoByIdAsync(Guid id)
        {
            Conjunto? conjunto = await _conjuntoRepository.GetByIdAsync<Conjunto>(id);
            ConjuntoDto conjuntoResponse = _mapper.Map<ConjuntoDto>(conjunto);
            return conjuntoResponse;
        }

        public async Task<Guid> AddConjuntoAsync(ConjuntoAddViewModel conjunto)
        {
            Conjunto conjuntoEntity = _mapper.Map<Conjunto>(conjunto);
            _conjuntoRepository.Add(conjuntoEntity);
            await _uow.Commit();
            return conjuntoEntity.Id;
        }

        public async Task UpdateConjuntoAsync(ConjuntoUpdateViewModel conjunto)
        {
            Conjunto conjuntoEntity = await _conjuntoRepository.GetByIdAsync<Conjunto>(conjunto.Id) ?? throw new Exception("Conjunto not found");
            _mapper.Map(conjunto, conjuntoEntity);
            _conjuntoRepository.Update(conjuntoEntity);
            await _uow.Commit();
        }

        public async Task AlterarStatusConjuntoAsync(Guid id, StatusConjunto status)
        {
            Conjunto conjunto = await _conjuntoRepository.GetByIdAsync<Conjunto>(id) ?? throw new Exception("Item not found");
            conjunto.Status = status;
            _conjuntoRepository.Update(conjunto);
            await _uow.Commit();
        }

        public async Task DeleteConjuntoAsync(Guid id)
        {
            Conjunto conjunto = await _conjuntoRepository.GetByIdAsync<Conjunto>(id) ?? throw new Exception("Conjunto not found");
            _conjuntoRepository.Delete(conjunto);
            await _uow.Commit();
        }

        public async Task<IEnumerable<ConjuntoDto>> GetConjuntoByNameAsync(string name)
        {
            IEnumerable<Conjunto> conjuntos = await _conjuntoRepository.GetConjuntosByNameAsync(name);
            IEnumerable<ConjuntoDto> conjuntosResponse = _mapper.Map<IEnumerable<ConjuntoDto>>(conjuntos);
            return conjuntosResponse;
        }
    }
}
