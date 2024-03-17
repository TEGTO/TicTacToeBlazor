using TicTacToeBlazor.GameLogic;
using TicTacToeBlazor.Models;
using TicTacToeBlazorServer.GameLogic;

namespace TicTacToeBlazor.Services
{
    public interface IGameLogicApiClient
    {
        Task<IGameLogicCommand> GetCommand(string currentLobby);
        Task UpdateCommand(string currentLobby, IGameLogicCommand gameLogicCommand);
    }
}
