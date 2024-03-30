//==================================================
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//==================================================

using Microsoft.EntityFrameworkCore;
using Sheenam.Api.Models.Foundations.Guests;
using Sheenam.Api.Models.Foundations.Guests.Exceptions;

namespace Sheenam.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Guest> Guests { get; set; }

        public async ValueTask<Guest> InsertGuestAsync(Guest guest)
        {
           return await InsertAsync(guest);
        }
        public IQueryable<Guest> SelectAllGuests()
        {
            return SelectAll<Guest>();
        }
        public async ValueTask<Guest> SelectGuestByIdAsync(Guid guestId)
        {
            return await SelectAsync<Guest>(guestId);
        }
        public async ValueTask<Guest> UpdateGuestAsync(Guest guest)
        {
            return await UpdateAsync(guest);
        }
       public async ValueTask<Guest> DeleteGuestAsync(Guest guest)
        {
            return await DeleteAsync<Guest>(guest);
        }
    }
}
