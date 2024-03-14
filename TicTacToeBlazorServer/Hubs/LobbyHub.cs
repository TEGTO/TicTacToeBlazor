using Microsoft.AspNetCore.SignalR;
using TicTacToeBlazorServer.Models;

namespace TicTacToeBlazorServer.Hubs
{
    public class LobbyHub : Hub
    {
        public async Task UpdateLobbyState(Lobby lobby, string GameData)
        {
            await Clients.Others.SendAsync("ReceiveUpdateLobbyState", lobby, GameData);
        }
        public async Task AskGameData(Lobby lobby)
        {
            await Clients.Others.SendAsync("ResponseAskGameData", lobby);
        }
    }
}
