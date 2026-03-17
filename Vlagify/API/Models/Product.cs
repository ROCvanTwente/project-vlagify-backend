namespace API.Models
{
	public class Product
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public string NameEnUs { get; set; } = string.Empty;
		public string NameNlNL { get; set; } = string.Empty;
		public string NameDeDe { get; set; } = string.Empty;
		public string NameFrFr { get; set; } = string.Empty;
		public string DescriptionEnUs { get; set; } = string.Empty;
		public string DescriptionNlNl { get; set; } = string.Empty;
		public string DescriptionDeDe { get; set; } = string.Empty;
		public string DescriptionFrFr { get; set; } = string.Empty;
		public decimal Width { get; set; } = 0;
		public decimal Height { get; set; }  = 0;
		public decimal Price { get; set; } = 0;
		public int Stock { get; set; } = 0;
		public string Image { get; set; } = string.Empty;
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
		public List<CartItem> CartItems { get; set; } = new();
		public List<OrderDetail> OrderItems { get; set; } = new();

		// Foreign key to Category
		public Guid CategoryId { get; set; }
		public Category Category { get; set; } = null!;

		// Foreign key to Flagtype
		public Guid FlagtypeId { get; set; }
		public Flagtype Flagtype { get; set; } = null!;

		// Foreign key to MountingMethod
		public Guid MountingMethodId { get; set; }
		public MountingMethod MountingMethod { get; set; } = null!;
	}
}
