using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
	public class OrderHeader
	{
		public Guid Id { get; set; } = Guid.NewGuid();

		public string? ApplicationUserId { get; set; }
		[ForeignKey("ApplicationUserId")]
		public ApplicationUser? ApplicationUser { get; set; }

		public DateTime OrderDate { get; set; } = DateTime.UtcNow;
		public DateTime? ShippingDate { get; set; }

		public decimal OrderTotal { get; set; } // decimal is nauwkeuriger dan double

		public string? OrderStatus { get; set; }
		public string? PaymentStatus { get; set; }
		public string? TrackingNumber { get; set; }
		public string? Carrier { get; set; }

		public DateTime? PaymentDate { get; set; }
		public DateTime? PaymentDueDate { get; set; }

		// Stripe/Payment Provider velden
		public string? SessionId { get; set; }
		public string? PaymentIntentId { get; set; }

		// Verzendgegevens (Snapshot op moment van aankoop)
		[Required] public string? PhoneNumber { get; set; }
		[Required] public string? StreetAddress { get; set; }
		[Required] public string? City { get; set; }
		[Required] public string? PostalCode { get; set; }
		[Required] public string? Name { get; set; }

		public List<OrderDetail> OrderDetails { get; set; } = new();
	}
}
