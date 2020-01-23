using Microsoft.EntityFrameworkCore;

namespace ServiceLayer.Models
{
    public class ServiceContext : DbContext
    {
        public ServiceContext(DbContextOptions<ServiceContext> options)
            : base(options)
        {
        }

        public DbSet<UsersData> Items { get; set; }

    }
}
