using DotnetCoreRabbitMqSample.Api.Model;
using Microsoft.EntityFrameworkCore;

namespace DotnetCoreRabbitMqSample.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Message> Messages { get; set; }

    }
}
