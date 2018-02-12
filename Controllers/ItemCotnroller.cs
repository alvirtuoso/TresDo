using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThreeDo.Models;
using ThreeDo.Repository;
namespace ThreeDo.Controllers
{
	[Route("api/[controller]")]
	public class ItemController : Controller
	{
		private readonly ItemRepository itemRepo = new ItemRepository();

		// GET api/item/5
		[HttpGet("{id}")]
		public Item Get(Guid id)
		{
			//#if (DEBUG)
			//{
			//	Console.WriteLine("line 17 controller");
			//}
			//#endif
			return this.itemRepo.FindByID(id);
		}


		// GET api/item/getitemsbycardid
		[HttpGet("getitemsbycardid")]
		public IEnumerable<Item> GetItemsByCardId(Guid cardId)
		{
			var items = this.itemRepo.GetItemsByCardId(cardId);

			return items;
		}

		// POST: api/item/create
		[HttpPost("create")]
		public Item Create([FromBody]Item item)
		{
			var newItem = new Item();
			if (ModelState.IsValid)
			{
				newItem = this.itemRepo.Add(item);
				//return RedirectToAction("Index");
			}

			return newItem;
		}

		// POST: api/item/createforcard items
		[HttpPost("createforcard")]
		public IEnumerable<Item> CreateForCard([FromBody]Item item)
		{
			IEnumerable<Item> items = new List<Item>();
			if (ModelState.IsValid)
			{
				items = this.itemRepo.AddItemToCard(item);
				//return RedirectToAction("Index");
			}

			return items;
		}

		// Put: api/item/editItemCard?item_id=123dec32&card_id=45212detc
		[HttpPut("editItemCard")]
		public ActionResult EditItemCardId([FromForm]Guid item_id, Guid card_id)
		{
			string result = "Success";
			try
			{
				if (ModelState.IsValid)
				{
					this.itemRepo.UpdateItemCardId(item_id, card_id);
				}
			}
			catch (Exception ex)
			{
				result = "Error: " + ex.Message;
			}
			return new JsonResult(result);
		}

		// PUT: api/item/edit
		[HttpPut("edit")]
		public ActionResult Edit([FromBody]Item item)
		{
			string result = "Success";
			try
			{
				if (item != null)
				{
					item.Date_Modified = DateTime.Now;
				}
				if (ModelState.IsValid)
				{
					this.itemRepo.Update(item);

				}
			}
			catch (Exception ex)
			{
				result = "Error: " + ex.Message;
			}
			return new JsonResult(result);
		}

		// DELETE: api/item/delete/{someID}
		[HttpDelete("delete/{item_id}")]
		public void Delete(Guid item_id)
		{
			this.itemRepo.Remove(item_id);
			//return new NoContentResult();
		}

		// Archive: api/item/archive/{someID}
		[HttpDelete("archive/{item_id}")]
		public ActionResult Archive(Guid item_id)
		{
			string result = "Success";
			try
			{
				this.itemRepo.ArchiveItem(item_id);
			}
			catch (Exception ex)
			{
				result = "Server API Error ItemController.Archive(): " + ex.Message;
			}
			return new JsonResult(result);
		}

	}
}
