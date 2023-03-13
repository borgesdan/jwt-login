using Marketplace.Application.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Application.Data.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<PersonEntity> Persons { get; set; }
        public DbSet<AdvertisementEntity> Advertisements { get; set; }

        public AppDbContext(DbContextOptions options) : base(options) 
        {             
        }
    }
}