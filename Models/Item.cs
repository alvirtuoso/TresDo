using System;
using System.ComponentModel.DataAnnotations;

namespace ThreeDo.Models
{
	public class Item: BaseEntity
	{
		public Item()
		{
		}
		[Key]
		public Guid? Item_Id
		{
			get;
			set;
		}

		public DateTime Date_Created
		{
			get;
			set;
		}

		public DateTime Due_Date
		{
			get;
			set;
		}

		public string Title
		{
			get;
			set;
		}
		public string Description
		{
			get;
			set;
		}
		public int Status_Id
		{
			get;
			set;
		}
		public DateTime Date_Modified
		{
			get;
			set;
		}
		public Guid Card_Id
		{
			get;
			set;
		}
		public Guid Owner_Id
		{
			get;
			set;
		}
		public Guid Modified_By_Id
		{
			get;
			set;
		}

		public int Sort_Order
		{
			get;
			set;
		}
	}
}
