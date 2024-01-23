using AutoMapper;
using BackendIotvos.Domain.DTOs.Responses;
using BackendIotvos.Domain.DTOs.ViewModels;
using BackendIotvos.Domain.Entities;
using BackendIotvos.Domain.Enumerations;
using BackendIotvos.Repository.Interfaces;
using BackendIotvos.Services.Interfaces;

namespace BackendIotvos.Services
{
    public class OrdemServicoService : IOrdemServicoService
    {
        private readonly IOrdemServicoRepository _ordemServicoRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public OrdemServicoService(IOrdemServicoRepository ordemServicoRepository, IMapper mapper, IUnitOfWork uow)
        {
            _ordemServicoRepository = ordemServicoRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<IEnumerable<OrdemServicoDto>> GetAllOrdemServicosAsync()
        {
            IEnumerable<OrdemServico> ordemServicos = await _ordemServicoRepository.GetAllAsync<OrdemServico>();
            IEnumerable<OrdemServicoDto> ordemServicosResponse = _mapper.Map<IEnumerable<OrdemServicoDto>>(ordemServicos);
            return ordemServicosResponse;
        }

        public async Task<OrdemServicoDto?> GetOrdemServicoByIdAsync(Guid id)
        {
            OrdemServico? ordemServico = await _ordemServicoRepository.GetByIdAsync<OrdemServico>(id);
            OrdemServicoDto ordemServicoResponse = _mapper.Map<OrdemServicoDto>(ordemServico);
            return ordemServicoResponse;
        }

        public async Task<Guid> AddOrdemServicoAsync(OrdemServicoAddViewModel ordemServico)
        {
            OrdemServico ordemServicoEntity = _mapper.Map<OrdemServico>(ordemServico);
            _ordemServicoRepository.Add(ordemServicoEntity);
            await _uow.Commit();
            return ordemServicoEntity.Id;
        }

        public async Task UpdateOrdemServicoAsync(OrdemServicoUpdateViewModel ordemServico)
        {
            OrdemServico ordemServicoEntity = await _ordemServicoRepository.GetByIdAsync<OrdemServico>(ordemServico.Id) ?? throw new Exception("OrdemServico not found");
            _mapper.Map(ordemServico, ordemServicoEntity);
            _ordemServicoRepository.Update(ordemServicoEntity);
            await _uow.Commit();
        }

        public async Task AlterarStatusOrdemServicoAsync(Guid id, StatusOrdemServico status)
        {
            OrdemServico ordemServico = await _ordemServicoRepository.GetByIdAsync<OrdemServico>(id) ?? throw new Exception("OrdemServico not found");
            ordemServico.Status = status;
            _ordemServicoRepository.Update(ordemServico);
            await _uow.Commit();
        }

        public async Task DeleteOrdemServicoAsync(Guid id)
        {
            OrdemServico ordemServico = await _ordemServicoRepository.GetByIdAsync<OrdemServico>(id) ?? throw new Exception("OrdemServico not found");
            _ordemServicoRepository.Delete(ordemServico);
            await _uow.Commit();
        }

        public async Task<IEnumerable<OrdemServicoDto>> GetOrdemServicoByNameAsync(string name)
        {
            IEnumerable<OrdemServico> ordemServicos = await _ordemServicoRepository.GetOrdensServicoByNameAsync(name);
            IEnumerable<OrdemServicoDto> ordemServicosResponse = _mapper.Map<IEnumerable<OrdemServicoDto>>(ordemServicos);
            return ordemServicosResponse;
        }
    }
}
