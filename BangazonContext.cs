using Bangazon.Models;
using Microsoft.EntityFrameworkCore;
namespace Bangazon.Models
{
    public class BangazonDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Ordered_Products> ordered_Products { get; set; }
        public DbSet<User> users { get; set; }
        public BangazonDbContext(DbContextOptions<BangazonDbContext> context) : base(context) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ordered_Products>().HasKey(op => new
            {
                op.ProductId,
                op.OrderId,
            });
            modelBuilder.Entity<Ordered_Products>().HasData(new Ordered_Products[]
            {
                new Ordered_Products()
                        {
                            OrderId = 1,
                            ProductId = 1,
                        }
            });
            modelBuilder.Entity<Ordered_Products>().HasOne(p => p.Product).WithMany(op => op.ordered_Products).HasForeignKey(p => p.ProductId);
            modelBuilder.Entity<Ordered_Products>().HasOne(o => o.Order).WithMany(op => op.ordered_Products).HasForeignKey(o => o.OrderId);

            modelBuilder.Entity<User>().HasData(new User[]
            {
                new User()
                        {
                            Id = "1",
                            Email = "Email@email.com",
                            FirstName = "Test",
                            LastName = "Testerson",
                            Address = "123 Test Road",
                            IsSeller = true,
                        },
                        new User()
                        {
                            Id = "2",
                            Email = "email@notEmail.com",
                            FirstName = "Bob",
                            LastName = "Bobberson",
                            Address = "123 Not Test Road",
                            IsSeller = false,
                        }
            });
            modelBuilder.Entity<Order>().HasData(new Order[]
            {
                 new Order()
                        {
                            Id = 1,
                            CustomerId  = "1",
                            DatePlaced = DateTime.Now,
                            DateShipped = DateTime.Now,
                            OrderStatus = OrderStatus.Pending,
                        },
                        new Order()
                        {
                            Id = 2,
                            CustomerId = "2",
                            DatePlaced = DateTime.Now,
                            DateShipped = DateTime.Now,
                            OrderStatus = OrderStatus.Shipped,
                        }
            });
            modelBuilder.Entity<Product>().HasData(new Product[]
            {
                new Product()
                        {
                            Id = 1,
                            SellerId = "1",
                            DateAdded = DateTime.Now,
                            InStock = true,
                            Description = "This is a  Electronic Product",
                            ImageUrl = "this is url",
                            ProductType = ProductType.Electronics,
                            Price = 19.99M,
                        }
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}

