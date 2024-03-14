// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Sheenam.Api.Models.Foundations.Guests;
using Sheenam.Api.Models.Foundations.Guests.Exceptions;
using System;
using Xeptions;

namespace Sheenam.Api.Services.Foundations.Guests
{
    public partial class GuestService
    {
        private delegate ValueTask<Guest> ReturningGuestFunction();

        private async ValueTask<Guest> TryCatch(ReturningGuestFunction returningGuestFunction)
        {
            try
            {
                return await returningGuestFunction();
            }
            catch (NullGuestException nullGuestException)
            {
                throw CreateAndLogValidationException(nullGuestException);
            }
            catch (InvalidGuestException invalidGuestException)
            {
                throw CreateAndLogValidationException(invalidGuestException);
            }
            catch(SqlException  sqlException)
            {
                var failedGuestStorageException=new FailedGuestStorageException(sqlException);
                throw CreateAndLogCriticalDependencyException(failedGuestStorageException);
            }
            catch(DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistGuestException=new AlreadyExistGuestException(duplicateKeyException);
                throw CreateAndDependencyValidationException(alreadyExistGuestException);
            }
            catch(Exception exception)
            {
                var failedGuestServiceException=
                    new FailedGuestServiceException(exception);

                throw CreateAndLogServiseException(failedGuestServiceException);
            }
            
        }
        private GuestValidationException CreateAndLogValidationException(Xeption exception)
        {
            var guestValidationException =
                   new GuestValidationException(exception);

            this.loggingBroker.LogError(guestValidationException);

            return guestValidationException;
        }
        private GuestDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
           var guestDependencyException = new GuestDependencyException(exception);
            this.loggingBroker.LogCritical(guestDependencyException);

            return guestDependencyException;
        }
        public GuestDependencyValidationException CreateAndDependencyValidationException(Xeption xeption)
        {
            var guestDependencyValidationException=new GuestDependencyValidationException(xeption);
            this.loggingBroker.LogError(guestDependencyValidationException);
            return guestDependencyValidationException;
        }

        public GuestServiceException CreateAndLogServiseException(Xeption xeption)
        {
            var guestServiceException=new GuestServiceException(xeption);
            this.loggingBroker.LogError(guestServiceException);
            return guestServiceException;
        }
    }
}
