using Microsoft.AspNetCore.SignalR;
using TicTacToeBlazor.Models;

namespace TicTacToeBlazor.Hubs
{
    public class LobbyHub : Hub
    {
        public async Task UpdateLobbyState(string senderId, Lobby lobby)
        {
            await Clients.Others.SendAsync("ReceiveUpdateLobbyState", senderId, lobby);
        }
        public async Task SendMessage(string lobbyId, string senderId, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", lobbyId, senderId, message);
        }
    }
}
