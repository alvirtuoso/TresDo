using System;
using System.ComponentModel.DataAnnotations;
namespace ThreeDo.Models
{
	public class User: BaseEntity
	{
		public User()
		{
		}
		public string First_Name
		{
			get;
			set;
		}

		public string Last_Name
		{
			get;
			set;
		}
		[Key]
		public Guid User_Id
		{
			get;
			set;
		}
		public Guid membership_Id
		{
			get;
			set;
		}
		public string Email
		{
			get;
			set;
		}

		public string Address
		{
			get; set;
		}
		public string City
		{
			get;
			set;
		}
		public string Zip
		{
			get;
			set;
		}

		public Boolean Active
		{
			get;
			set;
		}

		public string Phone
		{
			get;
			set;
		}

		public string Cell
		{
			get; set;
		}

		public string Display_Name
		{
			get;
			set;
		}
	}
}
