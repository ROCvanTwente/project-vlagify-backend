namespace API.Models
{
	public class MountingMethod
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public string NameEnUs { get; set; } = string.Empty;
		public string NameNlNl { get; set; } = string.Empty;
		public string NameDeDe { get; set; } = string.Empty;
		public string NameFrFr { get; set; } = string.Empty;
		public List<Product> Products { get; set; } = new();
	}
}
