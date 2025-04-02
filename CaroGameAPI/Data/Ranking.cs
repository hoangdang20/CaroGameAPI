using CaroGameAPI.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaroGameAPI.Data
{
	[Table("Ranking")]
	public class Ranking
	{
		[Key]
		public Guid ID { get; set; }

		[Required]
		public Guid UserID { get; set; }

		[ForeignKey("UserID")]
		public Users? User { get; set; }

		[Required]
		public int Win { get; set; }

		[Required]
		public int Lose { get; set; }

		[Required]
		public int Draw { get; set; }

		[Required]
		public int EloRating { get; set; }
	}
}
