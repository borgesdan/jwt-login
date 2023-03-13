using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.Application.Data.Entities
{
    public class AdvertisementEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(120)]        
        public string? Title { get; set; }

        [Required]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }

        [Range(0.0, 1.0)]
        public double Discount { get; set; }

        [Required]
        [StringLength(5000)]
        public string? Description { get; set; }

        [ForeignKey(nameof(Person))]
        public int SellerId { get; set; }

        public virtual PersonEntity? Person { get; set; }
    }
}
