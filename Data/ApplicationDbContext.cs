using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Models;

namespace ProductCatalog.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>()
                .HasData(new Category[]
                {
                    new Category { Id = 1, Name="Mobile Accessories"},
                    new Category { Id = 2, Name="PC Accessories"},
                    new Category { Id = 3, Name="MobileS"},
                    new Category { Id = 4, Name="PCs"},
                });
        }

        public DbSet<User> Users {  get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
