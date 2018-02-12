using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThreeDo.Models;
using ThreeDo.Repository;

namespace ThreeDo.Controllers
{
	[Route("api/[controller]")]
	public class MediaDataController
	{
		private readonly MediaDataRepository mediaRepo = new MediaDataRepository();

		/// <summary>
		/// Get this instance. url: api/mediadata
		/// </summary>
		/// <returns>The get.</returns>
		[HttpGet("{itemId}")]
		public IEnumerable<MediaData> FindMediaListByItemId(Guid itemId)
		{
			//var custRepo = new cardRepository();
			var mList = this.mediaRepo.GetMediaDataByItemId(itemId);

			return mList;
		}

		// PUT api/mediadata/updateitemid
		[HttpPut("updateItemId")]
		public ActionResult UpdateItemIdInMediaData([FromForm]Guid item_id, Guid media_id)
		{
			var result = "Success";
			try
			{
				if (item_id != Guid.Empty && media_id != Guid.Empty)
				{
					this.mediaRepo.UpdateItemIdInMediaData(item_id, media_id);
				}
			}
			catch (Exception ex)
			{
				result = "Failure: " + ex.Message;
			}

			return new JsonResult(result);
		}

		// POST api/mediadata/upload
		[HttpPost("upload")]
		public async Task<ActionResult> Post(IFormFile file) //List<IFormFile> files)
		{
			// full path to file in temp location
			byte[] byteArray = null;
			var result = "Success"; 
		try
			{
				if (file != null && file.Length > 0)
				{
					var mediaID = Guid.Empty;
					// process uploaded files
					// Don't rely on or trust the FileName property without validation.
					using (var memoryStream = new MemoryStream())
					{
						await file.CopyToAsync(memoryStream);
						byteArray = memoryStream.ToArray(); // save to db	
						mediaID = this.mediaRepo.SaveItemMedia(Path.GetExtension(file.FileName), file.FileName, byteArray);

						result = mediaID.ToString(); 
				}
					// Option: copy to server temp file
					//using (var stream = new FileStream(filePath, FileMode.Create))
					//{
					//	await formFile.CopyToAsync(stream);
					//}
				}
			}
			catch(Exception ex)
			{
				result = "ServerError: " + ex.Message; 
		}

			return new JsonResult(result);
		}
	}

}
