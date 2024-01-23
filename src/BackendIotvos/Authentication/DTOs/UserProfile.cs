using Microsoft.AspNetCore.Identity;
using AutoMapper;

namespace BackendIotvos.Authentication.DTOs
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRegisterViewModel, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

            CreateMap<IdentityResult, UserRegisterResponse>()
                .ForMember(dest => dest.Success, opt => opt.MapFrom(src => src.Succeeded));

            CreateMap<SignInResult, UserLoginResponse>()
                .ForMember(dest => dest.Sucesso, opt => opt.MapFrom(src => src.Succeeded));
        }
    }
}
