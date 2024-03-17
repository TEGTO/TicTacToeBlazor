using Microsoft.AspNetCore.Mvc;
using TicTacToeBlazor.Models;
using TicTacToeBlazor.Services;

namespace TicTacToeBlazorServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LobbiesController : ControllerBase
    {
        private ILobbyService lobbyService;

        public LobbiesController(ILobbyService lobbyService)
        {
            this.lobbyService = lobbyService;
        }

        [HttpGet]
        public IEnumerable<Lobby> GetLobbies()
        {
            return lobbyService.GetLobbies();
        }
        [HttpPost("createlobby")]
        public IActionResult CreateLobby([FromBody] Lobby lobby)
        {
            try
            {
                lobbyService.CreateLobby(lobby);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error occured! {ex.Message}");
            }
            return Ok();
        }
        [HttpGet("lobby/{playerId}")]
        public ActionResult<Lobby> GetLobbyByPlayerId(string playerId)
        {
            Lobby requstedLobby = lobbyService.GetLobbyByPlayerId(playerId);
            return requstedLobby == null ? NotFound() : requstedLobby;
        }
        [HttpGet("player/{playerId}")]
        public ActionResult<Player> GetPlayerById(string playerId)
        {
            Player requstedPlayer = lobbyService.GetPlayerById(playerId);
            return requstedPlayer == null ? NotFound() : requstedPlayer;
        }
        [HttpPatch("joinlobby")]
        public IActionResult JoinLobby([FromBody] LobbyDto lobbyDto)
        {
            try
            {
                lobbyService.JoinLobby(lobbyDto.Lobby, lobbyDto.Player);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error occured! {ex.Message}");
            }
            return Ok();
        }
        [HttpPatch("leavelobby")]
        public IActionResult LeaveLobby([FromBody] LobbyDto lobbyDto)
        {
            try
            {
                lobbyService.LeaveLobby(lobbyDto.Lobby, lobbyDto.Player);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error occured! {ex.Message}");
            }
            return Ok();
        }
        [HttpPut("updatelobby")]
        public IActionResult UpdateLobby([FromBody] Lobby lobby)
        {
            try
            {
                lobbyService.UpdateLobby(lobby);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error occured! {ex.Message}");
            }
            return Ok();
        }
    }
}
