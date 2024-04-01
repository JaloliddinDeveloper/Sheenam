﻿//==================================================
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//==================================================

using Microsoft.EntityFrameworkCore;
using Sheenam.Api.Models.Foundations.Guests;

namespace Sheenam.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Guest> Guests { get; set; }

        public async ValueTask<Guest> InsertGuestAsync(Guest guest)=>
        await InsertAsync(guest);
        
        public IQueryable<Guest> SelectAllGuests()=>
        SelectAll<Guest>();
        
        public async ValueTask<Guest> SelectGuestByIdAsync(Guid guestId)=>
        await SelectAsync<Guest>(guestId);
        
        public async ValueTask<Guest> UpdateGuestAsync(Guest guest)=>
         await UpdateAsync(guest);
        
       public async ValueTask<Guest> DeleteGuestAsync(Guest guest)=>
        await DeleteAsync<Guest>(guest);
    }
}
