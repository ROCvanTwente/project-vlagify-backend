namespace API.Models
{
	public class Review
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public string Comment { get; set; } = string.Empty;
		public int Rating { get; set; } = 0;
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		// Foreign key to Product
		public string ProductId { get; set; } = string.Empty;
		public Product Product { get; set; } = null!;

		// Foreign key to ApplicationUser
		public string UserId { get; set; } = string.Empty;
		public ApplicationUser User { get; set; } = null!;
	}
}
