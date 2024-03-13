using System.Text.Json.Serialization;

namespace TicTacToeBlazorServer.Components
{
    public enum PlayerType
    {
        Empty, XPlayer, OPlayer
    }
    public class TicTacToeGameLogic
    {
        private PlayerType[] board = new PlayerType[9];
        private PlayerType currentPlayer = PlayerType.XPlayer;

        public PlayerType[] Board1D { get => board; private set => board = value; }
        [JsonIgnore]
        public PlayerType[,] Board { get => Get2DBoard(); }
        public bool IsGameStarted {  get; private set; }

        public bool MakeMove(int row, int col)
        {
            if (row < 0 || row >= 3 || col < 0 || col >= 3 || Board[row, col] != PlayerType.Empty)
                return false; 
            Board[row, col] = currentPlayer;
            return true;
        }
        public bool CheckWin()
        {
            for (int i = 0; i < 3; i++)
            {
                if (Board[i, 0] != PlayerType.Empty && Board[i, 0] == Board[i, 1] && Board[i, 1] == Board[i, 2])
                    return true; // Row win
                if (Board[0, i] != PlayerType.Empty && Board[0, i] == Board[1, i] && Board[1, i] == Board[2, i])
                    return true; // Column win
            }
            if (Board[0, 0] != PlayerType.Empty && Board[0, 0] == Board[1, 1] && Board[1, 1] == Board[2, 2])
                return true; // Diagonal win
            if (Board[0, 2] != PlayerType.Empty && Board[0, 2] == Board[1, 1] && Board[1, 1] == Board[2, 0])
                return true; // Diagonal win
            return false;
        }
        public bool CheckDraw()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Board[i, j] == PlayerType.Empty)
                        return false; // Not a draw
                }
            }
            return !CheckWin(); 
        }
        public void SwitchPlayer()
        {
            currentPlayer = (currentPlayer == PlayerType.XPlayer) ? PlayerType.OPlayer : PlayerType.XPlayer;
        }
        public void StartGame()
        {
            Board1D = new PlayerType[9];
            currentPlayer = PlayerType.XPlayer;
            IsGameStarted = true;
        }
        private PlayerType[,] Get2DBoard()
        {
            PlayerType[,] result = new PlayerType[3, 3];
            for (int i = 0; i < 9; i++)
            {
                result[i / 3, i % 3] = Board1D[i];
            }
            return result;
        }

    }
}
