using System.ComponentModel.DataAnnotations;

namespace API.Contracts
{
    public class CreateProductDto
    {
        public int? Id { get; set; }
        [MaxLength(100)]
        public required string Name { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
        public required decimal Price { get; set; }
    }
}
