using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ThreeDo.Models;
using ThreeDo.Repository;

namespace ThreeDo.Controllers
{
	[Route("api/[controller]")]
	public class UserController : Controller
	{
		private readonly UserRepository userRepo = new UserRepository();

	
		// GET api/user/id/5
		[HttpGet("id/{id}")]
		public User Get(Guid id)
		{
			//#if (DEBUG)
			//{
			//	Console.WriteLine("line 17 cusomtercontroller");
			//}
			//#endif
			return this.userRepo.FindByID(id);
		}
		// GET api/user/email/{email}
		[HttpGet("email/{email}")]
		public User GetByEmail(string email)
		{
			User u = null;
			//#if (DEBUG)
			//{
			//	Console.WriteLine("line 17 cusomtercontroller");
			//}
			//#endif
			try
			{
				u = this.userRepo.FindByEmail(email);
			}
			catch (Exception ex)
			{
				// Log error here.
				var err = ex.Message;
			}
			return u;
		}

		[HttpGet("isExisting/{email}")]
		public Boolean IsExisting(string email)
		{
			return this.userRepo.IsExisting(email);
		}

		// GET api/user
		[HttpGet]
		public IEnumerable<User> Get()
		{
			//var custRepo = new userRepository();
			var custList = this.userRepo.FindAll();

			return custList;
		}

		// POST: api/user/add
		[HttpPost("add")]
		public User Add(string email, string display_name)
		{
			User u = null;

			if (ModelState.IsValid)
			{
				u = this.userRepo.Add(email, display_name);
			}
			return u;

		}

		// PUT: api/user/edit
		[HttpPut("edit")]
		public void Edit([FromBody]User user)
		{
			if (ModelState.IsValid)
			{
				this.userRepo.Update(user);

			}

		}

		// DELETE: api/user/delete?user_id=2
		[HttpDelete("delete/{user_id}")]
		public void Delete(Guid id)
		{
			this.userRepo.Remove(id);
			//return new NoContentResult();
		}
		
	}
}
