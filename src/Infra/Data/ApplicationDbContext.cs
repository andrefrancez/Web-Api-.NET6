using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using WebApiUdemy.Domain.Products;

namespace WebApiUdemy.Infra.Data;

public class ApplicationDbContext : DbContext {
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder) {
        builder.Ignore<Notification>();

        builder.Entity<Product>().Property(p => p.Name).IsRequired();
        builder.Entity<Product>().Property(p => p.Description).HasMaxLength(250).IsRequired(false);
        builder.Entity<Category>().Property(p => p.Name).IsRequired();
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configuration) {
        configuration.Properties<string>().HaveMaxLength(100);
    }
}
