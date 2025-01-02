using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class Product : BaseEntity
    {
        [MaxLength(100)]
        public string? Name { get;private set; }
        [MaxLength(500)]
        public string? Description { get; private set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; private set; }
        public bool IsActive { get; private set; } = true;
        public void SetIsActive(bool active)
        {
            IsActive = active;
        }
    }
}
