using CommonCrm.Data.Entities.AppUser;
using CommonCrm.Data.Entities.Product;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonCrm.Data.Entities;
using CommonCrm.Data.Entities.Offer;
using Microsoft.AspNetCore.Identity;
using Attribute = CommonCrm.Data.Entities.Product.Attribute;

namespace CommonCrm.Data.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
   .HasMany(p => p.Categories)
   .WithMany(c => c.Products);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Collections)
                .WithMany(c => c.Products);

            modelBuilder.Entity<CategoryProduct>()
                .HasKey(cp => new { cp.CategoryId, cp.ProductId });

            modelBuilder.Entity<CollectionProduct>()
                .HasKey(cp => new { cp.CollectionId, cp.ProductId });



        }

        //public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<OfferProduct> OfferProducts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Attribute> Attributes { get; set; }
        public DbSet<ProductUnit> ProductsUnit { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<ExchangeRate> ExchangeRates { get; set; }
        public DbSet<CategoryProduct> CategoryProducts { get; set; }
        public DbSet<CollectionProduct> CollectionProducts { get; set; }
        public DbSet<CountryStates> CountryStates { get; set; }
        public DbSet<Country> Countries { get; set; }

    }
    public class IdentityContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {
        }
    }
}
