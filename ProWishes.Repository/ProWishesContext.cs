using Microsoft.EntityFrameworkCore;
using ProWishes.Domain;

namespace ProWishes.Repository
{
    public class ProWishesContext : DbContext
    {
        public ProWishesContext(DbContextOptions<ProWishesContext> options) : base (options){ }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Wishe> Wishes { get; set; }
    }
}