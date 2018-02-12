using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ThreeDo.Models;
using ThreeDo.Repository;

namespace ThreeDo.Controllers
{
	[Route("api/[controller]")]
	public class ItemHistoryController : Controller
	{
		private readonly ItemHistoryRepository itemHistoryRepo = new ItemHistoryRepository();


		// GET api/itemHistory/5
		[HttpGet("{id}")]
		public ItemHistory Get(Guid id)
		{
			//#if (DEBUG)
			//{
			//	Console.WriteLine("line 17 cusomtercontroller");
			//}
			//#endif
			return this.itemHistoryRepo.FindByID(id);
		}

		// GET api/itemHistory
		[HttpGet]
		public IEnumerable<ItemHistory> Get()
		{
			//var custRepo = new itemHistoryRepository();
			var items = this.itemHistoryRepo.FindAll();

			return items;
		}

		// POST: api/itemHistory/create
		[HttpPost("create")]
		public void Create([FromBody]ItemHistory cust)
		{
			if (ModelState.IsValid)
			{
				this.itemHistoryRepo.Add(cust);
				//return RedirectToAction("Index");
			}


		}

		// PUT: api/itemHistory/edit
		[HttpPut("edit")]
		public void Edit([FromBody]ItemHistory itemHistory)
		{
			if (ModelState.IsValid)
			{
				this.itemHistoryRepo.Update(itemHistory);

			}

		}

		// DELETE: api/itemHistory/delete?itemHistory_id=2
		[HttpDelete("{itemHistory_id}")]
		public void Delete(Guid id)
		{
			this.itemHistoryRepo.Remove(id);
			//return new NoContentResult();
		}

	}
}
