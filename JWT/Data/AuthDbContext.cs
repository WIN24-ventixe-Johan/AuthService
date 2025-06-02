using JWT.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JWT.Data;

public class AuthDbContext(DbContextOptions options) : IdentityDbContext<ApplicationUser>(options)
{

}

