using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SnakeGame.Api.Data;
using SnakeGame.Api.Models;

namespace SnakeGame.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScoresController : ControllerBase
    {
        private readonly GameDbContext _context;

        public ScoresController(GameDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> SaveScore([FromBody] Score score)
        {
            if (string.IsNullOrWhiteSpace(score.PlayerName))
            {
                return BadRequest("Player name is required.");
            }

            score.Date = DateTime.UtcNow;
            _context.Scores.Add(score);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Score saved successfully!", id = score.Id });
        }

        [HttpGet("leaderboard")]
        public async Task<ActionResult<IEnumerable<Score>>> GetLeaderboard()
        {
            var topScores = await _context.Scores
                .OrderByDescending(s => s.Points)
                .Take(10)
                .ToListAsync();

            return Ok(topScores);
        }
    }
}
