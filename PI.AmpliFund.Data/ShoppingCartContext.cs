using Microsoft.EntityFrameworkCore;

namespace PI.AmpliFund.Data;

public class ShoppingCartContext: DbContext
{
    public ShoppingCartContext() { }
    
    public ShoppingCartContext(DbContextOptions<ShoppingCartContext> options)
        : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShoppingCart>()
            .Property(sc => sc.RowVersion)
            .IsRowVersion();

        modelBuilder.Entity<ShoppingCartItem>()
            .Property(sc => sc.RowVersion)
            .IsRowVersion();
    }
    public virtual DbSet<ApplicationUser> ApplicationUser { get; set; }
    public virtual DbSet<ShoppingCart> ShoppingCart { get; set; }
    public virtual DbSet<ShoppingCartItem> ShoppingCartItem { get; set; }
    public virtual DbSet<Product> Product { get; set; }
    public virtual DbSet<StoreMembership> StoreMembership { get; set; }
}