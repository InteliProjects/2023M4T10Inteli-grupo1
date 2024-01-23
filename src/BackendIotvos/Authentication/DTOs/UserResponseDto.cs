namespace BackendIotvos.Authentication.DTOs
{
    public class UserResponseDto
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public List<string> Roles { get; set; }
    }
}
