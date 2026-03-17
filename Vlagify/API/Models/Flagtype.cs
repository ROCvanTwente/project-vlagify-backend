namespace API.Models
{
	public class Flagtype
	{
		public string Id { get; set; } = string.Empty;
		public string NameEnUs { get; set; } = string.Empty;
		public string NameNlNl { get; set; } = string.Empty;
		public string NameDeDe { get; set; } = string.Empty;
		public string NameFrFr { get; set; } = string.Empty;
		public List<Product> Products { get; set; } = new();
	}
}
