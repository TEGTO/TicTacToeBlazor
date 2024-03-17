using System.ComponentModel.DataAnnotations;

namespace TicTacToeBlazor.Models
{
    public class Player
    {
        [Key]
        public string? Id { get; set; }
        [MaxLength(16)]
        public string? Name { get; set; }
    }
}
