using AutoMapper;
using BackendIotvos.Domain.DTOs.Responses;
using BackendIotvos.Domain.DTOs.ViewModels;
using BackendIotvos.Domain.Entities;

namespace BackendIotvos.Domain.DTOs.Helpers
{
    public class ItemProfile : Profile
    {
        public ItemProfile() 
        { 
            CreateMap<ItemAddViewModel, Item>();
            CreateMap<Item, ItemDto>();
        }

    }
}
