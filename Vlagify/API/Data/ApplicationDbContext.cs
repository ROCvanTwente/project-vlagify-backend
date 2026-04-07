using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
        public DbSet<ContactMessage> ContactMessages { get; set; }
        // --- Productgerelateerde tabellen ---
        public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Flagtype> Flagtypes { get; set; }
		public DbSet<MountingMethod> MountingMethods { get; set; }
		public DbSet<Review> Reviews { get; set; }

		// --- Shopping Cart & Orders ---
		public DbSet<Cart> Carts { get; set; }
		public DbSet<CartItem> CartProducts { get; set; }
		public DbSet<OrderHeader> OrderHeaders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }

		// --- Informatieve tabellen ---
		public DbSet<FlagInstructions> FlagInstructions { get; set; }
		public DbSet<NationalDays> NationalDays { get; set; }
		public DbSet<MaterialInstructions> MaterialInstructions { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			// Zorg dat prijzen en totalen altijd 2 decimalen hebben in de database
			builder.Entity<Product>()
				.Property(p => p.Price)
				.HasPrecision(18, 2);

			builder.Entity<OrderHeader>()
				.Property(o => o.OrderTotal)
				.HasPrecision(18, 2);

			builder.Entity<OrderDetail>()
				.Property(o => o.Price)
				.HasPrecision(18, 2);

			// Als je FlagInstructions.Country als Primary Key gebruikt:
			builder.Entity<FlagInstructions>()
				.HasKey(f => f.CountryCode);
		}
	}
}