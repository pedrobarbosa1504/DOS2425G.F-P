using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{

    public DbSet<User> Users { get; set; }
    public DbSet<Project> Projects { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }
}
