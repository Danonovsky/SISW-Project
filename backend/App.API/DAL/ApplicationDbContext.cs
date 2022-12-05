using App.API.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace App.API.DAL;

public class ApplicationDbContext : DbContext
{
    public DbSet<Application> Applications { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) {}
}