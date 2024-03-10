//==================================================
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//==================================================

using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.Guests;

namespace Sheenam.Api.Services.Foundations.Guests
{
    public class GuestService : IGuestService
    {
        private readonly IStorageBroker storageBroker;

        public GuestService(IStorageBroker storageBroker)=>
            this.storageBroker = storageBroker;
        

        public async ValueTask<Guest> AddGuestAsync(Guest guest)
        {
            return await storageBroker.InsertGuestAsync(guest);
        }
            

        
    }
}
