using Microsoft.AspNetCore.Identity;

namespace API.Models
{
	public class ApplicationUser : IdentityUser
	{
		public string FirstName { get; set; } = string.Empty;
		public string? Infix { get; set; }
		public string LastName { get; set; } = string.Empty;
		public string Address { get; set; } = string.Empty;
		public string PostalCode { get; set; } = string.Empty;
		public string City { get; set; } = string.Empty;
		public string Country { get; set; } = string.Empty;
		public Cart? Cart { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
	}
}
