using AutoMapper;
using BackendIotvos.Domain.DTOs.Responses;
using BackendIotvos.Domain.DTOs.ViewModels;
using BackendIotvos.Domain.Entities;

namespace BackendIotvos.Domain.DTOs.Helpers
{
    public class OrdemServicoProfile : Profile
    {
        public OrdemServicoProfile()
        {
            CreateMap<OrdemServicoAddViewModel, OrdemServico>();
            CreateMap<OrdemServico, OrdemServicoDto>();
        }

    }
}
