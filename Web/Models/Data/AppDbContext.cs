using Microsoft.EntityFrameworkCore;
using System;
using Web.Models.Domain;

namespace Web.Models.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Product> Product => Set<Product>();

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
        
        }


    }
}