using System;

namespace ThreeDo.Models
{
	public class CardItem:BaseEntity
	{
		public CardItem()
		{
		}

		public int Card_Item_Id
		{
			get;
			set;
		}
		public string Title
		{
			get;
			set;
		}
		public int Owner_Id
		{
			get;
			set;
		}
		public int Card_Id
		{
			get;
			set;
		}
		public DateTime Date_Created
		{
			get;
			set;
		}
		public DateTime Date_Modified
		{
			get;
			set;
		}
		public string Modified_By
		{
			get;
			set;
		}



	}
}
