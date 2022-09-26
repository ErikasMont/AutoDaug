using Microsoft.EntityFrameworkCore;
using AutoDaug.Models;

namespace AutoDaug.DataContext
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext()
        {
        }

        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }

        public DbSet<AdvertType> AdvertTypes { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Advert> Adverts { get; set; }

        public DbSet<Car> Cars { get; set; }
    }
}
