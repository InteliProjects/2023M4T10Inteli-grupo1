using AutoMapper;
using BackendIotvos.Domain.DTOs.Responses;
using BackendIotvos.Domain.DTOs.ViewModels;
using BackendIotvos.Domain.Entities;

namespace BackendIotvos.Domain.DTOs.Helpers
{
    public class ConjuntoProfile : Profile
    {
        public ConjuntoProfile()
        {
            CreateMap<ConjuntoAddViewModel, Conjunto>();
            CreateMap<Conjunto, ConjuntoDto>();
        }

    }
}
