using TicTacToeBlazor.GameLogic;
using TicTacToeBlazor.Models;

namespace TicTacToeBlazorServer.GameLogic
{
    public interface IGameLogicCommand
    {
        public bool IsCreatorReady { get; set; }
        public bool IsJoinedReady { get; set; }
        public string LastGameEndState { get; set; }
        public bool IsGameStarted { get; set; }
        public int CreatorScore { get; set; }
        public int JoinedScore { get; set; }
        public Player CurrentPlayerTurn { get; set; }
        public PlayerType CurrentPlayerSymbol { get; set; }
        public PlayerType[] Board { get; set; }

        public void StartNewGame(Player playerWhoBegin);
        public void OnMove(int row, int col, string idInCookies, Lobby currentLobby);
        public void RestartGame();
    }
}
