using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class eStoreContext : DbContext
    {
        public eStoreContext(DbContextOptions<eStoreContext> options) : base(options) { }

        public DbSet<Member> Members { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetail>()
                .HasKey(od => new { od.OrderId, od.ProductId });

            modelBuilder.Entity<Member>().HasData(
                new Member
                {
                    MemberId = 1,
                    Email = "admin@estore.com",
                    Password = "admin@@",
                    FullName = "Administrator",
                    Birthday = DateTime.Now
                }
            );
        }
    }
}