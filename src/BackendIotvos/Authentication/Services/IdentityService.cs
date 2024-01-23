using AutoMapper;
using BackendIotvos.Authentication.DTOs;
using BackendIotvos.Authentication.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BackendIotvos.Authentication.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IIdentityRepository _identityRepository;

        public IdentityService(
            SignInManager<User> signInManager,
            ITokenService tokenService,
            IMapper mapper,
            IIdentityRepository identityRepository)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
            _identityRepository = identityRepository;
        }

        public async Task<UserRegisterResponse> RegisterUser(UserRegisterViewModel userRegister)
        {
            User identityUser = _mapper.Map<User>(userRegister);

            IdentityResult result = await _identityRepository.CreateUserAsync(identityUser, userRegister.Password);

            UserRegisterResponse userRegisterResponse = _mapper.Map<UserRegisterResponse>(result);
            if (!result.Succeeded && result.Errors.Any())
                userRegisterResponse.Success = false;
            userRegisterResponse.AdicionarErros(result.Errors.Select(r => r.Description));

            return userRegisterResponse;
        }

        public async Task<UserLoginResponse> Login(UserLoginViewModel userLogin)
        {
            SignInResult result = await _signInManager.PasswordSignInAsync(userLogin.Email, userLogin.Password, false, true);
            if (result.Succeeded)
            {
                User user = await _identityRepository.FindByEmailAsync(userLogin.Email);
                TokenResponseDto tokenResponse = await _tokenService.GenerateToken(user);
                return new UserLoginResponse(true, tokenResponse.Token, tokenResponse.ExpirationDate);
            }

            UserLoginResponse userLoginResponse = _mapper.Map<UserLoginResponse>(result);
            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                    userLoginResponse.AddError("Essa conta está bloqueada");
                else if (result.IsNotAllowed)
                    userLoginResponse.AddError("Essa conta não tem permissão para fazer login");
                else if (result.RequiresTwoFactor)
                    userLoginResponse.AddError("É necessário confirmar o login no seu segundo fator de autenticação");
                else
                    userLoginResponse.AddError("Usuário ou senha estão incorretos");
            }

            return userLoginResponse;
        }

        public async Task<IEnumerable<UserResponseDto>> GetUsersWithRolesAsync()
        {
            List<User> users = await _identityRepository.GetUsersAsync();

            List<UserResponseDto> usersWithRoles = new();

            foreach (User user in users)
            {
                IList<string> roles = await _identityRepository.GetRolesAsync(user);

                UserResponseDto userResponseDto = new()
                {
                    Login = user.UserName,
                    Name = user.Name,
                    Roles = roles.ToList()
                };

                usersWithRoles.Add(userResponseDto);
            }

            return usersWithRoles;
        }

        public async Task<bool> SetUserRoleAsync(AddUserRoleViewModel userRole)
        {
            User user = await _identityRepository.FindByEmailAsync(userRole.Login) ?? throw new ArgumentException("Usuário não encontrado");

            await _identityRepository.AddToRoleAsync(user, userRole.Role.ToString());

            return true;
        }

        public async Task<bool> RemoveUserAsync(string login)
        {
            User user = await _identityRepository.FindByEmailAsync(login) ?? throw new ArgumentException("Usuário não encontrado");
            IdentityResult result = await _identityRepository.DeleteUserAsync(user);

            return result.Succeeded;
        }
    }
}
