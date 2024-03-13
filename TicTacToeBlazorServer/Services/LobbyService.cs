using Microsoft.EntityFrameworkCore;
using TicTacToeBlazorServer.Data;
using TicTacToeBlazorServer.Models;

namespace TicTacToeBlazorServer.Services
{
    public class LobbyService : ILobbyService
    {
        private LobbyContext lobbyContext;

        public LobbyService(LobbyContext lobbyContext)
        {
            this.lobbyContext = lobbyContext;
        }
        public void CreateLobby(Lobby lobby)
        {
            lobbyContext.Lobbies.Add(lobby);
            SaveLobbiesState();
        }
        public IEnumerable<Lobby> GetLobbies()
        {
            return lobbyContext.Lobbies.Include(l => l.Creator).Include(l => l.JoinedPlayer).ToList();
        }
        public void UpdateLobby(Lobby lobby)
        {
            Lobby l = GetLobbies().FirstOrDefault(x => x.Id == lobby.Id);
            if (l != null)
                lobbyContext.Entry(l).Reload();
        }
        public void JoinLobby(Lobby lobby, Player player)
        {
            if (!lobby.Creator.Id.Equals(player.Id) && lobby.JoinedPlayer == null)
            {
                Lobby prevLobby = GetLobbyByPlayerId(player.Id);
                if (prevLobby != null)
                    LeaveLobby(prevLobby, player);
                lobby.JoinedPlayer = player;
                lobby.IsWaitingForPlayer = false;
                lobbyContext.Lobbies.Update(lobby);
                SaveLobbiesState();
            }
        }
        public void LeaveLobby(Lobby lobby, Player player)
        {
            if (lobby != null)
            {
                lobby.IsWaitingForPlayer = true;
                if (lobby.Creator.Id.Equals(player.Id))
                {
                    if (lobby.JoinedPlayer == null)
                    {
                        DeleteLobby(lobby);
                        return;
                    }
                    else
                    {
                        DeleteCreatorWithoutSaving(lobby);
                        lobby.Creator = lobby.JoinedPlayer;
                        lobby.JoinedPlayer = null;
                    }
                }
                else
                    DeleteJoinedWithoutSaving(lobby);
                lobbyContext.Lobbies.Update(lobby);
                SaveLobbiesState();
            }
        }
        public Lobby? GetLobbyByPlayerId(string playerId)
        {
            Lobby lobby = GetLobbies().FirstOrDefault(x => x.Creator.Id == playerId || (x.JoinedPlayer != null && x.JoinedPlayer.Id == playerId));
            if (lobby != null)
                lobbyContext.Entry(lobby).Reload();
            return lobby;
        }
        public Player GetPlayerById(string playerId) =>
            lobbyContext.Players.FirstOrDefault(x => x.Id == playerId);
        private void SaveLobbiesState() => lobbyContext.SaveChanges();
        private void DeleteLobby(Lobby lobby)
        {
            DeleteCreatorWithoutSaving(lobby);
            if (lobby.JoinedPlayer != null)
                DeleteJoinedWithoutSaving(lobby);
            lobbyContext.Lobbies.Remove(lobby);
            SaveLobbiesState();
        }
        private void DeleteCreatorWithoutSaving(Lobby lobby)
        {
            RemovePlayerByIdWithoutSaving(lobby.Creator.Id);
            lobby.Creator = null;
        }
        private void DeleteJoinedWithoutSaving(Lobby lobby)
        {
            RemovePlayerByIdWithoutSaving(lobby.JoinedPlayer.Id);
            lobby.JoinedPlayer = null;
        }
        private void RemovePlayerByIdWithoutSaving(string playerId) => lobbyContext.Players.Remove(lobbyContext.Players.Find(playerId));
    }
}