using TicTacToeBlazor.Models;

namespace TicTacToeBlazor.Services
{
    public interface ILobbyApiClient
    {
        Task<IEnumerable<Lobby>> GetLobbies();
        Task CreateLobby(Lobby lobby);
        Task<Lobby> GetLobbyByPlayerId(string playerId);
        Task<Player> GetPlayerById(string playerId);
        Task JoinLobby(Lobby lobby, Player player);
        Task LeaveLobby(Lobby lobby, Player player);
        Task UpdateLobby(Lobby lobby);
    }
}
