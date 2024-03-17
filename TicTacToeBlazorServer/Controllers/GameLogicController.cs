using Microsoft.AspNetCore.Mvc;
using TicTacToeBlazor.GameLogic;
using TicTacToeBlazor.Models;
using TicTacToeBlazorServer.GameLogic;
using TicTacToeBlazorServer.Services;

namespace TicTacToeBlazorServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameLogicController : ControllerBase
    {
        private IGameLogicCommandService gameLogicService;

        public GameLogicController(IGameLogicCommandService gameLogicService)
        {
            this.gameLogicService = gameLogicService;
        }
        [HttpGet]
        public IGameLogicCommand GetCommand([FromQuery] string currentLobby)
        {
            try
            {
                return gameLogicService.GetCommand(currentLobby);
            }
            catch (Exception)
            {
                return null;
            }
        }
        [HttpPut("updatecommand")]
        public IActionResult UpdateCommand([FromQuery] string currentLobby, [FromBody] GameLogicCommand  gameLogicCommand)
        {
            try
            {
                gameLogicService.UpdateCommand(currentLobby, gameLogicCommand);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error occured! {ex.Message}");
            }
            return Ok();
        }
    }
}
