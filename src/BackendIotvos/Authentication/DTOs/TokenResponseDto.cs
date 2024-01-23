namespace BackendIotvos.Authentication.DTOs
{
    public class TokenResponseDto
    {
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }

        public TokenResponseDto(string token, DateTime expirationDate)
        {
            Token = token;
            ExpirationDate = expirationDate;
        }
    }
}
