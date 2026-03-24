using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
	public class NationalDays
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public DateOnly Date { get; set; } = DateOnly.MinValue;
		public string TitleEnUs { get; set; } = string.Empty;
		public string TitleNlNl { get; set; } = string.Empty;
		public string TitleDeDe { get; set; } = string.Empty;
		public string TitleFrFr { get; set; } = string.Empty;

		// Foreign key to Country
		public string CountryCode { get; set; } = string.Empty;
		[ForeignKey("CountryCode")]
		public FlagInstructions FlagInstructions { get; set; } = null!;

	}
}
