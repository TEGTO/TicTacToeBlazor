using TicTacToeBlazorServer.GameLogic;

namespace TicTacToeBlazorServer.Services
{
    public interface IGameLogicCommandService
    {
        public IGameLogicCommand GetCommand(string currentLobbyId);
        public void UpdateCommand(string currentLobbyId, IGameLogicCommand gameLogicCommand);
    }
}
