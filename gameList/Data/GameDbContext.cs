using gameList.Models;
using Microsoft.EntityFrameworkCore;

namespace gameList.Data
{
    public class GameDbContext : DbContext
    {
        public GameDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Game> Games { get; set; }
    }
}
