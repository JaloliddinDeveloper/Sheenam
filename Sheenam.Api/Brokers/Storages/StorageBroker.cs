//==================================================
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//==================================================

using EFxceptions;
using Microsoft.EntityFrameworkCore;

namespace Sheenam.Api.Brokers.Storages
{
    public partial class StorageBroker:EFxceptionsContext,IStorageBroker
    {
        private readonly IConfiguration configuration;

        public StorageBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.Database.Migrate();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-D1BB1FN;Initial Catalog=SheenamDB;Integrated Security=True;TrustServerCertificate=True");
        }
        public override void Dispose() { }
    }
}
