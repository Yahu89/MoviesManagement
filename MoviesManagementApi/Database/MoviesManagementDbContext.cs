using Microsoft.EntityFrameworkCore;

namespace MoviesManagementApi.Database;

public class MoviesManagementDbContext : DbContext
{
    public MoviesManagementDbContext(DbContextOptions<MoviesManagementDbContext> options) : base(options)
    {
       
    }

    public DbSet<Movie> Movies { get; set; }

    protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data source=localhost\\SQLEXPRESS; Trusted_Connection=true; Initial Catalog=MoviesBase; TrustServerCertificate=true; Integrated Security=true");

    protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Movie>(x =>
        {
            x.Property(c => c.Title).IsRequired().HasMaxLength(200);
            x.HasIndex(x => x.Title).IsUnique();
        });
    }
}
