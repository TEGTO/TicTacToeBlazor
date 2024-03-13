using TicTacToeBlazorServer.Models;

namespace TicTacToeBlazorServer.Services
{
    public interface ILobbyService
    {
        public IEnumerable<Lobby> GetLobbies();
        public void CreateLobby(Lobby lobby);
        public Lobby GetLobbyByPlayerId(string playerId);
        public Player GetPlayerById(string playerId);
        public void JoinLobby(Lobby lobby, Player player);
        public void LeaveLobby(Lobby lobby, Player player);
        public void UpdateLobby(Lobby lobby);
    }
}
