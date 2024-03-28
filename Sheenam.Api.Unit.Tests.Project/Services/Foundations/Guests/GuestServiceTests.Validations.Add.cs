//=================================================
//Copyright (c) Coalition of Good-Hearted Engineers 
//Free To Use To Find Comfort and Pease
//=================================================

using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Moq;
using Sheenam.Api.Models.Foundations.Guests;
using Sheenam.Api.Models.Foundations.Guests.Exceptions;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Guests
{
    public partial class GuestServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfGuestIsNullAndLogItAsync()
        {
            //given
            Guest nullGuest = null;

            var nullGuestException = new NullGuestException();

            var expectedGuestValidationException =
                new GuestValidationException(nullGuestException);

            // when
            ValueTask<Guest> addGuestTask =
                this.guestService.AddGuestAsync(nullGuest);

            //then
            await Assert.ThrowsAsync<GuestValidationException>(() =>
            addGuestTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
            broker.LogError(It.Is(SameExceptionAs(expectedGuestValidationException))),
                Times.Once);
            this.storageBrokerMock.Verify(broker =>
            broker.InsertGuestAsync(It.IsAny<Guest>()),
            Times.Never);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();

        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnAddIfGuestIsInvalidDataAndLogItAsync(string invalidData)
        {
            // given
            var invalidGuest = new Guest()
            {
                FirstName = invalidData
            };

            InvalidGuestException invalidGuestException = new();

            invalidGuestException.AddData(key: nameof(Guest.Id),
                values: "Id is required");

            invalidGuestException.AddData(key: nameof(Guest.FirstName),
                values: "Text is invalid");

            invalidGuestException.AddData(key: nameof(Guest.LastName),
               values: "Text is invalid");

            invalidGuestException.AddData(key: nameof(Guest.DateOfBirth),
              values: "Date is invalid");

            invalidGuestException.AddData(key: nameof(Guest.Email),
      values: "Text is invalid");

            invalidGuestException.AddData(key: nameof(Guest.Address),
                values: "Text is invalid");

            var expectedGuestValidationExpected =
                new GuestValidationException(invalidGuestException);

            // when
            ValueTask<Guest> addGuestTask =
               this.guestService.AddGuestAsync(invalidGuest);

            // then
            await Assert.ThrowsAsync<GuestValidationException>(() => addGuestTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
              broker.LogError(It.Is(SameExceptionAs(expectedGuestValidationExpected))),
              Times.Once());

            this.storageBrokerMock.Verify(broker =>
              broker.InsertGuestAsync(It.IsAny<Guest>()),
              Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
        [Fact]
        public async Task ShouldThrowExceptionOnAddIfGenderIsInvalidAndLogItAsync()
        {
            // given
            Guest randomGuest = CreateRandomGuest();
            Guest invalidGuest = randomGuest;

            invalidGuest.Gender = GetInvalidEnum<GenderType>();
            var invalidGuestException = new InvalidGuestException();

            invalidGuestException.AddData(key: nameof(Guest.Gender),
                values: "Value is invalid");

            var expectedGuestValidationException =
                new GuestValidationException(invalidGuestException);

            // when
            ValueTask<Guest> AddGuestTask =
                this.guestService.AddGuestAsync(invalidGuest);

            // then
            await Assert.ThrowsAsync<GuestValidationException>(() =>
               AddGuestTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
               broker.LogError(It.Is(SameExceptionAs(expectedGuestValidationException))),
               Times.Once());

            this.storageBrokerMock.Verify(broker =>
               broker.InsertGuestAsync(It.IsAny<Guest>()),
               Times.Never());

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogItAsync()
        {
            //given
            Guest someGuest = CreateRandomGuest();
            SqlException sqlException = GetSqlError();
            var failedGuestStorageException =
                new FailedGuestStorageException(sqlException);
            var expectedGuestDependencyException =
                new GuestDependencyException(failedGuestStorageException);

            this.storageBrokerMock.Setup(broker =>
            broker.InsertGuestAsync(someGuest))
                .ThrowsAsync(sqlException);

            //when 
            ValueTask<Guest> addGuestTask =
                this.guestService.AddGuestAsync(someGuest);
            //then
            await Assert.ThrowsAsync<GuestDependencyException>(() => addGuestTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
            broker.InsertGuestAsync(someGuest), Times.Once());

            this.loggingBrokerMock.Verify(broker =>
            broker.LogCritical(It.Is(SameExceptionAs(
                expectedGuestDependencyException))),
                Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();

        }
        [Fact]
        public async Task ShouldThrowDependencyValidationOnAddIfDublicateKeyErrorOccursAndLogItAsync()
        {
            //given
            Guest someGuest = CreateRandomGuest();
            string someMessage = GetRandomString();

            var duplicateKeyException =
                new DuplicateKeyException(someMessage);

            var alreadyExistGuestException =
                new AlreadyExistGuestException(duplicateKeyException);
            var expectedGuestDependencyValidationException =
                new GuestDependencyValidationException(alreadyExistGuestException);

            this.storageBrokerMock.Setup(broker =>
            broker.InsertGuestAsync(someGuest))
                .ThrowsAsync(duplicateKeyException);

            //when
            ValueTask<Guest> addGuestTask =
                this.guestService.AddGuestAsync(someGuest);


            //then
            await Assert.ThrowsAsync<GuestDependencyValidationException>(() =>
            addGuestTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
            broker.InsertGuestAsync(someGuest),
            Times.Once);

            this.loggingBrokerMock.Verify(broker =>
            broker.LogError(It.Is(SameExceptionAs(
                expectedGuestDependencyValidationException))),
                Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfServiceErrorOccursAndLogItAsync()
        {
            //given
            Guest someGuest = CreateRandomGuest();
            var serviceException = new Exception();

            var failedGuestServiceException =
                new FailedGuestServiceException(serviceException);

            var expectedGuestServiceException = new GuestServiceException(
                failedGuestServiceException);
            this.storageBrokerMock.Setup(broker =>
            broker.InsertGuestAsync(someGuest))
                .ThrowsAsync(serviceException);

            //when

            ValueTask<Guest> addGuestTask =
                this.guestService.AddGuestAsync(someGuest);

            //than
            await Assert.ThrowsAsync<GuestServiceException>(() =>
            addGuestTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
            broker.InsertGuestAsync(someGuest),
            Times.Once);

            this.loggingBrokerMock.Verify(broker =>
            broker.LogError(It.Is(SameExceptionAs(
                expectedGuestServiceException))),
                Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}