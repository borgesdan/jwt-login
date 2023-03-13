using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.Application.Data.Entities
{
    public class PersonEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid Uid { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        public virtual UserEntity? User { get; set; }
        public virtual ICollection<AdvertisementEntity>? Advertisements { get; set; }

        public PersonEntity()
        {
            Uid = Guid.NewGuid();
        }
    }
}
