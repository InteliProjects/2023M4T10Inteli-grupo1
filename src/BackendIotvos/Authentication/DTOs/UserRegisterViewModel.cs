using System.ComponentModel.DataAnnotations;

namespace BackendIotvos.Authentication.DTOs
{
    public class UserRegisterViewModel
    {
        [Required(ErrorMessage = "O campo email é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo email é inválido")]

        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Password é obrigatório")]

        public string Password { get; set; }

        [Required(ErrorMessage = "Name é obrigatório")]
        public string Name { get; set; }
    }
}
