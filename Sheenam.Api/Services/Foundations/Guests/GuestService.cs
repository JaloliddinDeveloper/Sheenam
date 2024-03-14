﻿//==================================================
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//==================================================

using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.Guests;

namespace Sheenam.Api.Services.Foundations.Guests
{
    public partial class GuestService : IGuestService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public GuestService(IStorageBroker storageBroker, ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }


        public ValueTask<Guest> AddGuestAsync(Guest guest) =>
            TryCatch(async () =>
            {
                ValidateGuestOnModify(guest);

                return await this.storageBroker.InsertGuestAsync(guest);
            });
    }
}
        

            
        

    