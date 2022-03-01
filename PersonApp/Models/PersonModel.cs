using System.ComponentModel.DataAnnotations;
namespace PersonApp.Models
{
    public class PersonModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        [MinLength(2, ErrorMessage = "First Name should have at least 2 characters")] 
        public string? FirstName { get; set; } = string.Empty;
        [Required]
        [MinLength(1, ErrorMessage = "Last Name cannot be empty")] 
        public string? LastName { get; set; } = string.Empty;
        [Required]
        [Range(18, 60, ErrorMessage = "Value of age must be between 18 and 60")] 
        public int? Age { get; set; }

    }
}
