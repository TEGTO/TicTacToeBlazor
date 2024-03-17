using TicTacToeBlazor.GameLogic;
using TicTacToeBlazor.Models;

namespace TicTacToeBlazorServer.GameLogic
{
    public class GameLogicCommand : TicTacToeGameLogic, IGameLogicCommand
    {
        public bool IsCreatorReady { get; set; }
        public bool IsJoinedReady { get; set; }
        public string LastGameEndState { get; set; } = string.Empty;
        public bool IsGameStarted { get; set; }
        public Player CurrentPlayerTurn { get; set; } = new();
        public int CreatorScore { get; set; }
        public int JoinedScore { get; set; }

        public void StartNewGame(Player playerWhoBegin)
        {
            if (IsCreatorReady && IsJoinedReady && !IsGameStarted)
            {
                CurrentPlayerTurn = playerWhoBegin;
                OnGameStart();
                StartGame();
            }
        }
        public void OnMove(int row, int col, string idInCookies, Lobby currentLobby)
        {
            if (CurrentPlayerTurn.Id == idInCookies && MakeMove(row, col))
            {
                if (CheckDraw())
                    LastGameEndState = "Draw";
                else if (CheckWin())
                {
                    LastGameEndState = $"Winner is '{CurrentPlayerTurn.Name}'";
                    if (CurrentPlayerTurn.Id == currentLobby.Creator.Id)
                        CreatorScore++;
                    else
                        JoinedScore++;
                }
                else
                {
                    CurrentPlayerTurn = CurrentPlayerTurn.Id == currentLobby.Creator.Id ? currentLobby.JoinedPlayer : currentLobby.Creator;
                    return;
                }
                OnGameEnd();
            }
        }
        public void RestartGame()
        {
            CreatorScore = 0;
            JoinedScore = 0;
            OnGameStart();
            OnGameEnd();
        }
        private void OnGameStart()
        {
            IsGameStarted = true;
            LastGameEndState = string.Empty;
            Board = new PlayerType[9];
        }
        private void OnGameEnd()
        {
            IsGameStarted = false;
            IsCreatorReady = false;
            IsJoinedReady = false;
            CurrentPlayerTurn = new Player();
        }
    }
}
