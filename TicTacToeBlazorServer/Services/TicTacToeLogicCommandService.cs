using TicTacToeBlazorServer.GameLogic;

namespace TicTacToeBlazorServer.Services
{
    public class TicTacToeLogicCommandService : IGameLogicCommandService
    {
        private Dictionary<string, IGameLogicCommand> LobbiesGames = new Dictionary<string, IGameLogicCommand>();

        public IGameLogicCommand GetCommand(string currentLobbyId)
        {
            CheckIfGameNullAndCreateNewGame(currentLobbyId);
            return LobbiesGames[currentLobbyId];
        }
        public void UpdateCommand(string currentLobbyId, IGameLogicCommand gameLogicCommand)
        {
            if (LobbiesGames.ContainsKey(currentLobbyId))
                LobbiesGames[currentLobbyId] = gameLogicCommand;
        }
        private void CheckIfGameNullAndCreateNewGame(string currentLobby)
        {
            if (!LobbiesGames.ContainsKey(currentLobby))
                LobbiesGames[currentLobby] = new GameLogicCommand();
        }
    }
}
