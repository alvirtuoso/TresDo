using System;
using ThreeDo.Models;
using System.Collections.Generic;
using System.Data;
using Dapper;
using ThreeDo.DbContext;

namespace ThreeDo.Repository
{
	public class BoardRepository : ConnectionFactory, IRepository<Board>
	{

		//private string connectionString;
		public BoardRepository()
		{
			//this.connectionString = Environment.GetEnvironmentVariable("ConnectionString");
		}

		public Guid AddBoard(Board board)
		{
			using (IDbConnection dbConnection = GetDapperConnection)
			{
				dbConnection.Open();
				var newID = dbConnection
					.Query<Guid>("INSERT GuidO public.board (title, owner_id, classification_id) VALUES(@Title, @Owner_id, @Classification_Id) RETURNING board_id", board).AsList<Guid>()[0];
				return newID;
			}

		
		}
		public Board Add(Board board)
		{
			Board b = null;
			using (IDbConnection dbConnection = GetDapperConnection)
			{
				
				var p = new DynamicParameters();
				p.Add("_title", board.Title);
				p.Add("_owner_id", board.Owner_Id);
				p.Add("_classification_id", board.Classification_Id);
				//p.Add("oboard_id", dbType: DbType.Int32, direction: ParameterDirection.Output);
				//p.Add("ocard_id", dbType: DbType.Int32, direction: ParameterDirection.Output);
				b = dbConnection.Query<Board>("fn_add_board", p, commandType: CommandType.StoredProcedure).AsList<Board>()[0];
				//newBoardID = p.Get<int>("oboard_id");
				//newCardID = p.Get<int>("ocard_id");	


			}
			return b;

		}

		public void Remove(Guid id)
		{
			using (IDbConnection dbConnection = GetDapperConnection)
			{
				dbConnection.Open();
				dbConnection.Execute("DELETE FROM public.board WHERE board_id=@Board_id", new { board_id = id });
			}

		}
		public void Update(Board board)
		{
			using (IDbConnection dbConnection = GetDapperConnection)
			{
				dbConnection.Open();
				dbConnection.Query("UPDATE public.board SET title = @Title,  owner_id = @Owner_id, date_created  = @Date_Created, classification_id=@Classification_Id", board);

			}
		}
		public Board FindByID(Guid id)
		{
			var board = new Board();
			using (IDbConnection dbConnection = GetDapperConnection)
			{
				dbConnection.Open();
				board = dbConnection.Query<Board>("SELECT * FROM public.board WHERE board_id = @board_id", new { board_id = id }).AsList<Board>()[0];
			}
			return board;

		}
		public IEnumerable<Board> FindAll()
		{
            // 
			IEnumerable<Board> boards;
			// DapperConnection from ConnectionFactory
			using (var dbCon = GetDapperConnection)
			{
                try
                {
                    dbCon.Open();
                    boards = dbCon.Query<Board>("SELECT * FROM public.board");
                }
                catch (Exception ex)
                {
                    var s = ex.Message;
                    boards = null;
                }

			}
			return boards;
		}

        public IEnumerable<Board> FindPublicBoards()
        {
            // faad67d2-7ec6-494e-a4ef-ac126481f77f is public classification_id
            IEnumerable<Board> boards;
            // DapperConnection from ConnectionFactory
            using (var dbCon = GetDapperConnection)
            {
                try
                {
                    dbCon.Open();
                    boards = dbCon.Query<Board>("SELECT * FROM public.board WHERE classification_id = 'faad67d2-7ec6-494e-a4ef-ac126481f77f'");
                }
                catch (Exception ex)
                {
                    var s = ex.Message;
                    boards = null;
                }

            }
            return boards;
        }

        public IEnumerable<Board>FindPrivateBoards(String email)
        {
            IEnumerable<Board> boards;
            // DapperConnection from ConnectionFactory
            using (var dbCon = GetDapperConnection)
            {
                try
                {
                    using (IDbConnection dbConnection = GetDapperConnection)
                    {
                        var p = new DynamicParameters();
                        p.Add("_email", email);

                        boards = dbConnection.Query<Board>("fn_get_boards_by_email", p, commandType: CommandType.StoredProcedure).AsList();
                    }
                }
                catch (Exception ex)
                {
                    var s = ex.Message;
                    boards = null;
                }
            }
            return boards;
        }
	}
}
