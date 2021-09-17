using Duende.IdentityServer.EntityFramework.Options;
using HomeHarvest.Server.Entities;
using HomeHarvest.Server.Models;

using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace HomeHarvest.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
            Debug.WriteLine($"{ContextId} context created");
        }

        public DbSet<Crop> Crops { get; set; }
        public DbSet<Sown> Sowns { get; set; }
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