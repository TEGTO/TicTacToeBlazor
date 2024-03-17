using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicTacToeBlazor.Models
{
    public class Lobby
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }
        [ForeignKey("CreatorId")]
        public virtual Player? Creator { get; set; } = null!;
        [ForeignKey("JoinedPlayerId")]
        public virtual Player? JoinedPlayer { get; set; }
        public bool IsWaitingForPlayer { get; set; } = true;
    }
}
