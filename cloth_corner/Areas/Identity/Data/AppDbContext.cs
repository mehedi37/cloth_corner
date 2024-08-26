using cloth_corner.Areas.Identity.Data;
using cloth_corner.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace cloth_corner.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Products> Products { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartDetails> CartDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure cascade delete for Cart -> CartDetails
            modelBuilder.Entity<Cart>()
                .HasMany(c => c.CartDetails)
                .WithOne(cd => cd.Cart)
                .HasForeignKey(cd => cd.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure restrict delete for Products -> CartDetails
            modelBuilder.Entity<CartDetails>()
               .HasOne(cd => cd.Products)
               .WithMany()
               .HasForeignKey(cd => cd.ProductId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
