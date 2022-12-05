using Identity.API.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Identity.API.DAL;

public class IdentityDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;

    public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
        : base(options)
    {
    }
}