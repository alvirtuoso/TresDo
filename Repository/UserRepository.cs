using System;
using ThreeDo.Models;
using System.Collections.Generic;
using System.Data;
using Dapper;
using ThreeDo.DbContext;

namespace ThreeDo.Repository
{
	public class UserRepository: ConnectionFactory, IRepository<User>
	{
		
		//private string connectionString;
		public UserRepository()
		{
			//this.connectionString = Environment.GetEnvironmentVariable("ConnectionString");
		}
	
		/// <summary>
		/// Adds the user.
		/// </summary>
		/// <returns>The user.</returns>
		/// <param name="user">User.</param>
		public User Add(User user)
		{
			using (IDbConnection dbConnection = GetDapperConnection)
			{
				dbConnection.Open();
				return dbConnection.Query<User>("INSERT INTO public.user (first_name,last_name,phone,cell, address, city, zip, email) VALUES(@First_Name, @Last_Name,@Phone, @Cell, @Address, @City, @Zip, @Email)", user).AsList<User>()[0];

			}
		}

		/// <summary>
		/// Add the specified email and displayName.
		/// </summary>
		/// <returns>The add.</returns>
		/// <param name="email">Email.</param>
		/// <param name="displayName">Display name.</param>
		public User Add(string email, string displayName)
		{			
			using (IDbConnection dbConnection = GetDapperConnection)
			{
				var p = new DynamicParameters();
				p.Add("_email", email);
				p.Add("_display_name", displayName);

				return dbConnection.Query<User>("fn_add_user", p, commandType: CommandType.StoredProcedure).AsList<User>()[0];

			}
		}

		/// <summary>
		/// Remove the specified id.
		/// </summary>
		/// <returns>The remove.</returns>
		/// <param name="id">Identifier.</param>
		public void Remove(Guid id)
		{
			using (IDbConnection dbConnection = GetDapperConnection)
			{
				dbConnection.Open();
				dbConnection.Execute("DELETE FROM public.user WHERE user_id=@User_id", new { user_id = id });
			}
		
		}
		/// <summary>
		/// Update the specified cust.
		/// </summary>
		/// <returns>The update.</returns>
		/// <param name="cust">Cust.</param>
		public void Update(User cust)
		{
			using (IDbConnection dbConnection = GetDapperConnection)
			{
				dbConnection.Open();
				dbConnection.Query("UPDATE public.user SET first_name = @First_Name,  last_name = @Last_Name, phone  = @Phone, cell=@Cell, email= @Email, address=@Address, city=@City, zip=@Zip, active=@Active, display_name=@Display_Name WHERE user_id = @User_id", cust);

			}
		}
		/// <summary>
		/// Finds the User by identifier.
		/// </summary>
		/// <returns>The by identifier.</returns>
		/// <param name="id">Identifier.</param>
		public User FindByID(Guid id)
		{
			var custo = new User();
			using (IDbConnection dbConnection = GetDapperConnection)
			{
				dbConnection.Open();
				custo = dbConnection.Query<User>("SELECT * FROM public.user WHERE user_id = @user_id", new { user_id = id }).AsList<User>()[0];
			}
			return custo; 	

		}


		public Boolean IsExisting(string email)
		{
			var result = false;
			using (IDbConnection conn = GetDapperConnection)
			{
				result = conn.ExecuteScalar<bool>("select count(1) from public.user where email=@email", new { email });
			}

			return result;
		}

		public User FindByEmail(string email_param)
		{
			User custo = null;
			try
			{
				using (IDbConnection dbConnection = GetDapperConnection)
				{
					dbConnection.Open();
					custo = dbConnection.Query<User>("SELECT * FROM public.user WHERE email = @email", new { email = email_param }).AsList<User>()[0];
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

			return custo;

		}

		/// <summary>
		/// Finds all Users.
		/// </summary>
		/// <returns>The all.</returns>
		public IEnumerable<User> FindAll()
		{			

			IEnumerable<User> custList;
			// DapperConnection from ConnectionFactory
			using (var dbCon = GetDapperConnection)
			{
				dbCon.Open();
				custList = dbCon.Query<User>("SELECT * FROM public.user");
			}
			return custList;
		}

	
	}
}
