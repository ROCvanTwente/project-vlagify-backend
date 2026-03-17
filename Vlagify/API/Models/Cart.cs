namespace API.Models
{
	public class Cart
	{
		public string Id { get; set; } = string.Empty;
		public List<CartItem> CartItems { get; set; } = new();

		// Foreign key to ApplicationUser
		public string UserId { get; set; } = string.Empty;
		public ApplicationUser User { get; set; } = null!;
	}
}
