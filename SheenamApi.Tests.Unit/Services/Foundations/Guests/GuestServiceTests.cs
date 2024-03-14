﻿//=================================================
//Copyright (c) Coalition of Good-Hearted Engineers 
//Free To Use To Find Comfort and Pease
//=================================================

using Moq;
using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.Guests;
using Sheenam.Api.Services.Foundations.Guests;
using System.Linq.Expressions;
using Tynamix.ObjectFiller;
using Xeptions;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Guests
{
    public partial  class GuestServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IGuestService guestService;

        public GuestServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.guestService = new GuestService
                (storageBroker: this.storageBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static Guest CreateRandomGuest() =>
            CreateGuestFiller(data: GetRandomDateTimeOffset()).Create();

        public static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static int GetRandomNumber()
        {
            return new IntRange(min: 2, max: 9).GetValue();
        }
        
        private static T GetInvalidEnum<T>()
        {
            int randomNumber=GetRandomNumber();

            while (Enum.IsDefined(typeof(T),randomNumber) is true)
            {
                randomNumber=GetRandomNumber();
            }
            return (T)(object)randomNumber;
        }

        private Expression<Func<Xeption,bool>> SameExceptionAs(Xeption expectedException)=>
            actualException =>actualException.SameExceptionAs(expectedException);
           



        private static Filler<Guest> CreateGuestFiller(DateTimeOffset data)
        {
            var filler=new Filler<Guest>();

            filler.Setup()
               .OnType<DateTimeOffset>().Use(data);
            return filler;
        }
    }
}
