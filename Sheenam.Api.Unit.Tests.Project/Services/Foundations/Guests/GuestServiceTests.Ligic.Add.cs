//=================================================
//Copyright (c) Coalition of Good-Hearted Engineers 
//Free To Use To Find Comfort and Pease
//=================================================

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Sheenam.Api.Models.Foundations.Guests;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Guests
{
    public partial class GuestServiceTests
    {
        [Fact]
        public async Task ShouldAddGuestAsync()
        {
            //given
            Guest randomGuest = CreateRandomGuest();
            Guest inputGuest = randomGuest;
            Guest returningGuest = inputGuest;
            Guest expectedGuest = returningGuest.DeepClone();

            this.storageBrokerMock.Setup(broker =>
            broker.InsertGuestAsync(inputGuest))
                .ReturnsAsync(returningGuest);
            //when
            Guest actualGuest =
                await this.guestService.AddGuestAsync(inputGuest);

            //then
            actualGuest.Should().BeEquivalentTo(expectedGuest);

            this.storageBrokerMock.Verify(broker =>
            broker.InsertGuestAsync(inputGuest), Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();

        }
        [Fact]
        public void ShouldRetrieveAllGuests()
        {
            //given
            IQueryable<Guest> randomGuests = CreateRandomGuests();
            IQueryable<Guest> storageGuests = randomGuests;
            IQueryable<Guest> expectedGuests = storageGuests.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllGuests()).Returns(storageGuests);

            //when
            IQueryable<Guest> actualGuests =
                this.guestService.RetrieveAllGuests();

            //then
            actualGuests.Should().BeEquivalentTo(expectedGuests);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllGuests(), Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
