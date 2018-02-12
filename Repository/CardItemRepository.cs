using System;
using ThreeDo.Models;
using System.Collections.Generic;
using System.Data;
using Dapper;
using ThreeDo.DbContext;

namespace ThreeDo.Repository
{
	public class CardItemRepository : ConnectionFactory, IRepository<Card>
	{
		public CardItemRepository()
		{
			
		}

		public CardItem Add(CardItem card)
		{
			var c = new CardItem();
			using (IDbConnection dbConnection = GetDapperConnection)
			{

				var p = new DynamicParameters();
				p.Add("_name", card.Title);
				p.Add("_owner_id", card.Owner_Id);
				p.Add("_board_id", card.Card_Id);
				

                c = dbConnection.Query<CardItem>("fn_add_card", p, commandType: CommandType.StoredProcedure).AsList()[0];

			}
			return c;

		}

		public void Remove(int id)
		{
			using (IDbConnection dbConnection = GetDapperConnection)
			{
				dbConnection.Open();
				dbConnection.Execute("DELETE FROM public.card WHERE card_id=@Card_Id", new { card_id = id });
			}

		}
		public void Update(Card card)
		{
			using (IDbConnection dbConnection = GetDapperConnection)
			{
				dbConnection.Open();
				dbConnection.Query("UPDATE public.card SET name = @Name, date_created  = @Date_Created, active=@Active, board_id=@Board_Id, owner_id=@Owner_Id", card);

			}
		}
		public Card FindByID(int id)
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
		public IEnumerable<Card> FindByBoardId(int boardId)
		{

			IEnumerable<Card> cards;
			// DapperConnection from ConnectionFactory
			using (var dbCon = GetDapperConnection)
			{
				dbCon.Open();
				cards = dbCon.Query<Card>("SELECT * FROM public.card WHERE board_id = @board_id", new { board_id = boardId });
			}
			return cards;
		}

        public Card Add(Card item)
        {
            throw new NotImplementedException();
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public Card FindByID(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
