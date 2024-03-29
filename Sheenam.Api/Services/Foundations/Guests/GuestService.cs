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
                ValidateGuestOnAdd(guest);

                return await this.storageBroker.InsertGuestAsync(guest);
            });

        public IQueryable<Guest> RetrieveAllGuests() =>
     TryCatch(() => this.storageBroker.SelectAllGuests());


        public ValueTask<Guest> RetrieveGuestByIdAsync(Guid guestId) =>
      TryCatch(async () =>
      {
          ValidateGuestId(guestId);

          Guest maybeGuest =
              await storageBroker.SelectGuestByIdAsync(guestId);

          ValidateStorageGuestExists(maybeGuest, guestId);

          return maybeGuest;
      });

        public ValueTask<Guest> ModifyGuestAsync(Guest Guest) =>
           TryCatch(async () =>
           {
               ValidateGuestOnModify(Guest);
               var maybeGuest =
                   await this.storageBroker.SelectGuestByIdAsync(Guest.Id);
               ValidateAgainstStorageGuestOnModify(inputGuest: Guest, storageGuest: maybeGuest);
               return await this.storageBroker.UpdateGuestAsync(Guest);
           });
    }
}







