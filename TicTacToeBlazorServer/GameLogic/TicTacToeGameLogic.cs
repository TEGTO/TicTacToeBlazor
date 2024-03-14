namespace TicTacToeBlazorServer.GameLogic
{
    public class TicTacToeGameLogic
    {
        public PlayerType CurrentPlayerSymbol { get; set; } = PlayerType.XPlayer;
        public PlayerType[] Board { get; set; } = new PlayerType[9];

        protected bool MakeMove(int row, int col)
        {
            if (row < 0 || row >= 3 || col < 0 || col >= 3 || Board[row * 3 + col] != PlayerType.Empty)
                return false;
            Board[row * 3 + col] = CurrentPlayerSymbol;
            SwitchPlayer();
            return true;
        }
        protected bool CheckWin()
        {
            for (int i = 0; i < 3; i++)
            {
                if (Board[i * 3] != PlayerType.Empty && Board[i * 3] == Board[i * 3 + 1] && Board[i * 3 + 1] == Board[i * 3 + 2])
                    return true; // Row win
                if (Board[i] != PlayerType.Empty && Board[i] == Board[1 * 3 + i] && Board[1 * 3 + i] == Board[2 * 3 + i])
                    return true; // Column win
            }
            if (Board[0 * 3 + 0] != PlayerType.Empty && Board[0 * 3 + 0] == Board[1 * 3 + 1] && Board[1 * 3 + 1] == Board[2 * 3 + 2])
                return true; // Diagonal win
            if (Board[0 * 3 + 2] != PlayerType.Empty && Board[0 * 3 + 2] == Board[1 * 3 + 1] && Board[1 * 3 + 1] == Board[2 * 3 + 0])
                return true; // Diagonal win
            return false;
        }
        protected bool CheckDraw()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Board[i * 3 + j] == PlayerType.Empty)
                        return false; // Not a draw
                }
            }
            return !CheckWin();
        }
        protected void StartGame()
        {
            Board = new PlayerType[9];
            CurrentPlayerSymbol = PlayerType.XPlayer;

        }
        protected void SwitchPlayer()
        {
            CurrentPlayerSymbol = CurrentPlayerSymbol == PlayerType.XPlayer ? PlayerType.OPlayer : PlayerType.XPlayer;
        }
    }
}
