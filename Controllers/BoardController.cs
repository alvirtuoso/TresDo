using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ThreeDo.Models;
using ThreeDo.Repository;

namespace ThreeDo.Controllers
{
	[Route("api/[controller]")]
	public class BoardController: Controller
	{
		private readonly BoardRepository boardRepo = new BoardRepository();

		// api/board/{id}
		[HttpGet("id/{id}")]
		public Board GetBoard(Guid id)
		{
			return this.boardRepo.FindByID(id);

		}

		// GET api/board
		[HttpGet]
		public IEnumerable<Board> Get()
		{
			var boards = this.boardRepo.FindAll();

			return boards;
		}

        // GET api/board/public
        [HttpGet("public")]
        public IEnumerable<Board> GetPublicBoard()
        {
            var boards = this.boardRepo.FindPublicBoards();

            return boards;
        }

        // GET api/board/private
        [HttpGet("private/{email}")]
        public IEnumerable<Board> GetPrivateBoard(string email)
        {
            var boards = this.boardRepo.FindPrivateBoards(email);

            return boards;
        }

		// POST: api/board/create
        [HttpPost("create")]
        public Board Create([FromBody]Board board)
		{
			var b = new Board();
			if (ModelState.IsValid)
			{
				 b = this.boardRepo.Add(board);

			}
			return b;

		}

		// PUT: api/board/edit
		[HttpPut("edit")]
		public void Edit([FromBody]Board board)
		{
			if (ModelState.IsValid)
			{
				this.boardRepo.Update(board);

			}

		}

		// DELETE: api/board/delete/{idhere}
		[HttpDelete("delete/{id}")]
		public void Delete(Guid id)
		{
			this.boardRepo.Remove(id);

		}

	}
}
