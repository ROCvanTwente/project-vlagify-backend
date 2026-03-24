namespace API.Models
{
	public class MaterialInstructions
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public string NameEnUs { get; set; } = string.Empty;
		public string NameNlNl { get; set; } = string.Empty;
		public string NameDeDe { get; set; } = string.Empty;
		public string NameFrFr { get; set; } = string.Empty;
		public string WashingInstructionsEnUs { get; set; } = string.Empty;
		public string WashingInstructionsNlNl { get; set; } = string.Empty;
		public string WashingInstructionsDeDe { get; set; } = string.Empty;
		public string WashingInstructionsFrFr { get; set; } = string.Empty;
		public string DryingInstructionsEnUs { get; set; } = string.Empty;
		public string DryingInstructionsNlNl { get; set; } = string.Empty;
		public string DryingInstructionsDeDe { get; set; } = string.Empty;
		public string DryingInstructionsFrFr { get; set; } = string.Empty;
		public string IroningInstructionsEnUs { get; set; } = string.Empty;
		public string IroningInstructionsNlNl { get; set; } = string.Empty;
		public string IroningInstructionsDeDe { get; set; } = string.Empty;
		public string IroningInstructionsFrFr { get; set; } = string.Empty;
		public string StorageInstructionsEnUs { get; set; } = string.Empty;
		public string StorageInstructionsNlNl { get; set; } = string.Empty;
		public string StorageInstructionsDeDe { get; set; } = string.Empty;
		public string StorageInstructionsFrFr { get; set; } = string.Empty;
	}
}
