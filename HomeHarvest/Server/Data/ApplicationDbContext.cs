using HomeHarvest.Server.Entities;
using HomeHarvest.Server.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace HomeHarvest.Server.Data
{
	public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
	{

		public static readonly string RowVersion = nameof(RowVersion);

		public static readonly string DataDb = nameof(DataDb).ToLower();
		public ApplicationDbContext(
			DbContextOptions options,
			IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
		{
			Debug.WriteLine($"{ContextId} context created");
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Crop>()
			   .Property<byte[]>(RowVersion)
			   .IsRowVersion();
			modelBuilder.Entity<Sow>()
			   .Property<byte[]>(RowVersion)
			   .IsRowVersion();
			modelBuilder.Entity<Plant>()
			   .Property<byte[]>(RowVersion)
			   .IsRowVersion();
			base.OnModelCreating(modelBuilder);
		}
		public DbSet<Crop> Crops { get; set; }
		public DbSet<Sow> Sows { get; set; }
		public DbSet<Plant> Plants { get; set; }

		public override void Dispose()
		{
			Debug.WriteLine($"{ContextId} context disposed.");
			base.Dispose();
		}

		public override ValueTask DisposeAsync()
		{
			Debug.WriteLine($"{ContextId} context disposed async.");
			return base.DisposeAsync();
		}
	}
}

