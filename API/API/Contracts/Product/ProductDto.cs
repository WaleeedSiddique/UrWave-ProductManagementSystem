namespace API.Contracts
{
    public class ProductDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; private set; }
    }
}
