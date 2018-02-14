using System;
using Microsoft.EntityFrameworkCore;
//using Npgsql.EntityFrameworkCore.PostgreSQL;
namespace ThreeDo.DbContext
{
	public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
			
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("ConnectionString"));
		}
	}
}
