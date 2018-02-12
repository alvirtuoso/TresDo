using System;
using System.ComponentModel.DataAnnotations;

namespace ThreeDo.Models
{
	public class Board: BaseEntity
	{
		
		public Board()
		{
		}
		[Key]
		public Guid Board_Id { get; set; }

		public Guid? Owner_Id
		{
			get;
			set;
		}
		public string Title
		{
			get;
			set;
		}

		public DateTime? Date_Created
		{
			get;
			set;
		}
		public Guid Classification_Id
		{
			get;
			set;
		}

		public Guid? Initial_Card_Id { get; set; }

	}
}
