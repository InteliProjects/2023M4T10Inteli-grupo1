using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BackendIotvos.Authentication.Data
{
    public class IdentityDataContext : IdentityDbContext<User>
    {
        public IdentityDataContext() { }

        public IdentityDataContext(DbContextOptions<IdentityDataContext> options) : base(options) { }
    }
}
