using System.ComponentModel.DataAnnotations;

namespace TicTacToeBlazorServer.Models
{
    public class Player
    {
        [Key]
        public string Id { get; set; }
        [Required, MaxLength(16)]
        public string Name { get; set; }
    }
}
