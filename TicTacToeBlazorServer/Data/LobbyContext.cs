using Microsoft.EntityFrameworkCore;
using TicTacToeBlazor.Models;

namespace TicTacToeBlazor.Data
{
    public class LobbyContext : DbContext
    {
        public DbSet<Lobby> Lobbies { get; set; } = null!;
        public DbSet<Player> Players { get; set; } = null!;
        public LobbyContext(DbContextOptions<LobbyContext> options) : base(options)
        {
        }
    }
}
