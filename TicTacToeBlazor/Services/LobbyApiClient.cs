using System.Text.Json;
using TicTacToeBlazor.Models;

namespace TicTacToeBlazor.Services
{
    public class LobbyApiClient : ILobbyApiClient
    {
        private readonly HttpClient httpClient;

        public LobbyApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<Lobby>> GetLobbies()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<Lobby>>("Lobbies");
        }
        public async Task CreateLobby(Lobby lobby)
        {
            await httpClient.PostAsJsonAsync("Lobbies/createlobby", lobby);
        }
        public async Task<Lobby> GetLobbyByPlayerId(string playerId)
        {
            Lobby lobby;
            try
            {
                lobby = await httpClient.GetFromJsonAsync<Lobby>($"Lobbies/lobby/{playerId}");
            }
            catch (HttpRequestException)
            {
                lobby = null;
            }
            return lobby;
        }
        public async Task<Player> GetPlayerById(string playerId)
        {
            Player player;
            try
            {
                player = await httpClient.GetFromJsonAsync<Player>($"Lobbies/player/{playerId}");
            }
            catch (HttpRequestException)
            {
                player = null;
            }
            return player;
        }
        public async Task JoinLobby(Lobby lobby, Player player)
        {
            await httpClient.PatchAsJsonAsync("Lobbies/joinlobby", new LobbyDto { Lobby = lobby, Player = player });
        }
        public async Task LeaveLobby(Lobby lobby, Player player)
        {
            await httpClient.PatchAsJsonAsync("Lobbies/leavelobby", new LobbyDto { Lobby = lobby, Player = player });
        }
        public async Task UpdateLobby(Lobby lobby)
        {
            await httpClient.PutAsJsonAsync("Lobbies/updatelobby", lobby);
        }
    }
}
