using Microsoft.AspNetCore.Identity;

namespace BackendIotvos.Authentication
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
    }
}
