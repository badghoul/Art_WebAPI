using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Art_WebAPI.Data
{
    /// <summary>
    /// custom DbContext for managing user authentication and authorization 
    /// </summary>
    public class AuthDbContext:IdentityDbContext<IdentityUser>
    {
        public AuthDbContext(DbContextOptions options) : base(options) { }
    }
}
