using System.Text.Json.Serialization;
using System.Text.Json;
using TicTacToeBlazorServer.GameLogic;
using TicTacToeBlazorServer.Models;

namespace TicTacToeBlazorServer.Components
{
    public class GameLogicCommand : TicTacToeGameLogic
    {
        public bool IsCreatorReady { get; set; }
        public bool IsJoinedReady { get; set; }
        public Player CurrentPlayerTurn { get; set; } = new();
        public string LastGameEndState { get; set; } = string.Empty;
        public bool IsGameStarted { get; set; }
        [JsonIgnore]
        public PlayerType[,] Board2D { get => Get2DBoard(); }

        public static GameLogicCommand DeserializeFromJson(string jsonString) => JsonSerializer.Deserialize<GameLogicCommand>(jsonString);
        public string SerializeToJson() => JsonSerializer.Serialize(this);
        public void StartNewGame(Player currentPlayerTurn)
        {
            if (IsCreatorReady && IsJoinedReady && !IsGameStarted)
            {
                CurrentPlayerTurn = currentPlayerTurn;
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
                    LastGameEndState = $"Winner is '{CurrentPlayerTurn.Name}'";
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
            CurrentPlayerTurn = null;
        }
        private PlayerType[,] Get2DBoard()
        {
            PlayerType[,] result = new PlayerType[3, 3];
            for (int i = 0; i < 9; i++)
            {
                result[i / 3, i % 3] = Board[i];
            }
            return result;
        }
    }
}
