using Microsoft.EntityFrameworkCore;
using MongoServicesAPI.Models;

namespace MongoServicesAPI.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
