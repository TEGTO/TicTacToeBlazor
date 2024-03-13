using Microsoft.EntityFrameworkCore;
using TicTacToeBlazorServer.Models;

namespace TicTacToeBlazorServer.Data
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
