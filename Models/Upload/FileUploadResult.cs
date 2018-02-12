using System;
namespace ThreeDo.Models.Upload
{
	public class FileUploadResult
	{
		public FileUploadResult()
		{

		}

		public string LocalFilePath { get; set; }
		public string FileName { get; set; }
		public long FileLength { get; set; }
	}
}
