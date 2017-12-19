using CheeseMVC.Data;
using CheeseMVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.EntityFrameworkCore.Infrastructure;


namespace CheeseMVC.Data
{
    public class CheeseDbContext : DbContext
    {
        public DbSet<Cheese> Cheeses { get; set; }
        public DbSet<CheeseCategory> Categories { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<CheeseMenu> CheeseMenus { get; set; }

        public CheeseDbContext(DbContextOptions<CheeseDbContext> options) 
            : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CheeseMenu>()
                .HasKey(c => new { c.CheeseID, c.MenuID });
        }
    }
}

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<CheeseDbContext>
{
    public CheeseDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

        var builder = new DbContextOptionsBuilder<CheeseDbContext>();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        builder.UseSqlServer(connectionString);

        return new CheeseDbContext(builder.Options);
    }
}

