using Microsoft.EntityFrameworkCore;
using NetProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetProject.Context
{
    public class OurDbContext : DbContext
    {
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillDetail> BillDetails { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<TypeProduct> TypeProducts { get; set; }
        public DbSet<User> Users { get; set; }
        public OurDbContext(DbContextOptions<OurDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>().ToTable("comment");
            modelBuilder.Entity<Product>().ToTable("product");
            modelBuilder.Entity<TypeProduct>().Ignore(tp => tp.CategoryName).ToTable("type");
            modelBuilder.Entity<Category>().ToTable("category");
            modelBuilder.Entity<Slider>().ToTable("slide");
            modelBuilder.Entity<BillDetail>().ToTable("bill_detail");
            modelBuilder.Entity<Bill>().ToTable("bill");
            modelBuilder.Entity<User>().ToTable("user");
        }
    }
}
