using System;
using ThreeDo.Models;
using System.Collections.Generic;
using System.Data;
using Dapper;
using ThreeDo.DbContext;

namespace ThreeDo.Repository
{
	public class ItemHistoryRepository : ConnectionFactory, IRepository<ItemHistory>
	{

		//private string connectionString;
		public ItemHistoryRepository()
		{
			//this.connectionString = Environment.GetEnvironmentVariable("ConnectionString");
		}

		public Guid AddItemHistory(ItemHistory itemHx)
		{
			using (IDbConnection dbConnection = GetDapperConnection)
			{
				dbConnection.Open();
				var i = dbConnection
					.Query<Guid>("INSERT INTO public.item_history (item_id, date_modified, card_id, status_id) VALUES(@Item_id, @Date_Modified, @Card_id, @Status_Id)", itemHx).AsList<Guid>()[0];
				return i;
			}
		}

		public ItemHistory Add(ItemHistory item)
		{
			var i = new ItemHistory();
			using (IDbConnection dbConnection = GetDapperConnection)
			{

				var p = new DynamicParameters();
				p.Add("_card_id", item.Card_id);
				p.Add("_status_id", item.Status_Id);

				i = dbConnection.Query<ItemHistory>("fn_add_item_history", p, commandType: CommandType.StoredProcedure).AsList<ItemHistory>()[0];

			}
			return i;

		}

		public void Remove(Guid id)
		{
			using (IDbConnection dbConnection = GetDapperConnection)
			{
				dbConnection.Open();
				dbConnection.Execute("DELETE FROM public.item_history WHERE item_id=@Item_id", new { item_id = id });
			}

		}
		public void Update(ItemHistory itemHx)
		{
			using (IDbConnection dbConnection = GetDapperConnection)
			{
				dbConnection.Open();
				dbConnection.Query("UPDATE public.item_history SET item_id = @item_id, date_modified  = @Date_Modified, card_id=@Card_id, status_id=@Status_Id", itemHx);

			}
		}
		public ItemHistory FindByID(Guid id)
		{
			var item = new ItemHistory();
			using (IDbConnection dbConnection = GetDapperConnection)
			{
				dbConnection.Open();
				item = dbConnection.Query<ItemHistory>("SELECT * FROM public.item_history WHERE item_history_id = @Item_History_id", new { item_hisotry_id = id }).AsList<ItemHistory>()[0];
			}
			return item;

		}
		public IEnumerable<ItemHistory> FindAll()
		{

			IEnumerable<ItemHistory> items;
			// DapperConnection from ConnectionFactory
			using (var dbCon = GetDapperConnection)
			{
				dbCon.Open();
				items = dbCon.Query<ItemHistory>("SELECT * FROM public.item_history");
			}
			return items;
		}
	}
}
