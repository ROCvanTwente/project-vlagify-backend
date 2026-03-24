using System.ComponentModel.DataAnnotations;

namespace API.Models
{
	public class FlagInstructions
	{
		[Key]
		public string CountryCode { get; set; } = string.Empty;
		public string CountryNameEnUs { get; set; } = string.Empty;
		public string CountryNameNlNl { get; set; } = string.Empty;
		public string CountryNameDeDe { get; set; } = string.Empty;
		public string CountryNameFrFr { get; set; } = string.Empty;
		public string MourningPortocolEnUs { get; set; } = string.Empty;
		public string MourningPortocolNlNl { get; set; } = string.Empty;
		public string MourningPortocolDeDe { get; set; } = string.Empty;
		public string MourningPortocolFrFr { get; set; } = string.Empty;
		public string GeneralRulesEnUs { get; set; } = string.Empty;
		public string GeneralRulesNlNl { get; set; } = string.Empty;
		public string GeneralRulesDeDe { get; set; } = string.Empty;
		public string GeneralRulesFrFr { get; set; } = string.Empty;
		public List<NationalDays> NationalDays { get; set; } = new();
	}
}
