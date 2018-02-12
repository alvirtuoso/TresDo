using System;
using System.Collections.Generic;
using ThreeDo.Repository;
using System.ComponentModel.DataAnnotations;

namespace ThreeDo.Models
{
	public class Card: BaseEntity
	{
		IEnumerable<Item> itemList;
		Guid card_id;
		//int board_id;
		//int owner_id;
		//Boolean active;
		//string name;

		public Card()
		{
		}
		[Key]
		public Guid? Card_Id
		{			
			get { return this.card_id; }
			set 
			{
				Guid.TryParse(value.ToString(), out this.card_id);
			}
		}
		public string Name
		{
			get;
			set;
			//get { return this.name; }
			//set { this.name = value; }
		}
		public DateTime? Date_Created
		{
			get;
			set;
		}
		public bool Active
		{
			get;
			set;
			//get { return this.active; }
			//set { this.active = value; }
		}
		public Guid Board_Id
		{
			get;
			set;
				//get { return this.board_id; }
				//set { this.board_id = value; }
		}
		public Guid Owner_Id
		{
			get;
			set;
			//get { return this.owner_id; }
			//set { this.owner_id = value; }
		}
		public IEnumerable<Item> Items
		{
			get
			{
				if (null == this.itemList)
				{
					var itemRepo = new ItemRepository();
					this.itemList = itemRepo.GetItemsByCardId(this.card_id);
				}
				// Avoid returning null value.
				return (this.itemList != null) ? this.itemList: new List<Item>();
			}
			set { this.itemList = value;}

		}

		public int Sort_Order
		{
			get;
			set;
		}

	}
}
