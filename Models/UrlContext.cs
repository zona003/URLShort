using Microsoft.EntityFrameworkCore;

namespace URLShort.Models
{
    public class UrlContext : DbContext
    {
        public DbSet<Url> Urls { get; set; }

        public UrlContext(DbContextOptions<UrlContext> options) 
            : base(options) 
        {
            Database.EnsureCreated();
        }
    }
}
