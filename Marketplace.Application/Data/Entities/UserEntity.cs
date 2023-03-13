using Marketplace.Application.Data.Shared;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.Application.Data.Entities
{
    public class UserEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid Uid { get; set; }

        [Required]
        [StringLength(256)]
        public string? PrimaryLogin { get; set; }
        
        [StringLength(256)]
        public string? SecondaryLogin { get; set; }

        [Required]
        [StringLength(256)]
        public string? Password { get; set; }

        [Required]
        public AccessLevelType AccessLevel { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }        

        public virtual PersonEntity? Person { get; set; }

        public UserEntity()
        {
            Uid = Guid.NewGuid();
        }
    }
}