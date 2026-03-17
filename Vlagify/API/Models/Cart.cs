namespace API.Models
{
	public class Cart
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public List<CartItem> CartItems { get; set; } = new();

		// Foreign key to ApplicationUser
		public string UserId { get; set; } = string.Empty;
		public ApplicationUser User { get; set; } = null!;
	}
}
