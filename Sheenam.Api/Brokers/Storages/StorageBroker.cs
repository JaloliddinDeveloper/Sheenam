//==================================================
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//==================================================

using EFxceptions;
using Microsoft.EntityFrameworkCore;
namespace Sheenam.Api.Brokers.Storages
{
    public partial class StorageBroker : EFxceptionsContext, IStorageBroker
    {
        private readonly IConfiguration configuration;

        public StorageBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.Database.Migrate();
        }
        private async ValueTask<T> InsertAsync<T>(T @object) where T : class
        {
          using var broker = new StorageBroker(this.configuration);
                broker.Entry<T>(@object).State=EntityState.Added;
            await broker.SaveChangesAsync();
            return @object;
        }
        private IQueryable<T> SelectAll<T>() where T : class
        { 
           using var broker = new StorageBroker(this.configuration);
           
            return broker.Set<T>();
        }
        private async ValueTask<T> SelectAsync<T>(params object[] objectIds) where T: class
        {
          using var broker= new StorageBroker(this.configuration);

            return await broker.FindAsync<T>(objectIds);
        }
        

        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-CNTLCPV;Initial Catalog=TestDB;Integrated Security=True;TrustServerCertificate=True");
        }
        public override void Dispose() { }
    }
}
