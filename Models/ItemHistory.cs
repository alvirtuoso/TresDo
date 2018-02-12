using System;
using ThreeDo.Models;
using System.ComponentModel.DataAnnotations;

namespace ThreeDo.Models
{
	public class ItemHistory: BaseEntity
	{
		public ItemHistory()
		{
		}
		[Key]
		public Guid Item_History_Id
		{
			get;
			set;
		}
		public DateTime Date_Modified
		{
			get;
			set;
		}
		public Guid Card_id
		{
			get;
			set;
		}
		public int Status_Id
		{
			get;
			set;
		}
	}
}
