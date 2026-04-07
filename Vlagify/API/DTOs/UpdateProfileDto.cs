using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class UpdateProfileDto
    {
        public string? FirstName { get; set; }

        public string? Infix { get; set; }

        public string? LastName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public string? PostalCode { get; set; }

        public string? City { get; set; }

        public string? Country { get; set; }
    }
}
