using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaroGameAPI.Data
{
	[Table("Moves")]
	public class Moves
	{
		[Key]
		public Guid ID { get; set; }

		[Required]
		public Guid MatchID { get; set; }

		[ForeignKey("MatchID")]
		public GameMatches? GameMatch { get; set; }

		[Required]
		public Guid PlayerID { get; set; }

		[ForeignKey("PlayerID")]
		public Users? Player { get; set; }

		[Required]
		public int X { get; set; }

		[Required]
		public int Y { get; set; }

		public DateTime CreateAt { get; set; } = DateTime.Now;
	}
}
