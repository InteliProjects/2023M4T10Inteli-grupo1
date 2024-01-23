namespace BackendIotvos.Authentication.DTOs
{
    public class UserLoginResponse
    {
        public bool Sucesso { get; private set; }

        public string Token { get; private set; }

        public DateTime? ExpirationDate { get; private set; }
        public List<string> Erros { get; private set; }

        public UserLoginResponse(bool sucesso = false, string token = null, DateTime? expirationDate = null)
        {
            Sucesso = sucesso;
            Token = token;
            ExpirationDate = expirationDate;
            Erros = new List<string>();
        }

        public void AddError(string erro)
        {
            Erros.Add(erro);
        }
    }
}
