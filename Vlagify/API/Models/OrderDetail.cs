using API.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class OrderDetail
{
	public Guid Id { get; set; } = Guid.NewGuid();

	[Required]
	public Guid OrderId { get; set; } // Moet Guid zijn omdat Header Guid is
	[ForeignKey("OrderId")]
	public OrderHeader OrderHeader { get; set; } = null!;

	[Required]
	public string ProductId { get; set; } = string.Empty;
	[ForeignKey("ProductId")]
	public Product Product { get; set; } = null!;

	public int Count { get; set; }
	public decimal Price { get; set; } // Prijs op moment van verkoop
}