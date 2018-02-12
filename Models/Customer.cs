using System;
using System.ComponentModel.DataAnnotations;

namespace ThreeDo.Models
{
	public class Customer:BaseEntity
	{
		//string fname;
		//string lname;
		//int customerId;
		//string email;
		//string phone;
		public Customer()
		{
		}
		public string FName
		{
			get;
			set;
		}

		public string LName
		{
			get;
			set;
		}
		[Key]
		public Guid CustomerId
		{
			get;
			set;
		}
		public string Email
		{
			get;
			set;
		}

		public string Phone
		{
			get;
			set;
		}


	}
}
