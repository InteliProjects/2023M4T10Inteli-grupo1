using Microsoft.AspNetCore.Authorization;

namespace BackendIotvos.Authentication.Configurations
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public CustomAuthorizeAttribute(UserRole roleEnum)
        {
            Roles = roleEnum.ToString().Replace(" ", string.Empty);
        }
    }
}
