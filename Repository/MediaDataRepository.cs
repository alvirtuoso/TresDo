using System;
using ThreeDo.Models;
using System.Collections.Generic;
using System.Data;
using Dapper;
using ThreeDo.DbContext;

namespace ThreeDo.Repository
{
	public class MediaDataRepository: ConnectionFactory, IRepository<MediaData>
	{
		public MediaDataRepository()
		{
		}

		public MediaData Add(MediaData item)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<MediaData> FindAll()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Finds all. 
		/// </summary>
		/// <returns>The all.</returns>
		/// <param name="itemId">Item identifier.</param>
		public IEnumerable<MediaData> GetMediaDataByItemId(Guid itemId)
		{
			IEnumerable<MediaData> dataList;
			// DapperConnection from ConnectionFactory
			using (var dbCon = GetDapperConnection)
			{
				var p = new DynamicParameters();
				p.Add("_item_id", itemId);
				dataList = dbCon.Query<MediaData>("fn_get_media_by_item_id", p, commandType: CommandType.StoredProcedure).AsList<MediaData>();
			}

			return dataList;
		}

		public MediaData FindByID(Guid id)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Saves the item media.
		/// </summary>
		/// <param name="fileExt">File ext.</param>
		/// <param name="data">Data.</param>
		public Guid SaveItemMedia(string fileExt, string fileName, byte[] data)
		{
			var mediaId = Guid.Empty;
			using (IDbConnection dbConnection = GetDapperConnection)
			{
				var p = new DynamicParameters();

				p.Add("_file_ext", fileExt);
				p.Add("_filename", fileName);
				p.Add("_data", data);
				mediaId = dbConnection.Query<Guid>("fn_add_media_item", p, commandType: CommandType.StoredProcedure).AsList<Guid>()[0];
			}
			return mediaId;
		}

		/// <summary>
		/// Updates the item identifier in media data.
		/// </summary>
		/// <param name="itemId">Item identifier.</param>
		/// <param name="mediaId">Media identifier.</param>
		internal void UpdateItemIdInMediaData(Guid itemId, Guid mediaId)
		{
			using (IDbConnection dbConnection = GetDapperConnection)
			{
				var p = new DynamicParameters();
				p.Add("_item_id", itemId);
				p.Add("_media_id", mediaId);
				dbConnection.Query("fn_add_item_media_data", p, commandType: CommandType.StoredProcedure);
			}
		}

		/// <summary>
		/// Gets the data by media_data_id.
		/// </summary>
		/// <returns>The data in byte[].</returns>
		/// <param name="mediaId">Media identifier.</param>
		public byte[] GetData(Guid mediaId)
		{
			using (IDbConnection conn = GetDapperConnection)
			{
				var p = new DynamicParameters();
				p.Add("_mediaid", mediaId);
				var d = conn.Query<byte[]>("fn_get_media_byte_by_id", p, commandType: CommandType.StoredProcedure).AsList()[0];
				return d;
			}
		}

		public void Remove(Guid id)
		{
			throw new NotImplementedException();
		}

		public void Update(MediaData mFile)
		{
			throw new NotImplementedException();
		}


	}
}
