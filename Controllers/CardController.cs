using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ThreeDo.Models;
using ThreeDo.Repository;

namespace ThreeDo.Controllers
{
	[Route("api/[controller]")]
	public class CardController : Controller
	{
		private readonly CardRepository cardRepo = new CardRepository();


		// GET api/card/5
		[HttpGet("id/{id}")]
		public Card Get(Guid id)
		{
			//#if (DEBUG)
			//{
			//	Console.WriteLine("line 17 cardcontroller");
			//}
			//#endif
			return this.cardRepo.FindByID(id);
		}

		// api/card
		[HttpGet("cards_by_board_id/{boardid}")]
		public IEnumerable<Card> FindCardsByBoardId(Guid boardId)
		{
			var cards = this.cardRepo.FindByBoardId(boardId);
			return cards;
		}

		// GET api/card
		[HttpGet]
		public IEnumerable<Card> Get()
		{
			//var custRepo = new cardRepository();
			var cards = this.cardRepo.FindAll();

			return cards;
		}

		// POST: api/card/create
		[HttpPost("create")]
		public Card Create([FromBody]Card card)
		{
			Card c = null;
			if (ModelState.IsValid)
			{
				c = this.cardRepo.Add(card);
				//return RedirectToAction("Index");
			}

			return c;
		}

		// PUT: api/card/update
		[HttpPut, Route("update")]
		public string Edit([FromBody]Card card)
		{
			string result = "Failed";
			try
			{
				if (ModelState.IsValid)
				{
					this.cardRepo.Update(card);
					result = "Success";
				}
			}
			catch (Exception ex)
			{
				result = ex.Message;
			}
			return result;
		}

		// DELETE: api/card/delete/{card_id}
		[HttpDelete("delete/{card_id}")]
		public string Delete(Guid card_id)
		{
			string result = "Success";
			try
			{
				this.cardRepo.Remove(card_id);
			}
			catch (Exception ex)
			{
				result = ex.Message;
			}
			return result;
			//return new NoContentResult();
		}

	}
}
