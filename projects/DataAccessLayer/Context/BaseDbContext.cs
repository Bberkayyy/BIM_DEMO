using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Context;

public class BaseDbContext : DbContext
{
    protected IConfiguration Configuration { get; set; }

    public BaseDbContext(DbContextOptions options, IConfiguration configuration) : base(options) => Configuration = configuration;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Basket>()
     .HasOne(b => b.User)
     .WithMany(u => u.Baskets)
     .HasForeignKey(b => b.UserCode)
     .HasPrincipalKey(u => u.UserCode);

        modelBuilder.Entity<BasketItem>()
    .HasOne(bi => bi.Basket)
    .WithMany(b => b.BasketItems)
    .HasForeignKey(bi => bi.BasketId)
    .OnDelete(DeleteBehavior.Cascade);

    }

    public DbSet<Category>? Categories { get; set; }
    public DbSet<Product>? Products { get; set; }
    public DbSet<Store>? Stores { get; set; }
    public DbSet<User>? Users { get; set; }
    public DbSet<Role>? Roles { get; set; }
    public DbSet<UserRole>? UserRoles { get; set; }
    public DbSet<Basket>? Baskets { get; set; }
    public DbSet<BasketItem>? BasketItems { get; set; }
}
