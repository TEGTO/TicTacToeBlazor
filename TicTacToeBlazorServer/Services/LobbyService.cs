using Microsoft.EntityFrameworkCore;
using TicTacToeBlazor.Data;
using TicTacToeBlazor.Models;

namespace TicTacToeBlazor.Services
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
            if (!CheckLobbyCondtions(lobby))
                throw new InvalidDataException();
            if(GetLobbyByPlayerId(lobby.Creator.Id) == null)
            {
                lobbyContext.Lobbies.Add(lobby);
                SaveLobbiesState();
            }
        }
        public IEnumerable<Lobby> GetLobbies()
        {
            return lobbyContext.Lobbies.Include(l => l.Creator).Include(l => l.JoinedPlayer).ToList();
        }
        public void UpdateLobby(Lobby lobby)
        {
            Lobby l = GetLobbyById(lobby.Id);
            if (l != null)
                lobbyContext.Entry(l).Reload();
        }
        public void JoinLobby(Lobby l, Player player)
        {
            if (!CheckLobbyCondtions(l))
                throw new InvalidDataException();
            Lobby lobby = GetLobbyById(l.Id);
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
        public void LeaveLobby(Lobby l, Player p)
        {
            if (CheckLobbyCondtions(l))
            {
                Lobby lobby = GetLobbyById(l.Id);
                Player player = GetPlayerById(p.Id);
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
        private Lobby GetLobbyById(string lobbyId) =>
            GetLobbies().FirstOrDefault(x => x.Id == lobbyId);
        private void SaveLobbiesState() => lobbyContext.SaveChanges();
        private void DeleteLobby(Lobby l)
        {
            if (CheckLobbyCondtions(l))
            {
                Lobby lobby = GetLobbyById(l.Id);
                DeleteCreatorWithoutSaving(lobby);
                if (lobby.JoinedPlayer != null)
                    DeleteJoinedWithoutSaving(lobby);
                lobbyContext.Lobbies.Remove(lobby);
                SaveLobbiesState();
            }
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
        private bool CheckLobbyCondtions(Lobby lobby)
        {
            if (lobby == null ||
               lobby.Creator == null ||
               string.IsNullOrEmpty(lobby.Creator.Id)) return false;
            return true;
        }
    }
}