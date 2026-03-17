namespace API.Models
{
	public class CartItem
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public int Amount { get; set; } = 0;
		public DateTime AddedAt { get; set; } = DateTime.UtcNow;

		// Foreign key to Cart
		public string CartId { get; set; } = string.Empty;
		public Cart Cart { get; set; } = null!;

		// Foreign key to Product
		public string ProductId { get; set; } = string.Empty;
		public Product Product { get; set; } = null!;
	}
}
