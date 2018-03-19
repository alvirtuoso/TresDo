using System;
using ThreeDo.Models;
using System.Collections.Generic;
using System.Data;
using Dapper;
using ThreeDo.DbContext;

namespace ThreeDo.Repository
{
	public class CardRepository : ConnectionFactory, IRepository<Card>
	{

		//private string connectionString;
		public CardRepository()
		{
			//this.connectionString = Environment.GetEnvironmentVariable("ConnectionString");
		}


		public Guid AddNew(Card card)
		{
			using (IDbConnection dbConnection = GetDapperConnection)
			{
				dbConnection.Open();
				var i = dbConnection
					.Query<Guid>("INSERT INTO public.card (name, active, board_id, owner_id) VALUES(@Name, @Active, @Board_id, @Owner_id)", card).AsList<Guid>()[0];
				return i;
			}
		}

		public Card Add(Card card)
		{
			var c = new Card();
            try
            {
				using (IDbConnection dbConnection = GetDapperConnection)
				{
					
					var p = new DynamicParameters();
					p.Add("_name", card.Name);
					p.Add("_owner_id", card.Owner_Id);
					p.Add("_board_id", card.Board_Id);
					p.Add("_active", card.Active);
					
                    c = dbConnection.Query<Card>("fn_add_card", p, commandType: CommandType.StoredProcedure).AsList<Card>()[0];
					
				}
            }
            catch (Exception ex)
            {
                throw ex;
            }
			return c;

		}

		public void Remove(Guid id)
		{
			using (IDbConnection dbConnection = GetDapperConnection)
			{
				var p = new DynamicParameters();
				p.Add("_card_id", id);
				dbConnection.Open();
				dbConnection.Execute("fn_delete_card", p, commandType: CommandType.StoredProcedure);
			}

		}
		public void Update(Card card)
		{
			using (IDbConnection dbConnection = GetDapperConnection)
			{
				dbConnection.Open();
				dbConnection.Query("UPDATE public.card SET name = @Name, date_created  = @Date_Created, active=@Active, board_id=@Board_id, owner_id=@Owner_id WHERE card_id = @Card_id", card);
				//Query<Card>("fn_update_card", p, commandType: CommandType.StoredProcedure);
			}
		}


		public Card FindByID(Guid id)
		{
			var card = new Card();
			using (IDbConnection dbConnection = GetDapperConnection)
			{
				dbConnection.Open();
				card = dbConnection.Query<Card>("SELECT * FROM public.card WHERE card_id = @card_id", new { card_id = id }).AsList<Card>()[0];
			}
			return card;

		}
		public IEnumerable<Card> FindAll()
		{

			IEnumerable<Card> cards;
			// DapperConnection from ConnectionFactory
			using (var dbCon = GetDapperConnection)
			{
				dbCon.Open();
				cards = dbCon.Query<Card>("SELECT * FROM public.card");
			}
			return cards;
		}
		public IEnumerable<Card> FindByBoardId(Guid boardId)
		{

			IEnumerable<Card> cards;
			// DapperConnection from ConnectionFactory
			using (var dbCon = GetDapperConnection)
			{
				dbCon.Open();
				cards = dbCon.Query<Card>("SELECT * FROM public.card WHERE board_id = @board_id Order by sort_order ASC", new { board_id = boardId });
			}
			return cards;
		}
	}
}
