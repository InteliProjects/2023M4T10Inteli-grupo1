using System.Collections.Generic;

namespace BackendIotvos.Authentication.DTOs
{
    public class UserRegisterResponse
    {
        public bool Success { get; set; }
        public List<string> Erros { get; private set; }

        public UserRegisterResponse(bool success = true)
        {
            Success = success;
            Erros = new List<string>();
        }

        public void AdicionarErros(IEnumerable<string> erros)
        {
            Erros.AddRange(erros);
        }
    }
}
