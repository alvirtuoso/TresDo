using System;
using ThreeDo.Repository;

namespace ThreeDo.Models
{
	/// <summary>
	/// Media file. 
	/// </summary>
	public class MediaData: BaseEntity
	{
		public MediaData()
		{
		}

		//private byte[] dataByte;
		private Guid media_data_id;

        public Guid Item_Media_Data_Id{
            get;
            set;
        }
		public Guid Media_Data_Id
		{
			get{
				return this.media_data_id;
			}
			set { this.media_data_id = value; }
		}
		public string Filename
		{
			get;
			set;
		}

        public string File_Extension
		{
			get;
			set;
		}

		public Guid Item_Id
		{
			get;
			set;
		}

		public byte[] DataByte
		{
            get;
            set;
			//get{
			//	// Load data only when needed
			//	if (this.dataByte == null)
			//	{
			//		this.dataByte = new MediaDataRepository().GetData(this.media_data_id);
			//	}
	
			//	return this.dataByte;
			//}
			//set
			//{
			//	this.dataByte = value;
			//}
		}

	}
}
