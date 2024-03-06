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
		}

		public DbSet<Product> products;
		public DbSet<Category> categories;
		public DbSet<Attribute> attributes;
		public DbSet<ProductUnit> productsUnit;
		
	}
	public class IdentityContext : IdentityDbContext<ApplicationUser>
	{
		public IdentityContext(DbContextOptions<IdentityContext> options)
			: base(options)
		{
		}
	}
}
