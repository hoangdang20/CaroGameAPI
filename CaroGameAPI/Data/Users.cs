using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaroGameAPI.Data
{
	[Table("Users")]
	public class Users
	{
		[Key]
		public Guid ID { get; set; }

		[Required, MaxLength(50)]
		public string Username { get; set; } = string.Empty;

		[Required, MaxLength(255)]
		public string PasswordHash { get; set; } = string.Empty;

		[Required, MaxLength(100)]
		public string Email { get; set; } = string.Empty;

		// Giá trị mặc định EloRating là 1000, mặc dù giá trị mặc định trong CSDL đã được chỉ định.
		public int EloRating { get; set; } = 1000;

		public DateTime CreateAt { get; set; } = DateTime.Now;

		// Navigation properties (nếu cần thiết)
		public ICollection<Rooms>? OwnedRooms { get; set; }
		[InverseProperty("Player1")]
		public ICollection<GameMatches>? MatchesAsPlayer1 { get; set; }
		[InverseProperty("Player2")]
		public ICollection<GameMatches>? MatchesAsPlayer2 { get; set; }
		[InverseProperty("Winner")]
		public ICollection<GameMatches>? MatchesAsWinner { get; set; }
		public Ranking? Ranking { get; set; }
	}
}
