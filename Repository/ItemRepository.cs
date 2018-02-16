using System;
using ThreeDo.Models;
using System.Collections.Generic;
using System.Data;
using Dapper;
using ThreeDo.DbContext;

namespace ThreeDo.Repository
{
	public class ItemRepository : ConnectionFactory, IRepository<Item>
	{

		//private string connectionString;
		public ItemRepository()
		{
			//this.connectionString = Environment.GetEnvironmentVariable("ConnectionString");
		}

		/// <summary>
		/// Adds the item.
		/// </summary>
		/// <returns>The item.</returns>
		/// <param name="item">Item.</param>
		public Guid AddItem(Item item)
		{ 
			using (IDbConnection conn = GetDapperConnection)
			{
				conn.Open();
				var i = conn
					.Query<Guid>("INSERT INTO public.item (name, due_date, date_created, active, board_id, owner_id) VALUES(@Name, @Due_Date, @Date_Created, @Active, @Board_id, @Owner_id)", item).AsList<Guid>()[0];
				return i;
			}
		}

		public IEnumerable<MediaData> GetAllMediaByItemId(Guid itemId)
		{

			IEnumerable<MediaData> mFiles;
			// DapperConnection from ConnectionFactory
			using (var dbCon = GetDapperConnection)
			{
				var p = new DynamicParameters();
				p.Add("_item_id", itemId);
				dbCon.Open();
				mFiles = dbCon.Query<MediaData>("fn_get_media_by_item_id", p, commandType: CommandType.StoredProcedure).AsList<MediaData>();
			}
			return mFiles;
		}

		/// <summary>
		/// Creates an Item object
		/// </summary>
		/// <returns>Item</returns>
		/// <param name="item">Item.</param>
		public Item Add(Item item)
		{
			var i = new Item();
			using (IDbConnection dbConnection = GetDapperConnection)
			{

				var p = new DynamicParameters();
				p.Add("_title", item.Title);
				p.Add("_description", item.Description);
				p.Add("_card_id", item.Card_Id);
				p.Add("_owner_id", item.Owner_Id);
				p.Add("_modified_by_id", item.Modified_By_Id);
				p.Add("_status_id", item.Status_Id);
				p.Add("_due_date", item.Due_Date);
				i = dbConnection.Query<Item>("fn_add_item", p, commandType: CommandType.StoredProcedure).AsList<Item>()[0];

			}
			return i;

		}

		/// <summary>
		/// Adds the item to card. Each card has many items.
		/// </summary>
		/// <returns>IEnumerable of Item type.</returns>
		/// <param name="item">Item.</param>
		public IEnumerable<Item> AddItemToCard(Item item)
		{
			IEnumerable<Item> items = null;
            try
            {
                using (IDbConnection dbConnection = GetDapperConnection)
                {

                    var p = new DynamicParameters();
                    p.Add("_title", item.Title);
                    p.Add("_due_date", item.Due_Date);
                    p.Add("_description", item.Description);
                    p.Add("_card_id", item.Card_Id);
                    p.Add("_owner_id", item.Owner_Id);
                    p.Add("_modified_by_id", item.Modified_By_Id);
                    p.Add("_status_id", item.Status_Id);
                    items = dbConnection.Query<Item>("fn_add_item_to_card", p, commandType: CommandType.StoredProcedure);
                    //var l = dbConnection.Query<object>("fn_add_item_to_card", p, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                var m = ex.Message;
                items = null;
            }

			return items;

		}
		/// <summary>
		/// Remove an Item by specified id.
		/// </summary>
		/// <returns>Void.</returns>
		/// <param name="id">Identifier.</param>
		public void Remove(Guid id)
		{
			using (IDbConnection dbConnection = GetDapperConnection)
			{
				dbConnection.Execute("DELETE FROM public.item WHERE item_id=@Item_id", new { item_id = id });
			}

		}

		/// <summary>
		/// Archives the item.
		/// </summary>
		/// <param name="id">Identifier.</param>
		public void ArchiveItem(Guid id)
		{
			using (IDbConnection dbConnection = GetDapperConnection)
			{
				var p = new DynamicParameters();
				p.Add("_item_id", id);
				dbConnection.Query("fn_archive_item", p, commandType: CommandType.StoredProcedure);
			}
		}

		/// <summary>
		/// Update the specified item.
		/// </summary>
		/// <returns>The update.</returns>
		/// <param name="item">Item.</param>
		public void Update(Item item)
		{

				using (IDbConnection dbConnection = GetDapperConnection)
				{
					dbConnection.Open();
					dbConnection.Query("UPDATE public.item " +
									   "SET date_created  = @Date_Created" +
									   ", title = @Title, description = @Description" +
				                       ", due_date = @Due_Date" +
									   ", status_id = @Status_Id" +
									   ", card_id = @Card_Id" +
									   ", owner_id = @Owner_Id" +
									   ", modified_by_id = @Modified_By_Id" +
									   ", sort_order=@Sort_Order" +
									   " WHERE item_id = @Item_Id", item);

				}
		
		}
	
		/// <summary>
		/// Updates the item cardID that belongs to by identifier.
		/// </summary>
		public string UpdateItemCardId(Guid itemId, Guid cardId)
		{
			string result = "Success";
			try
			{
				using (IDbConnection dbConnection = GetDapperConnection)
				{
					dbConnection.Open();
					dbConnection.Query("UPDATE public.item SET card_id = @card_id WHERE item_id = @item_id", new { card_id = cardId, item_id = itemId });
				}
			}
			catch (Exception ex)
			{
				result = "Failure: " + ex.Message;
			}
			return result;
		}

		/// <summary>
		/// Finds the by identifier.
		/// </summary>
		/// <returns>The by identifier.</returns>
		/// <param name="id">Identifier.</param>
		public Item FindByID(Guid id)
		{
			var item = new Item();
			using (IDbConnection dbConnection = GetDapperConnection)
			{
				dbConnection.Open();
				item = dbConnection.Query<Item>("SELECT * FROM public.item WHERE item_id = @item_id", new { item_id = id }).AsList<Item>()[0];
			}
			return item;

		}
		/// <summary>
		/// Gets the items by card identifier. Each card has 1 or more items.
		/// </summary>
		/// <returns>The items by card identifier.</returns>
		/// <param name="cardId">Card identifier.</param>
		public IEnumerable<Item> GetItemsByCardId(Guid cardId)
		{
			IEnumerable<Item> items;
			using (var dbCon = GetDapperConnection)
			{
				dbCon.Open();
				items = dbCon.Query<Item>("SELECT i.item_id, i.title, i.due_date, i.date_created, i.description, i.status_id, i.date_modified, i.card_id, i.owner_id, i.modified_by_id, i.item_id, i.sort_order " +
				                          "FROM public.item i Join public.card c " +
				                          "ON i.card_id = c.card_id WHERE c.card_id = @card_id Order by i.sort_order ASC", new { card_id = cardId });
			}

			return items;
		}
		/// <summary>
		/// Finds all Items
		/// </summary>
		/// <returns>The all.</returns>
		public IEnumerable<Item> FindAll()
		{

			IEnumerable<Item> items;
			// DapperConnection from ConnectionFactory
			using (var dbCon = GetDapperConnection)
			{
				dbCon.Open();
				items = dbCon.Query<Item>("SELECT * FROM public.item");
			}
			return items;
		}
	}
}
