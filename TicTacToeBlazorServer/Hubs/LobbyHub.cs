using Microsoft.AspNetCore.SignalR;
using TicTacToeBlazorServer.Models;

namespace TicTacToeBlazorServer.Hubs
{
    public class LobbyHub : Hub
    {
        public async Task UpdateLobbyState(Lobby lobby)
        {
            await Clients.All.SendAsync("ReceiveUpdateLobbyState", lobby);
        }
        public async Task UpdateGameData(string GameData, Lobby lobby)
        {
            await Clients.All.SendAsync("ReceiveUpdateGameData", GameData, lobby);
        }
        public async Task AskGameData(Lobby lobby)
        {
            await Clients.All.SendAsync("ResponseAskGameData", lobby);
        }
    }
}
