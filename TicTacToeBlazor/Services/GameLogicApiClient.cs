using TicTacToeBlazorServer.GameLogic;

namespace TicTacToeBlazor.Services
{
    public class GameLogicApiClient : IGameLogicApiClient
    {
        private readonly HttpClient httpClient;

        public GameLogicApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IGameLogicCommand> GetCommand(string currentLobby)
        {
            return await httpClient.GetFromJsonAsync<GameLogicCommand>($"GameLogic?currentLobby={currentLobby}");
        }
        public async Task UpdateCommand(string currentLobby, IGameLogicCommand gameLogicCommand)
        {
            await httpClient.PutAsJsonAsync($"GameLogic/updatecommand?currentLobby={currentLobby}", gameLogicCommand);
        }
    }
}
