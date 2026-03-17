namespace API.Models
{
	public class CartItem
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public int Amount { get; set; } = 0;
		public DateTime AddedAt { get; set; } = DateTime.UtcNow;

		// Foreign key to Cart
		public Guid CartId { get; set; }
		public Cart Cart { get; set; } = null!;

		// Foreign key to Product
		public Guid ProductId { get; set; }
		public Product Product { get; set; } = null!;
	}
}
