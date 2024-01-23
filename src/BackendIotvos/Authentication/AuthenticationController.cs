using BackendIotvos.Authentication.Configurations;
using BackendIotvos.Authentication.DTOs;
using BackendIotvos.Authentication.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackendIotvos.Authentication
{
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public AuthenticationController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserRegisterResponse>> Register([FromBody] UserRegisterViewModel usuarioCadastro)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<string> errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(errors);
            }

            UserRegisterResponse resultado = await _identityService.RegisterUser(usuarioCadastro);
            if (resultado.Success)
            {
                return Ok(resultado);
            }
            else if (resultado.Erros.Any())
            {
                return BadRequest(resultado);
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserRegisterResponse>> Login([FromBody] UserLoginViewModel usuarioLogin)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<string> errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(errors);
            }

            UserLoginResponse resultado = await _identityService.Login(usuarioLogin);
            if (resultado.Sucesso)
            {
                return Ok(resultado);
            }

            return Unauthorized(resultado);
        }

        [CustomAuthorize(UserRole.Admin | UserRole.Operador)]
        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetUsersWithRoles()
        {
            IEnumerable<UserResponseDto> usersWithRoles = await _identityService.GetUsersWithRolesAsync();

            return Ok(usersWithRoles);
        }

        [CustomAuthorize(UserRole.Admin)]
        [HttpPut("users/setrole/{login}")]
        public async Task<IActionResult> SetUserRole([FromBody] AddUserRoleViewModel addUserRole)
        {
            try
            {
                await _identityService.SetUserRoleAsync(addUserRole);
                return Ok(new { message = "Sucesso" });
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [CustomAuthorize(UserRole.Admin | UserRole.Operador)]
        [HttpDelete("users/{login}")]
        public async Task<IActionResult> RemoveUser(string login)
        {
            try
            {
                await _identityService.RemoveUserAsync(login);
                return Ok(new { message = "Sucesso" });
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
