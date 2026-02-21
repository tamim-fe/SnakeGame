using Microsoft.EntityFrameworkCore;
using SnakeGame.Api.Models;

namespace SnakeGame.Api.Data
{
    public class GameDbContext : DbContext
    {
        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options) { }

        public DbSet<Score> Scores { get; set; }
    }
}
