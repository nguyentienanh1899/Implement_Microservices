using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.Product
{
    public abstract class CreateOrUpdateProductDto
    {
        [Required]
        [MaxLength(250, ErrorMessage = "Maximum length for Product Name is 250 characters.")]
        public string Name { get; set; }

        [MaxLength(250, ErrorMessage = "Maximum length for Product Summary is 250 characters.")]
        public string Summary { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal? Price { get; set; }

    }
}
