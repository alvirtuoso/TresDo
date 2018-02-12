﻿using System;
using System.Linq;
using DomainModel.Model;
using Microsoft.EntityFrameworkCore;

namespace DataAccessPostgreSqlProvider
{
	// >dotnet ef migration add testMigration in AspNet5MultipleProject
	public class DomainModelPostgreSqlContext : DbContext
	{
		public DomainModelPostgreSqlContext(DbContextOptions<DomainModelPostgreSqlContext> options) : base(options)
		{
		}

		public DbSet<DataEventRecord> DataEventRecords { get; set; }

		public DbSet<SourceInfo> SourceInfos { get; set; }
         
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<DataEventRecord>().HasKey(m => m.DataEventRecordId);
			modelBuilder.Entity<SourceInfo>().HasKey(m => m.SourceInfoId);

			// shadow properties
			modelBuilder.Entity<DataEventRecord>().Property<DateTime>("UpdatedTimestamp");
			modelBuilder.Entity<SourceInfo>().Property<DateTime>("UpdatedTimestamp");

			base.OnModelCreating(modelBuilder);
		}

		public override int SaveChanges()
		{
			ChangeTracker.DetectChanges();

			updateUpdatedProperty<SourceInfo>();
			updateUpdatedProperty<DataEventRecord>();

			return base.SaveChanges();
		}

		private void updateUpdatedProperty<T>() where T : class
		{
			var modifiedSourceInfo =
				ChangeTracker.Entries<T>()
					.Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

			foreach (var entry in modifiedSourceInfo)
			{
				entry.Property("UpdatedTimestamp").CurrentValue = DateTime.UtcNow;
			}
		}
	}
}